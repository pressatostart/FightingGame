using UnityEngine;
using SakugaEngine.GameState;
using SakugaEngine.Global;
using MessagePack;

namespace SakugaEngine
{
    public partial class SakugaVFX : SakugaNode
    {
        private SakugaActor _master;
        [Header("Settings")]
        [SerializeField] private int Duration;
        [SerializeField] private Animator Player;
        [SerializeField] private Transform Graphics;
        [SerializeField] private string AnimationName;
        [SerializeField] private SoundQueue Sound;
        [HideInInspector] public bool AttachedToOwner;
        [HideInInspector] public Vector2Int FixedPosition;
        [HideInInspector] public int Frame;
        [HideInInspector] public int Side;

        SakugaVFX actor;
        private SakugaVFXState State;

        public void SetMaster(SakugaActor newMaster) => _master = newMaster;
        public SakugaActor GetMaster() => _master;

        public override void Render()
        {
            Graphics.gameObject.SetActive(IsActive);
            Vector2Int pos = FixedPosition;
            if (AttachedToOwner) pos = _master.Body.FixedPosition + new Vector2Int(FixedPosition.x, FixedPosition.y);
            Graphics.position = GlobalFunctions.ToScaledVector3(pos);
            Graphics.rotation = Quaternion.identity;
            Graphics.localScale = new Vector3(Side, 1, 1);
            Player.PlayInFixedTime(AnimationName, 0, (Duration - Frame) / (float)GlobalVariables.TicksPerSecond);
        }

        public override void Initialize(uint id)
        {
            base.Initialize(id);
            actor = this;
            FixedPosition = Vector2Int.zero;
            Side = 0;
            Frame = -1;
        }

        public void Reset()
        {
            IsActive = StartActive;
            FixedPosition = Vector2Int.zero;
            Side = 0;
            Frame = -1;
        }

        public void Spawn(Vector2Int origin, int side, bool attached)
        {
            AttachedToOwner = attached;
            FixedPosition = origin;
            Side = side;
            Frame = Duration;
            if (AttachedToOwner)
            {
                FixedPosition -= new Vector2Int(_master.Body.FixedPosition.x, _master.Body.FixedPosition.y);
            } 
            if (Sound != null)
            {
                Sound.SimpleQueueSound();
            }
            IsActive = true;
        }

        public override void LateTick()
        {
            if (!IsActive) return;

            Frame--;
            if (Frame < 0) { IsActive = false;  return; }
        }

        public override byte[] GetStateData()
        {
            State.GetStateData(ref actor);
            return MessagePackSerializer.Serialize(State);
        }
		public override void SetStateData(byte[] stateBuffer)
        {
            State = MessagePackSerializer.Deserialize<SakugaVFXState>(stateBuffer);
            State.SetStateData(ref actor);
        }
    }
}
