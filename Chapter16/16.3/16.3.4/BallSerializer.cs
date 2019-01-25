using UnityEngine;
using System.Collections;

public class BallSerializer : MonoBehaviour {
	private Rigidbody rigidbody;
	private Renderer renderer;
	private NetworkView networkView;

	bool firstSerialize = true;
	void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
		renderer = GetComponent<Renderer>();

		networkView = GetComponent<NetworkView>();
		networkView.observed = rigidbody;
	}
//	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
//		if(firstSerialize)
//		{
//			firstSerialize = false;
//			Vector3 color = Vector3.zero;
//			if (stream.isWriting) {
//				if(Network.isServer)
//					color = new Vector3(1,0,0);
//				else
//					color = new Vector3(0,1,0);
//				stream.Serialize(ref color);
//			} else {
//				stream.Serialize(ref color);
//			}
//			renderer.material.color = new Color(color.x,color.y,color.z);
//		}
//
//		Vector3 pos= Vector3.zero;
//		Quaternion qua= Quaternion.identity;
//		if (stream.isWriting) {
//			pos = transform.position;
//			stream.Serialize(ref pos);
//
//			qua = transform.rotation;
//			stream.Serialize(ref qua);
//		} else {
//			stream.Serialize(ref pos);
//			transform.position = pos;
//
//			stream.Serialize(ref qua);
//			transform.rotation = qua;
//		}
//	}
}
