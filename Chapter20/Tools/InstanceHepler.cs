using UnityEngine;
using System.Collections;

public class InstanceHepler : MonoBehaviour {

	private static int instance = 0;

	public static int Get() {
	
		return instance++;
	
	}

}
