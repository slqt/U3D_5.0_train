using UnityEngine;
using System.Collections;

public class C_10_1_4 : MonoBehaviour {
	private string name;
	public string Name 
	{
		get 
		{ 
			return name; 
		}
		set 
		{
			name = value; 
		}
	}

	void Start()
	{
		Name = "Hi";
		Debug.Log(Name);
	}
}
