using UnityEngine;
using UnityEngine.InputSystem;
using PleaseResync;
using SakugaEngine.Resources;
using SakugaEngine.Collision;
using SakugaEngine.UI;
using System.Text;
using System.Collections.Generic;
using SakugaEngine.Global;
using MessagePack;
using SakugaEngine.GameState;
using System;
using TMPro;

namespace SakugaEngine.Game
{
    public partial class GameManager : MonoBehaviour, IGameState
    {
        [HideInInspector] public static GameManager Instance;
        [HideInInspector] public InputManager[] Inputs = new InputManager[0];
        public MatchSettings Match;
        public GameMonitor Monitor;
        public FighterList fightersList;
        public StageList stagesList;
        public BGMList songsList;
        public HitNotificationList notifications;
        //[SerializeField] private CanvasLayer FighterUI;
        public FighterCamera Camera;
        //[Export] public AudioStreamPlayer BGMSource;
        [SerializeField] private TextMeshProUGUI SeedViewer;
        [SerializeField] private FadeScreen Curtain;
        [HideInInspector] public uint InputSize;

        [HideInInspector] public List<SakugaNode> Nodes = new List<SakugaNode>();
        private SakugaActor[] Fighters;
        private PhysicsWorld World;
        
        [SerializeField] private HealthHUD healthHUD;
        [SerializeField] private MetersHUD metersHUD;

        [HideInInspector] public int Frame = 0;

        //Random Number Generator
        private int generatedSeed = 0;
        private int finalSeed = 0;
        public bool ShowHitboxes;

        [HideInInspector] public Vector3Int randomTest = new Vector3Int();

        private GameManagerState State;
        //private PauseMenu PauseScreen;

        [HideInInspector] public HitNotifications[] Notifs;

        private bool paused;
        
        public void Awake()
        {
            Application.targetFrameRate = 60;
            Instance = this;
            //healthHUD = (HealthHUD)FighterUI.GetNode("GameHUD_Background");
            //metersHUD = (MetersHUD)FighterUI.GetNode("GameHUD_Foreground");
            //SeedViewer = (Label)FighterUI.GetNode("Seed");
            //GameObject OnlinePlayersInfo = healthHUD.GetNode("OnlineInfo");
            //GameObject TrainingInfo = metersHUD.GetNode("TrainingInfo");
            //GameObject InputHistory = metersHUD.GetNode("InputHistory");
            //GameObject DebugWindow = FighterUI.GetNode("Debug");
            //GameObject NetcodeHUD = FighterUI.GetNode("PleaseResync_UI");

            //PauseScreen = (PauseMenu)FighterUI.GetNode("PauseMenu");
            if (Monitor != null)
            {
                //Monitor.Cards = FighterUI.transform.GetComponentInChildren<MatchCardsController>();
                //Monitor.ResultsScreen = (Control)FighterUI.GetNode("ResultsScreen");
                //Monitor.ResultsScreen.SetActive(false);
            }
            //Curtain = (FadeScreen)FighterUI.GetNode("FadeScreen");
            //OnlinePlayersInfo.SetActive(Match.SelectedModeSettings.ShowOnlineInfo);
            //TrainingInfo.SetActive(Match.SelectedModeSettings.ShowTrainingInfo);
            //InputHistory.SetActive(Match.SelectedModeSettings.AllowShowInputs);
            //DebugWindow.SetActive(Match.SelectedModeSettings.AllowShowDebugElements);
            //NetcodeHUD.SetActive(Match.SelectedModeSettings.UseLAN);
            SeedViewer.gameObject.SetActive(Match.SelectedModeSettings.AllowShowDebugElements);

            //if (Match.SelectedModeSettings.ShowOnlineInfo && OnlinePlayersInfo != null)
            //{
                //OnlinePlayersInfo.GetComponent<OnlineInfo>().Set(Match);
            //}
        }

