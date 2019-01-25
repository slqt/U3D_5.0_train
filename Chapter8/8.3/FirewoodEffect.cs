using UnityEngine;
using System.Collections;

public class FirewoodEffect : MonoBehaviour {
	MeshRenderer renderer;

	void Start () {
		renderer = GetComponent<MeshRenderer>();
		Blink();
	}
	
	void Blink()
	{
		renderer.enabled = false;
		Invoke("BlinkEnd",Time.deltaTime*2);
	}

	void BlinkEnd()
	{
		renderer.enabled = true;
		Invoke("Blink",Time.deltaTime*2);
	}
}
