using UnityEngine;
using System.Collections;

public class UI_Main : MonoBehaviour {
	//点击开始游戏按钮
	public void OnPress_Start()
	{
		Debug.Log("OnPress_Start");
		UI_Manager.Instance.PageTransition( UIState.Loading);
	}
	//点击设置选项按钮
	public void OnPress_Settings()
	{
		UI_Manager.Instance.Sound_Click();
		Debug.Log("OnPress_Settings");
		UI_Manager.Instance.PageTransition( UIState.Settings);
	}
}
