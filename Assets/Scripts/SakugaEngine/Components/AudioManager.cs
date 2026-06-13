using UnityEngine;
using SakugaEngine.Resources;

namespace SakugaEngine
{
	public partial class AudioManager : MonoBehaviour
	{
		[SerializeField] private AudioSource Announcer;
		[SerializeField] private AudioSource Menu;
		[SerializeField] private AudioSource BGM;
		[SerializeField] private SoundsList AnnouncerClips;
		[SerializeField] private SoundsList MenuClips;

		public static AudioManager Instance { get; private set; }
		public void Awake()
		{
			Instance = this;
		}

		public void PlayAnnouncerClip(int soundIndex)
        {
			if (AnnouncerClips == null) return;
			if (soundIndex < 0) return;
            Announcer.clip = AnnouncerClips.Sounds[soundIndex];
            Announcer.Play();
        }

		public void PlayMenuClip(int soundIndex)
        {
			if (MenuClips == null) return;
			if (soundIndex < 0) return;
            Menu.clip = MenuClips.Sounds[soundIndex];
            Menu.Play();
        }

		public void PlayBGM(AudioClip song)
		{
			if (song == null) return;
			if (BGM.isPlaying) StopBGM();

			BGM.clip = song;
            BGM.Play();
		}

		public void StopBGM()
		{
            BGM.Stop();
		}
	}
}
