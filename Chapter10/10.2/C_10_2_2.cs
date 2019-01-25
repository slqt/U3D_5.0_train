using UnityEngine;
using System.Collections;
using JsonFx.Json;
using System.IO;

public class C_10_2_2 : MonoBehaviour {
	string path =  "/Chapter8/8.2/data.txt";
	void OnGUI () {
		if(GUILayout.Button("保存"))
		{
			Write();
		}
		if(GUILayout.Button("读取"))
		{
			Read();
		}
	}
	void Write()
	{
		Person john = new Person("John",19);
		string Json_Text = JsonWriter.Serialize(john);
		File.WriteAllText (GetDataPath () +path, Json_Text);
	}
	void Read()
	{
		string Json_Text = File.ReadAllText (GetDataPath () +path);
		Person john = JsonReader.Deserialize<Person>(Json_Text);
		Debug.Log(john.name +"'s age is "+john.age);
	}
	public static string GetDataPath ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			//iphone路径
			string path = Application.dataPath.Substring (0, Application.dataPath.Length - 5); 
			path = path.Substring (0, path.LastIndexOf ('/'));  
			return path + "/Documents";
		} else if (Application.platform == RuntimePlatform.Android) {
			//安卓路径
			return Application.persistentDataPath + "/";
		} else
		{
			//其他路径
			return Application.dataPath;
		}
	}
}

