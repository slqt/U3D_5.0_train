using UnityEngine;
using System.Collections;

public class C_10_1_1 : MonoBehaviour {
	void Start () {
		int num = 10;
		PlayerPrefs.SetInt("Number",num);
		PlayerPrefs.GetInt("Number");

		float PI = 3.14f;
		PlayerPrefs.SetFloat("PI",PI);
		PlayerPrefs.GetFloat("PI");

		string str = "Hello World!";
		PlayerPrefs.SetString("HW",str);
		str = PlayerPrefs.GetString("HW");

		PlayerPrefs.Save();
		PlayerPrefs.HasKey("HW");
		PlayerPrefs.DeleteKey("HW");
		PlayerPrefs.DeleteAll();
	}
}
