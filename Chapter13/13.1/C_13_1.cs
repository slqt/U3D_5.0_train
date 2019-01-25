using UnityEngine;
using System.Collections;

public class C_13_1 : MonoBehaviour {
	public Material mat;//将要设置贴图的材质
	private string localAddress;//本地地址
	private string webAddress;//网络地址
	void Awake()
	{
		//地址
		localAddress = "file://"+Application.dataPath+"/Chapter13/13.1/13.1.Tex.jpg";
		webAddress = "http://p3.sinaimg.cn/1192309394/180/47331354702495";
	}

	void OnGUI()
	{
		if (GUILayout.Button ("Load")) {
			//使用协程下载
			//StartCoroutine(Load(localAddress));
			StartCoroutine(Load(webAddress));
		}
	}
	
	IEnumerator Load(string url)
	{
		Debug.Log (url);
		WWW www = new WWW (url);
		yield return www;
		mat.mainTexture = www.texture;
	}
}
