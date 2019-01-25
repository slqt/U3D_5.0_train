using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CodeExample : MonoBehaviour {
	public List<string> playerNames; 
	void Update()
	{
		for(int i=0;i<playerNames.Count;i++)
		{
			Debug.Log("player:"+playerNames[i]);
		}
	}
}
