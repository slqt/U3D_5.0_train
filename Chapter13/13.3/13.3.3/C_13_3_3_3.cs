using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_13_3_3_3: MonoBehaviour {
	public List<string> assetBundleNames;
	private int maxCount =4;
	private string address = "http://192.168.1.104/Unity/ABs/";
	private Dictionary<string,WWW> wwwDownloading = new Dictionary<string, WWW>();

	void Update()
	{
		foreach(var item in wwwDownloading)
		{
			if(item.Value.isDone)
			{
				item.Value.assetBundle.Unload(true);
				item.Value.Dispose();
				wwwDownloading.Remove(item.Key);
			}
		}

		foreach(string s in assetBundleNames)
		{
			if(wwwDownloading.Count<maxCount)
			{
				Load(s);
				assetBundleNames.Remove(s);
			}
		}
	}
	
	void Load(string abName)
	{
		WWW www = WWW.LoadFromCacheOrDownload(abName,0);
		wwwDownloading.Add(abName,www);
	}
}