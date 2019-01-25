using UnityEngine;
using System.Collections;

public class MyAgent : MonoBehaviour {
	public GameObject destinationTarget;
	public NavMeshAgent agent;
	void Start () {
		//设置导航代理的目标位置
		agent.destination = destinationTarget.transform.position;
	}
}