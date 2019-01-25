using UnityEngine;
using System.Collections;

public class UI_Gameover : MonoBehaviour {	
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			UI_Manager.Instance.Clean();
			Application.LoadLevel("scene");
		}
	}
}
