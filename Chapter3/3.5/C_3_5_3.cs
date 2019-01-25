using UnityEngine;
using System.Collections;

public class C_3_5_3 : MonoBehaviour {
	public Texture texture;
	private GameObject obj;
	private Renderer render;

	void Start()
	{
		//获取游戏对象
		obj = GameObject.Find("Cube");
		//获取该对象的渲染器
		render = obj.GetComponent<Renderer>();
	}

	void OnGUI()
	{
		if(GUILayout.Button("添加颜色",GUILayout.Width(100), GUILayout.Height(50)))
		{
			//修改渲染颜色为红色
			render.material.color = Color.red;
		}
		if(GUILayout.Button("添加贴图",GUILayout.Width(100), GUILayout.Height(50)))
		{
			//添加组件贴图
			render.material.mainTexture = texture;
		}
	}
}
