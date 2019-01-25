using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class ReverbTriggerZone : MonoBehaviour {
	public AudioMixer mixer; 
	public AudioMixerSnapshot snapshotNoReverb;
	public AudioMixerSnapshot snapshotReverb;
	void OnTriggerEnter(Collider other)
	{
		//当不是玩家进入时，不处理
		if (other.name != "Player")
			return;
		//开启混响
		//mixer.SetFloat ("MyExposedParam", 0);
		snapshotReverb.TransitionTo (0.1f);
	}

	void OnTriggerExit(Collider other)
	{
		//当不是玩家进入时，不处理
		if (other.name != "Player")
			return;
		//关闭混响
		//mixer.SetFloat ("MyExposedParam", -10000);
		snapshotNoReverb.TransitionTo (0.1f);
	}
}
