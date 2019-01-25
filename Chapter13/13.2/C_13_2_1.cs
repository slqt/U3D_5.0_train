using UnityEngine;
using System.Collections;

public class C_13_2_1: MonoBehaviour {
	private string address;
	private WWWForm form;
	void Awake()
	{
		//地址
		address = "file://"+Application.dataPath+"/Chapter13/info.txt";
		//创建WWWForm并添加版本号等信息
		form = new WWWForm ();
		form.AddField ("version", 1);//游戏版本号
		form.AddField ("username", "John");//用户名
		form.AddField ("Device", Application.platform.ToString());//设备类型
		form.AddField ("Memory", SystemInfo.systemMemorySize);//设备内存大小
	}
	
	void OnGUI()
	{
		if (GUILayout.Button ("Load")) {
			//使用协程下载
			StartCoroutine(Load(address));
		}
	}
	
	IEnumerator Load(string url)
	{
		Debug.Log (url);
		//向url地址上传form并下载资料
		WWW www = new WWW (url,form);
		yield return www;
		string text = www.text;
		Debug.Log (text);
	}
}