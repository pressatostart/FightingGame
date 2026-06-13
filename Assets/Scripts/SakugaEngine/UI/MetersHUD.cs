using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SakugaEngine.UI
{
    public partial class MetersHUD : MonoBehaviour
    {
        [SerializeField] private Slider P1Meter;
        [SerializeField] private Slider P2Meter;
        [SerializeField] private TextMeshProUGUI P1TrainingInfo;
        [SerializeField] private TextMeshProUGUI P2TrainingInfo;
        [SerializeField] private InputHistory P1InputHistory;
        [SerializeField] private InputHistory P2InputHistory;

        private int CurrentFrameAdvantage;

        /*public override void _Ready()
        {
            P1Meter = GetNode<TextureProgressBar>("Meters/P1Meter");
            P2Meter = GetNode<TextureProgressBar>("Meters/P2Meter");
            P1TrainingInfo = GetNode<Label>("TrainingInfo/P1Info/Information");
            P2TrainingInfo = GetNode<Label>("TrainingInfo/P2Info/Information");
        }*/

        public void Setup(SakugaActor[] fighters)
        {
            P1Meter.maxValue = fighters[0].Parameters.SuperGauge.MaxValue;
            P2Meter.maxValue = fighters[1].Parameters.SuperGauge.MaxValue;
        }

        public void UpdateMeters(SakugaActor[] fighters)
        {
            P1Meter.value = fighters[0].Parameters.SuperGauge.CurrentValue;
            P2Meter.value = fighters[1].Parameters.SuperGauge.CurrentValue;

            GetFrameAdvantage(fighters);

            if (P1InputHistory != null) P1InputHistory.SetHistoryList(fighters[0].Inputs);
            if (P2InputHistory != null) P2InputHistory.SetHistoryList(fighters[1].Inputs);

            P1TrainingInfo.text = TrainingInfoText(fighters[0], fighters[1]);
            P2TrainingInfo.text = TrainingInfoText(fighters[1], fighters[0]);
        }

        void GetFrameAdvantage(SakugaActor[] fighters)
        {
            for (int i = 0; i < fighters.Length; i++)
            {
                if (fighters[i].Parameters.Tracker.FrameAdvantage != 0)
                    CurrentFrameAdvantage = fighters[i].Parameters.Tracker.FrameAdvantage;
            }
        }

        private string TrainingInfoText(SakugaActor owner, SakugaActor reference)
        {
            string hitTypeText = "";

            switch (reference.Parameters.Tracker.LastHitType)
            {
                case 0:
                    hitTypeText = "HIGH";
                    break;
                case 1:
                    hitTypeText = "MID";
                    break;
                case 2:
                    hitTypeText = "LOW";
                    break;
                case 3:
                    hitTypeText = "UNBLOCKABLE";
                    break;
            }

            int refFrameAdv = CurrentFrameAdvantage;
            int ownFrameAdv = -CurrentFrameAdvantage;

            int finalFrameAdv = owner.Parameters.Tracker.FrameAdvantage != 0 ? ownFrameAdv : refFrameAdv;

            string frameAdvantageInfo = finalFrameAdv >= 0 ? 
                    ("+" + finalFrameAdv) : "" + finalFrameAdv;
            
            string frameAdvText = "("+frameAdvantageInfo+")";

            return reference.Parameters.Tracker.LastDamage + "\n" +
                    reference.Parameters.Tracker.CurrentCombo + "\n" +
                    reference.Parameters.Tracker.HighestCombo + "\n" +
                    hitTypeText + "\n" +
                    reference.Parameters.Prorations.CurrentDamageScaling + "%\n" +
                    owner.Parameters.Tracker.FrameData + frameAdvText;
        }
    }
}