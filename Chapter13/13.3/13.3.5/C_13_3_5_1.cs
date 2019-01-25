using UnityEngine;
using System.Collections;

public class C_13_3_5_1 : MonoBehaviour {
	private string address = "http://127.0.0.1/ABs/";
	void OnGUI()
	{
		if (GUILayout.Button ("Load Base")) {
			StartCoroutine(LoadBase(address+"sphere_base"));
		}
		if (GUILayout.Button ("Load Obj")) {
			StartCoroutine(LoadObj(address+"sphere"));
		}
		if (GUILayout.Button ("Clean")) {
			Caching.CleanCache();
		}
	}
	IEnumerator LoadBase(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		www.assetBundle.LoadAllAssets();
		www.Dispose();
	}
	
	IEnumerator LoadObj(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		GameObject prefab = www.assetBundle.LoadAsset ("13.3.5.Sphere") as GameObject;
		GameObject obj = GameObject.Instantiate (prefab);
	}
}
