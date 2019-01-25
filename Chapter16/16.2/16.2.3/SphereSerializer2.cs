using UnityEngine;
using System.Collections;

public class SphereSerializer2 : MonoBehaviour {
	private Rigidbody rigidbody;
	private Renderer renderer;
	private NetworkView networkView;
	
	bool firstSerialize = true;
	void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
		renderer = GetComponent<Renderer>();
		
		networkView = GetComponent<NetworkView>();
		networkView.observed = this;
	}
	//同步数据
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		if(firstSerialize)
		{
			firstSerialize = false;
			Vector3 color = Vector3.zero;
			if (stream.isWriting) {
				if(networkView.isMine)
					color = new Vector3(1,0,0);
				else
					color = new Vector3(0,1,0);
				stream.Serialize(ref color);
			} else {
				stream.Serialize(ref color);
			}
			renderer.material.color = new Color(color.x,color.y,color.z);
		}

		Vector3 pos= Vector3.zero;
		Quaternion rotation= Quaternion.identity;
		Vector3 velocity = Vector3.zero;

		if (stream.isWriting) {
			pos = rigidbody.position;
			stream.Serialize(ref pos);

			rotation = rigidbody.rotation;
			stream.Serialize(ref rotation);

			velocity = rigidbody.velocity; 
			stream.Serialize(ref velocity);
		} else {
			stream.Serialize(ref pos);
			transform.position = pos;

			stream.Serialize(ref rotation);
			transform.rotation = rotation;

			stream.Serialize(ref velocity);
			rigidbody.velocity = velocity;
		}
	}
}