        public void Update()
        {
            if (Fighters == null) return;
            if (Monitor == null) return;

            if (SeedViewer != null && SeedViewer.isActiveAndEnabled) SeedViewer.text = finalSeed.ToString();
            /*if (Input.IsActionJustPressed("pause") && Match.SelectedModeSettings.PauseMode != PauseMode.LOCK && Monitor.MatchState == MatchState.ROUND_RUNNING)
            {
                //CallDeferred("PauseControl", !paused);
                PauseControl(!paused);
            }
            // Return to menu for now
            if (Monitor.MatchState == MatchState.RESULTS && (Input.IsActionJustPressed("ui_accept") || Input.IsActionJustPressed("ui_cancel")))
            {
                LoadingScreenManager.Instance.LoadScene(Monitor.sceneToReturn);
            }*/

            DebugFeatures();
        }

        public void Render()
        {
            RenderNodes();
            Camera.UpdateCamera(Fighters[0], Fighters[1]);
            if (healthHUD != null) healthHUD.UpdateHealthBars(Fighters, Monitor);
            if (healthHUD != null) healthHUD.UpdateHitNotifications(Notifs);
            if (metersHUD != null) metersHUD.UpdateMeters(Fighters);
            Curtain.FadeIntensity = Monitor.FadeScreenIntensity;
            Monitor.Render();
        }

        void RenderNodes()
        {
            foreach (SakugaNode node in Nodes)
            {
                node.Render();
            }
        }

        /// <summary>
        /// Generates the base seed to be used for generating the PRNG. The base seed is a non-random 
        /// number generated with a default string and both characters' names.
        /// </summary>
        private void GenerateBaseSeed()
        {
            string seedText = RNG.baseSeed + Fighters[0].Data.Profile.FighterName + Fighters[1].Data.Profile.FighterName;
            byte[] seedArray = Encoding.ASCII.GetBytes(seedText);
            generatedSeed = (int)Platform.GetChecksum(seedArray);
        }

        /// <summary>
        /// Generate the PRNG seed with a bunch of everchanging values. 
        /// If the values used are deterministic, the generated seed will be deterministic.
        /// </summary>
        /// <returns>a 32-bit seed number</returns>
        private int CalculateSeed()
        {
            int posX = Fighters[0].Body.FixedPosition.x + Fighters[1].Body.FixedPosition.x;
            int posY = Fighters[0].Body.FixedPosition.y + Fighters[1].Body.FixedPosition.y;
            int stateFrame = Fighters[0].StateManager.CurrentStateFrame + Fighters[0].StateManager.CurrentState;
            stateFrame += Fighters[1].StateManager.CurrentStateFrame + Fighters[1].StateManager.CurrentState;
            return generatedSeed + posX + posY + stateFrame + (Frame * GlobalVariables.SimulationScale) + Monitor.Clock;
        }

        public void Setup()
        {
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }

            Inputs = new InputManager[2];
            Inputs[0] = new InputManager();
            Inputs[1] = new InputManager();

            Frame = 0;

            CreateStage(Match.SelectedStage);

            World = new PhysicsWorld();
            Nodes.Clear();
            Fighters = new SakugaActor[2];

            CreateFighter(Match.P1SelectedCharacter, 0);
            CreateFighter(Match.P2SelectedCharacter, 1);

            Fighters[0].SetAllies(new SakugaActor[]{ Fighters[0] });
            Fighters[1].SetAllies(new SakugaActor[]{ Fighters[1] });
            Fighters[0].SetOpponents(new SakugaActor[]{ Fighters[1] });
            Fighters[1].SetOpponents(new SakugaActor[]{ Fighters[0] });

            //Temporary code to define player inputs
            Fighters[1].GetComponent<PlayerInput>().SwitchCurrentActionMap("Keyboard2");

            GenerateBaseSeed();
            Monitor.Initialize(Fighters, Match);
            Notifs = new HitNotifications[2]
            {
                new HitNotifications(5), 
                new HitNotifications(5)
            };

