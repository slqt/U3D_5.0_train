using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_13_3_5_3 : MonoBehaviour {
	public string address = "http://127.0.0.1/ABs/";
	public string objToLoad = "13.3.5.Sphere";

	private string manifestAssetBundleName;
	private AssetBundleManifest manifest;
	private List<AssetBundle> assetBundles = new List<AssetBundle>();

	void Awake()
	{
		//Manifest文件的名称与生成的AssetBundle所在的文件夹名称同名
		string[] ss = address.Split('/');
		manifestAssetBundleName = ss[ss.Length-2];
		Debug.Log("manifestAssetBundleName:"+manifestAssetBundleName);
	}
	void OnGUI()
	{
		//第1步：读取Manifest
		if (GUILayout.Button ("Load Manifest")) {
			StartCoroutine(LoadManifest());
		}
		//第2步：下载并得到objToLoad所依赖的资源
		if (GUILayout.Button ("Load Depend")) {
			LoadDepend(objToLoad);
		}
		//第3步：下载并实例化objToLoad
		if (GUILayout.Button ("Load Obj")) {
			StartCoroutine(LoadObj(address,objToLoad));
		}
		//清除缓存
		if (GUILayout.Button ("Clean")) {
			bool reslut = Caching.CleanCache();
			Debug.Log(reslut);
		}
	}
	//下载并读取Manifest文件
	IEnumerator LoadManifest()
	{
		WWW www = new WWW(address+manifestAssetBundleName);
		yield return www;
		Debug.Log(www.assetBundle.LoadAllAssets()[0].name);
		manifest = www.assetBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
		string[] abs = manifest.GetAllAssetBundles();
		foreach(string s in abs)
			Debug.Log(s);
		www.Dispose();
	}
	//获取资源所依赖的资源并下载
	void LoadDepend(string assetName)
	{
		string[] dps = manifest.GetAllDependencies(assetName);
		for (int i = 0; i < dps.Length; i++)
		{
			StartCoroutine(LoadAsset(address,dps[i]));
		}
	}
	//下载资源
	IEnumerator LoadAsset(string url,string fileName)
	{
		Debug.Log (url);
		WWW www = WWW.LoadFromCacheOrDownload(url+fileName,manifest.GetAssetBundleHash(fileName));
		yield return www;
		assetBundles.Add(www.assetBundle);
		www.Dispose();
	}
	//下载并实例化游戏对象
	IEnumerator LoadObj(string url,string fileName)
	{
		Debug.Log (url);
		WWW www = WWW.LoadFromCacheOrDownload(url+fileName,manifest.GetAssetBundleHash(fileName));
		yield return www;
		GameObject prefab = www.assetBundle.LoadAsset ("abCube") as GameObject;
		GameObject obj = GameObject.Instantiate (prefab);

		assetBundles.Add(www.assetBundle);
		www.Dispose();
		UnloadUnuseAssetBundle();
	}
	//清理未使用资源所占用的内存
	void UnloadUnuseAssetBundle()
	{
		foreach(var item in assetBundles)
		{
			item.Unload(false);
		}
	}
}