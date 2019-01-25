using UnityEngine;
using System.Collections;

public class ThirdPersonCam : MonoBehaviour
{
	//摄像机所跟随的对象
	public Transform follow;
	//摄像机在水平方向与对象的距离
	public float distanceAway;		
	//摄像机在垂直方向与对象的距离
	public float distanceUp;
	//过渡速度
	public float smooth;				
	//摄像机的目标速度
	private Vector3 targetPosition;		
	
	//在LateUpdate中执行摄像机操作，确保在对象的操作完成之后
	void LateUpdate ()
	{
		//计算目标位置
		targetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
		//对当前位置进行插值计算
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
		//使摄像机观察对象
		transform.LookAt(follow);
	}
}