using UnityEngine;
using System.Collections;

public class C_3_7_2 : MonoBehaviour {
	void OnGUI()
	{
		if(GUILayout.Button("生成随机数"))
		{
			//生成随机数
			int i = Random.Range(0,10);
			Debug.Log("随机生成的一个0~10之间的整数是:"+i);
			
			float f = Random.Range(0f,10f);
			Debug.Log("随机生成的一个0~10之间的浮点数是:"+f);
		}
	}
}
