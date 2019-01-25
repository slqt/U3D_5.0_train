using UnityEngine;
using System.Collections;

public delegate void LoadDelegate();

public class ChangeScene : MonoBehaviour {

	public static LoadDelegate loadHandler = null;

	// Use this for initialization
	void Start () {

		if(loadHandler != null) {
		
			loadHandler();
			loadHandler = null;
		
		}
	
	}

}
