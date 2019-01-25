using UnityEngine;
using System.Collections;

public class C_13_3_3_1: MonoBehaviour {
	public bool cache = false;
	private string address = "http://192.168.1.104/Unity/ABs/cube";
	
	void OnGUI()
	{
		if (GUILayout.Button ("Load")) {
			StartCoroutine(Load(address));
		}
	}
	
	IEnumerator Load(string url)
	{
		Debug.Log (url);
		WWW www = null;
		if(!cache)
		{
			//普通下载
			www = new WWW (url);
		}
		else
		{
			//下载至缓存
			www = WWW.LoadFromCacheOrDownload(url,0);
		}
		yield return www;
		//读取assetbundle中名为"13.3.1.Cube"的资源并解析为游戏对象
		GameObject prefab = www.assetBundle.LoadAsset ("13.3.1.Cube") as GameObject;
		GameObject obj = GameObject.Instantiate (prefab);
		www.Dispose();
	}
}