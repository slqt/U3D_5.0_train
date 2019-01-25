using UnityEngine;
using System.Collections;

public class C_3_5_1 : MonoBehaviour {

	void OnGUI()
	{
		if(GUILayout.Button("创建立方体",GUILayout.Height(50)))
		{
			//设置该模型默认为立方体
			GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			//为对象添加一个刚体，赋予物理属性
			obj.AddComponent<Rigidbody>();
			//赋予对象的材质红色
			obj.GetComponent<Renderer>().material.color = Color.red;
			//设置对象的名称
			obj.name = "Cube";
			//设置此模型材质的位置坐标
			obj.transform.position = new Vector3(0,5f,0);
		}
		if(GUILayout.Button("创建球体",GUILayout.Height(50)))
		{
			//设置该模型默认为立方体
			GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			//为对象添加一个刚体，赋予物理属性
			obj.AddComponent<Rigidbody>();
			//赋予对象的材质红色
			obj.GetComponent<Renderer>().material.color = Color.green;
			//设置对象的名称
			obj.name = "Sphere";
			//设置此模型材质的位置坐标
			obj.transform.position = new Vector3(0,5f,0);
		}
	}
}
