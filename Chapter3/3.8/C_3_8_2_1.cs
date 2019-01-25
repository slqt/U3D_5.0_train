using UnityEngine;
using System.Collections;

public class C_3_8_2_1 : MonoBehaviour {
	void Update()
	{
		//按下Fire1键
		if(Input.GetButtonDown("Fire1"))
		{
			//...
		}
		//按住Fire1键
		if(Input.GetButton("Fire1"))
		{
			//...
		}
		//松开Fire1键
		if(Input.GetButtonUp("Fire1"))
		{
			//...
		}
	}
}
