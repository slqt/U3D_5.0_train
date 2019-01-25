using UnityEngine;
using System.Collections;

public class C_3_8_3_2 : MonoBehaviour {
	void OnGUI()
	{
		GUILayout.Label("X:"+ Input.acceleration.x);
		GUILayout.Label("Y:"+ Input.acceleration.y);
		GUILayout.Label("Z:"+ Input.acceleration.z);
	}
}
