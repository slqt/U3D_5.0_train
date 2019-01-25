using UnityEngine;
using System.Collections;

public class C_3_8_3_1 : MonoBehaviour {
	void OnGUI()
	{
		//遍历所有Touch
		foreach(Touch touch in Input.touches)
		{
			//输出Touch信息
			GUILayout.Label(string.Format("手指：{0} 状态：{1} 位置：{2}",touch.fingerId,touch.phase.ToString(),touch.position));
		}
	}
}
