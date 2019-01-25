using UnityEngine;
using System.Collections;

public class C_11_6 : MonoBehaviour {
	private AudioSource audioSource;
	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void Update () {
		//鸟一直叫
		if (!audioSource.isPlaying)
			Play ();
		//鸟向右飞
		transform.position += Vector3.right * 1f * Time.deltaTime;
	}
	
	void Play()
	{
		//播放鸟的叫声
		audioSource.PlayDelayed (Random.Range (0.5f, 1.6f));
	}
}
