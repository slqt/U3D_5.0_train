using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour {
	public float rotationSpeed = 3f;
	void Update () {
		//光圈自传
		transform.RotateAround(Vector3.up,rotationSpeed*Time.deltaTime);
	}
}
