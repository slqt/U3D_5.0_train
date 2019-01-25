using UnityEngine;
using System.Collections;

public class C_10_1_5 : MonoBehaviour {
	public string Name 
	{
		get 
		{ 
			return PlayerPrefs.GetString("Name"); 
		}
		set 
		{
			PlayerPrefs.SetString("Name",value);
			PlayerPrefs.Save();
		}
	}
	
	void Start()
	{
		Name = "Hi";
		Debug.Log(Name);
	}
}
