using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AudioManager{

	public static void PlaySound(string _name,float volume, bool is3D,Transform parent,Vector3 pos,float delay)
	{
		if(string.IsNullOrEmpty(_name))
			return;
		PlayAudio("Audio/Sound/"+_name,volume,is3D,parent,pos,delay);
	}
	public static void PlayAudioUI(string _name,float volume, bool is3D,Transform parent,Vector3 pos,float delay)
	{
		if(string.IsNullOrEmpty(_name))
			return;
		PlayAudio("Audio/UI/"+_name,volume,is3D,parent,pos,delay);
	}

	private static void PlayAudio(string path,float volume, bool is3D,Transform parent,Vector3 pos,float delay)
	{
		AudioClip clip = Resources.Load<AudioClip>(path);
		GameObject obj = new GameObject(path+":"+Time.time);
		obj.transform.parent = parent;
		obj.transform.localPosition = pos;
		AudioSource source = obj.AddComponent<AudioSource>();
		source.clip = clip;
		source.spatialBlend = is3D ? 1:0;
		source.playOnAwake = false;
		if(delay>0)
			source.PlayDelayed(delay);
		else
			source.Play();
		GameObject.Destroy(obj,delay+clip.length);
	}
}
