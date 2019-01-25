using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//UI状态枚举
public enum UIState
{
	Main=0,
	Settings=1,
	Gaming=2,
	Paused=3,
	Gameover=4,
	Loading=5
}

public class UI_Manager : MonoBehaviour {
	//指向实例对象的静态变量
	public static UI_Manager Instance;
	//所有子UI界面
	public List<GameObject> pages;
	//背景图
	public GameObject bg;
	//初始UI状态
	private UIState initState = UIState.Main;
	//当前状态
	public UIState currentState{get;set;}
	//是否锁定并隐藏光标
	public bool lockMouse;
	//摄像机
	public Camera cam;
	//点击按钮的音效
	public string sound_click;
	void Start()
	{
		//
		Instance = this;
		for(int i=0;i<pages.Count;i++)
		{
			if(i ==(int)initState)
				pages[i].SetActive(true);
			else
				pages[i].SetActive(false);
		}
		currentState = initState;
		//
		foreach (var item in pages) {
			SetResolution.SetAResolution(item.transform);
		}
	}
	//界面切换函数
	public void PageTransition(UIState newState)
	{
		if(newState == currentState)
			return;
		pages[(int)currentState].SetActive(false);
		pages[(int)newState].SetActive(true);
		currentState = newState;

		bg.SetActive( newState!= UIState.Gaming &&newState!= UIState.Paused && newState!= UIState.Gameover);
	}
	//清除函数
	public void Clean()
	{
		Destroy (gameObject);
	}
	void Update()
	{
		//更新光标
		if (lockMouse) {
			if (currentState == UIState.Gaming) {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			} else {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}
	}
	//点击按钮时播放音效
	public void Sound_Click()
	{
		AudioManager.PlayAudioUI(sound_click,1,false,null,Vector3.zero,0);
	}
}
