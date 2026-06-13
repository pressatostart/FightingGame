using UnityEngine;

namespace SakugaEngine
{
    public partial class SoundQueue : MonoBehaviour
    {
        private AudioSource Source;
        [SerializeField] private ushort QueueDelay = 12;
        [SerializeField] private AudioClip DefaultSound;
        private bool Queued;
        private ushort QueueTimer = 0;
        private AudioClip BufferedSound;

        public void FixedUpdate()
        {
            TickQueueTimer();
            PlayQueue();
        }

        public void SimpleQueueSound()
        {
            if (Queued) return;
            
            BufferedSound = DefaultSound;
            Queued = true;
        }

        public void QueueSound(AudioClip sound)
        {
            if (Queued && Source.clip == sound) return;

            BufferedSound = sound;
            QueueTimer = 0;
            Queued = true;
        }

        public void PlayQueue()
        {
            if (!Queued) return;
            if (BufferedSound == null) return;
            if (QueueTimer > 0) return;

            Source.clip = BufferedSound;
            
            Source.Play();
            QueueTimer = QueueDelay;
            Queued = false;
            BufferedSound = null;
        }

        private void TickQueueTimer()
        {
            if (QueueTimer == 0) return;

            QueueTimer--;
        }
    }
}
