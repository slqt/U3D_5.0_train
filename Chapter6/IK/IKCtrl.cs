using UnityEngine;
using System;
using System.Collections;

//要求游戏对象必须有Animator组件
[RequireComponent(typeof(Animator))] 
public class IKCtrl : MonoBehaviour {
	private Animator animator;
	//IK的开关
	public bool iKPositionActive;
	public bool iKLookAtActive;
	public bool iKRotationActive;
	//SetIKPosition和SetLookAtPosition的对象
	public Transform rightHandObj;
	//SetIKRotation的对象
	public Transform rightHandRotationObj;

	void Start () 
	{
		animator = GetComponent<Animator>();
	}

	void OnAnimatorIK()
	{
		if(iKPositionActive)
		{
			//当开启IK位置时
			//设置右手的IK位置权重为1
			animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1.0f);
			//设置右手的IK位置为rightHandObj的位置
			animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
		}
		else
		{
			//当不开启IK位置时，重置IK位置的权重为0
			animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0f);
		}
		if(iKLookAtActive)
		{
			//当开启IK的LookAt时
			//设置IK的LookAt权重为1
			animator.SetLookAtWeight(1);
			//设置头部看向rightHandObj
			animator.SetLookAtPosition(rightHandObj.transform.position);
		}
		else
		{
			//当不开启IK的LookAt时，重置IK的LookAt的权重为0
			animator.SetLookAtWeight(0f);
		}
		if(iKRotationActive)
		{
			//当开启IK旋转时
			//设置右手的IK旋转权重为1
			animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1.0f);
			//设置右手的IK旋转角度为rightHandObj的旋转角度
			animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
		}
		else
		{
			//当不开启IK旋转时，重置IK旋转的权重为0
			animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0f);
		}
	}    
}
