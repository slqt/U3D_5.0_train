using UnityEngine;
using System.Collections;

public class SoundManager : SingletonUnity<SoundManager> {

	public AudioSource MusicAudio;
	public AudioSource SoundFxAudio;
	public AudioListener audioListener;

	public void Play(string strSound) {

		SoundFxAudio.PlayOneShot(SoundLoader.Instance.LoadSound(strSound));

	}

	public void PlayMusic(string strSound) {

		MusicAudio.clip = SoundLoader.Instance.LoadSound(strSound);
		MusicAudio.Play();

	}

	public void StopMusic() {}

	public void CloseSound() {
	}

	public void OpenSound() {
	}

	public void CloseMusic() {
	}

	public void OpenMusic() {
	}


}
