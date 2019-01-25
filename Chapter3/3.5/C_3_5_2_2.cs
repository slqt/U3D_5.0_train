using UnityEngine;
using System.Collections;

public class C_3_5_2_2 : MonoBehaviour {
	private GameObject obj;
	void Start()
	{
		//寻找整个场景中名为Cube的游戏对象并赋予obj变量
		obj = GameObject.Find("Cube");
	}
}
