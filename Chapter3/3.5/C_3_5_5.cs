using UnityEngine;
using System.Collections;

public class C_3_5_5 : MonoBehaviour {
	public GameObject prefab;
	void Start()
	{
		//克隆预制体
		GameObject obj = Instantiate(prefab) as GameObject;
		//设置游戏对象obj的位置
		obj.transform.position = new Vector3(0,3,0); 
	}
}
