using UnityEngine;
using System.Collections;

public class GunTester : MonoBehaviour {
	public Animation ani;
	
	
	void OnGUI()
	{
		string gunName = "beretta";
		//gunName = "m16";
		if(GUILayout.Button("Fire"))
		{
			ani.Play("fire-"+gunName);
		}
		if(GUILayout.Button("Reload"))
		{
			ani.Play("reload-"+gunName);
		}
		if(GUILayout.Button("Walk"))
		{
			ani.Play("walking-"+gunName);
		}
		if(GUILayout.Button("Run"))
		{
			ani.Play("run-"+gunName);
		}
		if(GUILayout.Button("Jump"))
		{
			ani.Play("sprint-"+gunName);
		}
		if(GUILayout.Button("Swap"))
		{
			ani.Play("change-"+gunName);
		}
		if(GUILayout.Button("Swap2"))
		{
			ani.Play("deactivate-"+gunName);
		}
		if(GUILayout.Button("Idle"))
		{
			ani.Play("idle-"+gunName);
		}
		if(GUILayout.Button("activate"))
		{
			ani.Play("activate-"+gunName);
		}
		if(GUILayout.Button("sprint"))
		{
			ani.Play("sprint-"+gunName);
		}
	}
}