            healthHUD.Setup(Fighters);
            metersHUD.Setup(Fighters);
            
            AudioManager.Instance.PlayBGM(songsList.elements[Match.SelectedBGM].clip);
        }

        public void CreateFighter(int characterIndex, int playerIndex)
        {
            bool AIcontrolled = false;
            int selectedColor = 0;
            switch (playerIndex)
            {
                case 0:
                    AIcontrolled = !Match.SelectedModeSettings.NoAI && Match.P1SelectedDevice == -1;
                    selectedColor = Match.P1SelectedColor;
                    break;
                case 1:
                    AIcontrolled = !Match.SelectedModeSettings.NoAI && Match.P2SelectedDevice == -1;
                    selectedColor = Match.P2SelectedColor;
                    if (characterIndex == Match.P1SelectedCharacter && Match.P1SelectedColor == Match.P2SelectedColor)
                        selectedColor++;
                    if (selectedColor >= fightersList.elements[characterIndex].ColorPalettes.Length)
                        selectedColor = 0;
                    break;
            }
            
            GameObject temp = Instantiate(fightersList.elements[characterIndex].ColorPalettes[selectedColor].Instance, transform);
            Fighters[playerIndex] = temp.GetComponent<SakugaActor>();
            Fighters[playerIndex].playerID = (uint)playerIndex;
            AddActor(Fighters[playerIndex]);
            Fighters[playerIndex].name = playerIndex + "_Fighter_" + Fighters[playerIndex].Data.Profile.ShortName;
            Fighters[playerIndex].Inputs = Inputs[playerIndex];
            Fighters[playerIndex].InitializeBrain(AIcontrolled, Match.Difficulty);
            Fighters[playerIndex].InitializePool();
        }

        public void CreateStage(int stageIndex)
        {
            GameObject temp = Instantiate(stagesList.elements[stageIndex].Instance, transform);
            //AddChild(temp);
        }

        public void AddActor(SakugaNode newNode, bool isPhysicsBody = true)
        {
            newNode.Initialize((uint)Nodes.Count);
            //AddChild(newNode);
            Nodes.Add(newNode);
            if (isPhysicsBody) World.AddBody((newNode as SakugaActor).Body);
        }

        public SakugaNode GetActor(int nodeID)
        {
            return Nodes[nodeID];
        }

