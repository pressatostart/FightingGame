using SakugaEngine.Global;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SakugaEngine.UI
{
    public partial class HealthHUD : MonoBehaviour
    {    
        [Header("Player 1")]
        [SerializeField] private Image P1Portrait;
        [SerializeField] private Slider P1Health;
        [SerializeField] private Slider P1LostHealth;
        [SerializeField] private RoundsCounter P1Rounds;
        [SerializeField] private ComboCounter P1Combo;
        [SerializeField] private TextMeshProUGUI P1Name;
        [SerializeField] private HitNotificationController P1Notif;
        
        [Header("Player 2")]
        [SerializeField] private Image P2Portrait;
        [SerializeField] private Slider P2Health;
        [SerializeField] private Slider P2LostHealth;
        [SerializeField] private RoundsCounter P2Rounds;
        [SerializeField] private ComboCounter P2Combo;
        [SerializeField] private TextMeshProUGUI P2Name;
        [SerializeField] private HitNotificationController P2Notif;

        [Header("Extra")]
        [SerializeField] private TextMeshProUGUI Timer;
        [SerializeField] private TextMeshProUGUI P1Debug;
        [SerializeField] private TextMeshProUGUI P2Debug;

        public void Setup(SakugaActor[] fighters)
        {
            P1Health.maxValue = fighters[0].Parameters.Health.MaxValue;
            P2Health.maxValue = fighters[1].Parameters.Health.MaxValue;

            P1Name.text = fighters[0].Data.Profile.ShortName;
            P2Name.text = fighters[1].Data.Profile.ShortName;
            
            if (fighters[0].Data.Profile.Portrait != null)
                P1Portrait.sprite = fighters[0].Data.Profile.Portrait;

            if (fighters[1].Data.Profile.Portrait != null)
                P2Portrait.sprite = fighters[1].Data.Profile.Portrait;

            P1Rounds.Setup();
            P2Rounds.Setup();
        }

        public void UpdateHealthBars(SakugaActor[] fighters, GameMonitor monitor)
        {
            P1Health.value = fighters[0].Parameters.Health.CurrentValue;
            P2Health.value = fighters[1].Parameters.Health.CurrentValue;
            P1LostHealth.value = fighters[0].Parameters.Health.LostValue;
            P2LostHealth.value = fighters[1].Parameters.Health.LostValue;

            UpdateTimer(monitor);

            P1Rounds.ShowRounds(monitor.VictoryCounter[0]);
            P2Rounds.ShowRounds(monitor.VictoryCounter[1]);

            P1Combo.gameObject.SetActive(fighters[1].Parameters.Tracker.HitCombo > 0);
            P2Combo.gameObject.SetActive(fighters[0].Parameters.Tracker.HitCombo > 0);

            P1Combo.UpdateCounter((int)fighters[1].Hitstun.TimeLeft, fighters[1].Parameters.Tracker);
            P2Combo.UpdateCounter((int)fighters[0].Hitstun.TimeLeft, fighters[0].Parameters.Tracker);
            UpdateDebug(fighters);
        }

        public void UpdateDebug(SakugaActor[] fighters)
        {
            P1Debug.text = fighters[0].DebugInfo();
            P2Debug.text = fighters[1].DebugInfo();
        }

        public void UpdateTimer(GameMonitor monitor)
        {
            if (monitor.ClockLimit < 0)
            {
                Timer.text = "--";
                return;
            }
            int time = monitor.Clock / GlobalVariables.TicksPerSecond;
            time = Mathf.Clamp(time, 0, monitor.ClockLimit);
            Timer.text = time.ToString();
        }

        public void UpdateHitNotifications(HitNotifications[] notifs)
        {
            P1Notif.View(notifs[0].Elements);
            P2Notif.View(notifs[1].Elements);
        }
    }
}
