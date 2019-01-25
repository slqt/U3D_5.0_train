using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	void OnCollisionEnter(Collision collision ) 
	{
		if (!Network.isServer)
			return;
		if(collision.gameObject.tag != "Player")
			return;
		
		Vector3 dir = transform.position - collision.transform.position;
		dir.Normalize();

		float force = C_16_3_2.force2Ball;
		force = Mathf.Clamp(force,0f,50f);
		print("force "+force);

		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity= Vector3.zero;
		GetComponent<Rigidbody>().AddForce (dir*force);
	}
}
