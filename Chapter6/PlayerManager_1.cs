using UnityEngine;
using System.Collections;

public class PlayerManager_1 : MonoBehaviour {
	private Animator animator;
	void Awake()
	{
		animator = GetComponent<Animator>();
	}
	void Update () 
	{
		//得到Joystick水平轴向输入的值
		float h = Input.GetAxis("Horizontal");	
		//将该值传递至animator的Direction参数
		animator.SetFloat("Direction", h, 0.25f, Time.deltaTime);	
	}
}