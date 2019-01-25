using UnityEngine;
using System.Collections;

public class CamPos : MonoBehaviour {
	public float back = 3f;
	public float height = 1f;
	public Transform player;
	public float moveFac;
	Vector3 targetPos;
	void LateUpdate()
	{
		//设置摄像机的目标位置在主角的后上方
		targetPos = player.transform.position +
			-back * player.forward + new Vector3 (0f, height, 0f);
		//摄像机位置逐渐移动至目标位置
		transform.position = new Vector3 (Mathf.Lerp (transform.position.x, targetPos.x, 11),
		                                 Mathf.Lerp (transform.position.y, targetPos.y, 5),
		                                 Mathf.Lerp (transform.position.z, targetPos.z, 11));
		//摄像机观察主角
		transform.LookAt (player);
	}
}
