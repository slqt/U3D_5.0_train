using UnityEngine;
using System.Collections;

public class EnemyTester : MonoBehaviour {
	public Animation ani;
	
	void OnGUI()
	{
		if(GUILayout.Button("Walk"))
		{
			ani.Play("Walk");
		}
		if(GUILayout.Button("Run"))
		{
			ani.Play("RunLoop");
		}
		if(GUILayout.Button("Fire"))
		{
			ani.Play("Firing");
		}
		if(GUILayout.Button("Idle"))
		{
			ani.Play("Idle1");
		}
		if(GUILayout.Button("Die"))
		{
			ani.Play("DieEasy");
		}
		if(GUILayout.Button("Hit"))
		{
			ani.Play("HitTorsoFront");
		}
	}
}