        public void GameLoop(byte[] playerInput)
        {
            if (paused) return;

            finalSeed = CalculateSeed();
            RNG.Set(finalSeed);
            Frame++;
            Monitor.Tick();

            randomTest = new Vector3Int(
                RNG.Next(),
                RNG.Next(),
                RNG.Next()
            );

            int center = (Fighters[0].Body.FixedPosition.x + Fighters[1].Body.FixedPosition.x) / 2;

            foreach (var notif in Notifs)
            {
                notif.Tick();
            }

            for (int i = 0; i < Fighters.Length; i++)
            {
                if (Monitor.MatchState == MatchState.ROUND_START || Monitor.MatchState == MatchState.ROUND_END)
                {
                    Inputs[i].InsertToHistory(0);
                    continue;
                }
                if (Fighters[i].UseAI)
                {
                    Fighters[i].Brain.SelectCommand();
                    Fighters[i].Brain.UpdateCommand();
                    continue;
                }

                PlayerInputs combinedInput = 0;
                combinedInput |= (PlayerInputs)playerInput[i * InputSize];
                combinedInput |= (PlayerInputs)(playerInput[(i * InputSize) + 1] << 8);
                Inputs[i].InsertToHistory(combinedInput);
            }

            for (int i = 0; i < Nodes.Count; i++)
                Nodes[i].PreTick();

            for (int i = 0; i < Nodes.Count; i++)
                Nodes[i].Tick();

            World.Simulate();

            if (Fighters[0].Body.FixedPosition.x < Fighters[1].Body.FixedPosition.x)
            { Fighters[0].Body.UpdateSide(true); Fighters[1].Body.UpdateSide(false); }
            else if (Fighters[0].Body.FixedPosition.x > Fighters[1].Body.FixedPosition.x)
            { Fighters[0].Body.UpdateSide(false); Fighters[1].Body.UpdateSide(true); }

            for (int i = 0; i < Fighters.Length; i++)
            {
                Fighters[i].Body.FixedPosition.x = Mathf.Clamp(
                    Fighters[i].Body.FixedPosition.x,
                    center - GlobalVariables.MaxPlayersDistance / 2,
                    center + GlobalVariables.MaxPlayersDistance / 2
                );
                Fighters[i].Body.UpdateColliders();
                if (!Match.SelectedModeSettings.CanKO)
                {
                    if (Fighters[i].Parameters.Health.CurrentValue <= 0)
                        Fighters[i].Parameters.Health.CurrentValue = 1;
                    if (!Fighters[i].OnHitstun() && Fighters[i].StateManager.CurrentStateType() != StateType.HIT_REACTION)
                        Fighters[i].Parameters.Health.CurrentValue = Fighters[i].Data.MaxHealth;
                }
                if (Match.SelectedModeSettings.AutoFillResources)
                {
                    if (Fighters[i].StateManager.CurrentStateType() <= StateType.MOVEMENT)
                    {
                        Fighters[i].Parameters.SuperGauge.CurrentValue = Fighters[i].Data.MaxSuperGauge;
                    }    
                }
            }
            
            for (int i = 0; i < Nodes.Count; i++)
                Nodes[i].LateTick();
        }

