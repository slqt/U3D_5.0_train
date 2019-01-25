using UnityEngine;
using System.Collections;

public class C_7_4 : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		Debug.Log(Time.time+":进入触发器的对象是 "+other.gameObject.name);
	}

	void OnTriggerStay(Collider other) {
		Debug.Log(Time.time+":留在触发器的对象是 "+other.gameObject.name);
	}

	void OnTriggerExit(Collider other) {
		Debug.Log(Time.time+":离开触发器的对象是 "+other.gameObject.name);
	}
}
