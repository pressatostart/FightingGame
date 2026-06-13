using UnityEngine;
using SakugaEngine.Resources;

namespace SakugaEngine
{
    public partial class SakugaSuperGauge : MonoBehaviour
    {
        [SerializeField] private bool UseDataContainer;
        [SerializeField] private bool StartFull = false;
        [SerializeField] private bool CanReset = true;
        public int MaxValue;
        [HideInInspector] public int CurrentValue;
        

        public void Initialize(DataContainer data)
        {
            if (UseDataContainer)
            {
                MaxValue = data.MaxSuperGauge;
            }
            
            CurrentValue = StartFull ? MaxValue : 0;
        }

        public void Reset()
        {
            if (!CanReset) return;

            CurrentValue = StartFull ? MaxValue : 0;
        }

        public void Tick()
        {
            if (CurrentValue > MaxValue) CurrentValue = MaxValue;
        }

        public void SetSuperGauge(int value)
        {
            CurrentValue = value;
            CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxValue);
        }

        public void AddSuperGauge(int value)
        {
            CurrentValue += value;
            CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxValue);
        }

        public void SpendSuperGauge(int value)
        {
            CurrentValue -= value;
            CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxValue);
        }
    }
}