        public void DebugFeatures()
		{
            if (Input.GetKeyDown(KeyCode.F10) && Match.SelectedModeSettings.AllowShowHitboxes)
            {
                ShowHitboxes = !ShowHitboxes;
            }

            if (!Match.SelectedModeSettings.AllowUseDebugCommands) return;

            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                    Fighters[0].Parameters.Health.SetHealth(0);
                else
                    Fighters[0].Parameters.Health.RemoveHealth(100);
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    Fighters[1].Parameters.Health.SetHealth(0);
                else
                    Fighters[1].Parameters.Health.RemoveHealth(100);
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    Fighters[0].Parameters.Health.SetHealth(Fighters[0].Data.MaxHealth);
                else
                    Fighters[0].Parameters.Health.AddHealth(100);
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    Fighters[1].Parameters.Health.SetHealth(Fighters[1].Data.MaxHealth);
                else
                    Fighters[1].Parameters.Health.AddHealth(100);
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Fighters[0].Parameters.SuperGauge.SetSuperGauge(Fighters[0].Data.MaxSuperGauge);
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                Fighters[1].Parameters.SuperGauge.SetSuperGauge(Fighters[1].Data.MaxSuperGauge);
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    Monitor.ResetTimer();
                else
                    Monitor.Clock = 0;
            }
            if (Input.GetKeyDown(KeyCode.F12))
            {
                Fighters[0].Reset();
                Fighters[1].Reset();
            }
        }

        public void PauseControl(bool pause)
        {
            if (Match.SelectedModeSettings.PauseMode == PauseMode.LOCK) return;
            if (Monitor.MatchState != MatchState.ROUND_RUNNING) return;

            paused = pause;
            //PauseScreen.CallPauseMenu(paused);
        }

        public void PlayHitNotificationByIndex(int playerIndex, int notifIndex)
        {
            HitNotificationElement hitNotif = notifications.Elements[notifIndex];
            PlayHitNotification(playerIndex, hitNotif.NotificationName, hitNotif.AnnouncerVoiceLine);
        }

        public bool WaitingForFirstStrike()
        {
            return Monitor.CurrentRound == 0 && Match.SelectedModeSettings.CanKO && 
                Fighters[0].Parameters.Health.CurrentValue == Fighters[0].Data.MaxHealth && 
                Fighters[1].Parameters.Health.CurrentValue == Fighters[1].Data.MaxHealth;
        }

        public void PlayHitNotification(int playerIndex, string name, int announcerLine)
        {
            // Spawn notification on screen
            Notifs[playerIndex].Notify(name);
            AudioManager.Instance.PlayAnnouncerClip(announcerLine);
        }

        // Generate inputs for your game
        public byte[] ReadInputs(int id, int inputSize)
        {
            if (id < 0) return new byte[inputSize];

            PlayerInputs input = 0;

            //string prefix = GlobalFunctions.GetPlayerPrefix(id);
            PlayerInput controller = Fighters[id].GetComponent<PlayerInput>();

            if (controller.actions["Up"].IsInProgress() && !controller.actions["Down"].IsInProgress())
                input |= PlayerInputs.UP;

            if (!controller.actions["Up"].IsInProgress() && controller.actions["Down"].IsInProgress())
                input |= PlayerInputs.DOWN;

            if (controller.actions["Left"].IsInProgress() && !controller.actions["Right"].IsInProgress())
                input |= PlayerInputs.LEFT;

            if (!controller.actions["Left"].IsInProgress() && controller.actions["Right"].IsInProgress())
                input |= PlayerInputs.RIGHT;

            if (controller.actions["Face A"].IsInProgress())
                input |= PlayerInputs.FACE_A;

            if (controller.actions["Face B"].IsInProgress())
                input |= PlayerInputs.FACE_B;

            if (controller.actions["Face C"].IsInProgress())
                input |= PlayerInputs.FACE_C;

            if (controller.actions["Face D"].IsInProgress())
                input |= PlayerInputs.FACE_D;
            
            if (controller.actions["Face E"].IsInProgress())
                input |= PlayerInputs.FACE_E;

            if (controller.actions["Face F"].IsInProgress())
                input |= PlayerInputs.FACE_F;

            if (controller.actions["Face G"].IsInProgress())
                input |= PlayerInputs.FACE_G;

            if (controller.actions["Face H"].IsInProgress())
                input |= PlayerInputs.FACE_H;

            /*if (Input.IsActionPressed(prefix + "_macro_ab"))
                input |= Global.INPUT_FACE_A | Global.INPUT_FACE_B;

            if (Input.IsActionPressed(prefix + "_macro_ac"))
                input |= Global.INPUT_FACE_A | Global.INPUT_FACE_C;
            
            if (Input.IsActionPressed(prefix + "_macro_bc"))
                input |= Global.INPUT_FACE_B | Global.INPUT_FACE_C;

            if (Input.IsActionPressed(prefix + "_macro_abc"))
                input |= Global.INPUT_FACE_A | Global.INPUT_FACE_B | Global.INPUT_FACE_C;

            if (Input.IsActionPressed(prefix + "_macro_abcd"))
                input |= Global.INPUT_FACE_A | Global.INPUT_FACE_B | Global.INPUT_FACE_C | Global.INPUT_FACE_D;*/
        
            return BitConverter.GetBytes((ushort)input);
        }

        public byte[] SaveState()
        {
            State.GetStateData(ref Instance);
            return MessagePackSerializer.Serialize(State);
        }

        public void LoadState(byte[] stateBuffer)
        {
            State = MessagePackSerializer.Deserialize<GameManagerState>(stateBuffer);
            State.SetStateData(ref Instance);
        }

        public byte[] GetLocalInput(int PlayerID, int InputSize)
        {
            int playerInputDevice = 0;
            if (PlayerID == 0) playerInputDevice = Match.P1SelectedDevice;
            else if (PlayerID == 1) playerInputDevice = Match.P2SelectedDevice;
            byte[] result = ReadInputs(playerInputDevice, InputSize);

            return result;
        }
    }
}
