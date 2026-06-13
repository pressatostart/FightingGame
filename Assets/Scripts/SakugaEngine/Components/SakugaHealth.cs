using UnityEngine;
using SakugaEngine.Resources;

namespace SakugaEngine
{
    public partial class SakugaHealth : MonoBehaviour
    {
        [SerializeField] private bool UseDataContainer;
        public int MaxValue;
        [HideInInspector] public int CurrentValue;
        [HideInInspector] public int LostValue;
        [HideInInspector] public int RecoverableValue;

        public void Initialize(DataContainer data)
        {
            if (UseDataContainer)
                MaxValue = data.MaxHealth;
            
            CurrentValue = MaxValue;
            LostValue = CurrentValue;
        }

        public void Tick()
        {
            UpdateLostHealth();
        }

        public void SetHealth(int value)
        {
            CurrentValue = value;
            CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxValue);

            if (CurrentValue == 0 || CurrentValue == MaxValue)
                LostValue = CurrentValue;
        }

        public void AddHealth(int value)
        {
            CurrentValue += value;
            CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxValue);

            if (CurrentValue == 0 || CurrentValue == MaxValue)
                LostValue = CurrentValue;
        }

        public void RemoveHealth(int value)
        {
            CurrentValue -= value;
            CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxValue);

            if (CurrentValue == 0 || CurrentValue == MaxValue)
                LostValue = CurrentValue;
        }

        public void UpdateLostHealth()
        {
            if (LostValue > CurrentValue)
                LostValue -= MaxValue / 100;
            else if (LostValue < CurrentValue)
                LostValue = CurrentValue;
        }
    }
}
