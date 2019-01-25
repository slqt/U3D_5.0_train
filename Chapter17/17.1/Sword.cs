using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {
	public Transform bot;
	public float timeMax =0.2f;
	public float speed =900f;
	private float time=0;
	private Vector3 startPosition;
	private Vector3 startAngles;
	void Awake()
	{
		startPosition = transform.position;
		startAngles = transform.eulerAngles;
	}
	void OnGUI()
	{
		//按下按钮开始挥剑
		if (GUILayout.Button ("Swing")) {
			time=timeMax;
			transform.position = startPosition;
			transform.eulerAngles = startAngles;
		}
	}
	void Update()
	{
		if (time > 0) {
			time -= Time.deltaTime;	
			Swing();
		}
	}
	//更新剑的旋转
	void Swing()
	{
		transform.RotateAround(bot.position,Vector3.forward,-speed*Time.deltaTime);
	}
}
