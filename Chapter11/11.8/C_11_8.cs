using UnityEngine;
using System.Collections;

public class C_11_8 : MonoBehaviour {
	public AudioSource gunshot;
	public void OnGUI()
	{
		//当按下开火键时播放开枪音效
		if (GUILayout.Button ("Fire")) {
			gunshot.Play();
		}
	}
}
