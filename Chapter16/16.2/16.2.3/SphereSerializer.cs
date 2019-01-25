using UnityEngine;
using System.Collections;

public class SphereSerializer : MonoBehaviour {
	private Rigidbody rigidbody;
	private NetworkView networkView;
	void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
		networkView = GetComponent<NetworkView>();
		//设置networkView的监听对象为刚体
		networkView.observed = rigidbody;
	}
}
