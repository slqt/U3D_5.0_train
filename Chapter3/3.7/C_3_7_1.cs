using UnityEngine;
using System.Collections;

public class C_3_7_1 : MonoBehaviour {

	void OnGUI()
	{
		GUILayout.Label("当前游戏时间" + Time.time);
		GUILayout.Label("游戏时间的缩放" + Time.timeScale);
		GUILayout.Label("上一帧所消耗的时间" + Time.deltaTime);
		GUILayout.Label("固定增量时间" + Time.fixedTime);
		GUILayout.Label("上一帧所消耗的固定时间" + Time.fixedDeltaTime);
		GUILayout.Label("真实逝去时间" + Time.realtimeSinceStartup);
	}
}
