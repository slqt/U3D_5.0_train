using UnityEngine;
using System.Collections;

public class C_3_7_4 : MonoBehaviour {
	//绕y轴自转的速度
	float rotateSpeed = 50f;
	void Update()
	{
		//绕y轴自转
		transform.rotation =Quaternion.Euler(0f,rotateSpeed*Time.time,0);
	}
}
