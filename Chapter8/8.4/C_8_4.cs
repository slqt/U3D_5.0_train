using UnityEngine;
using System.Collections;

public class C_8_4 : MonoBehaviour {
	public Camera cam;
	public ParticleSystem ps;

	void Update()
	{
		ps.transform.position = GetInputPos2WorldPos();
	}

	//根据鼠标的位置计算出对应的世界坐标
	Vector3 GetInputPos2WorldPos()
	{
		Vector3 v = new Vector3(Input.mousePosition.x,Input.mousePosition.y,10);
		//核心接口,传入的Vector3中的x,y为鼠标屏幕位置,z为所要取的坐标与摄像机的距离
		Vector3 v2 = cam.ScreenToWorldPoint(v);
		Debug.Log(v + " __ "+v2);
		return v2;
	}
}
