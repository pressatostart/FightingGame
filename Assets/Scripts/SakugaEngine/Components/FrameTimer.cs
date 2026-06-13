using UnityEngine;

namespace SakugaEngine
{
    public partial class FrameTimer : MonoBehaviour
    {
        public bool Manual;
        public uint WaitTime = 0;
        [HideInInspector] public uint TimeLeft = 0;
        [HideInInspector] public bool Paused;

        public void StartTimer(uint startingTime = 0)
        {
            Paused = false;
            TimeLeft = startingTime == 0 ? WaitTime : startingTime;
        }

        public void Run() { if (TimeLeft > 0 && !Paused) TimeLeft--; }
        public void Pause() { Paused = true; }
        public void Resume() { Paused = false; }
        public void Stop() { TimeLeft =  0; Paused = false; }

        public bool IsRunning() => TimeLeft > 0;
        public bool IsCounting() => TimeLeft > 0 && !Paused;
        public bool IsPaused() => Paused;
    }
}
