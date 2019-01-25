using UnityEngine;
using System.Collections;
using JsonFx.Json;

public class C_10_2_1 : MonoBehaviour {
	void Start () {
		Person john = new Person("John",19);
		//将对象序列化成JSON字符串
		string Json_Text = JsonWriter.Serialize(john);
		Debug.Log(Json_Text);
		//将JSON字符串反序列化成对象
		john = JsonReader.Deserialize(Json_Text) as Person;
	}
}
