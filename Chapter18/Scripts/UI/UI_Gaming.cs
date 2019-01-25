using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_Gaming : MonoBehaviour {
	public static UI_Gaming Instance;
	//十字准心图片
	public Image cross; 
	//瞄准镜内视图片
	public Image scope;
	//受伤动画
	public Animator hurtAni; 
	//显示生命值的Text
	public Text text_life;
	//显示弹药情况的Text
	public Text text_ammo;
	//移动设备特有按钮的根节点
	public GameObject mobile_Root;
	//开火按钮
	public Button button_fire;
	//用bool值判断点击按钮是否有效，例如换子弹的时候是不能跳跃的
	public bool mobile_canFire{get;set;}
	public bool mobile_canReload{get;set;}
	public bool mobile_canScope{get;set;}
	public bool mobile_canSwap{get;set;}
	public bool mobile_canJump{get;set;}
	void Awake()
	{
		Instance = this;

		#if mobile
			mobile_Root.SetActive(true);
			//设置事件
			EventTriggerListener.Get(button_fire.gameObject).onDown = Event_FireBt_Press;
			EventTriggerListener.Get(button_fire.gameObject).onUp = Event_FireBt_Release;
		#else
			mobile_Root.SetActive(false);
		#endif
	}

	void Update () {
		if(Player.Instance ==null)
			return;
		//例：生命：100
		text_life.text = "生命："+ Player.Instance.hp;
		//例：30 | 90
		text_ammo.text = Player.Instance.gun.ammo+ " | "+ Player.Instance.gun.clip;
		//按下ESC键暂停/退出暂停
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}
	//设置准心及瞄准镜
	public void SetScope(bool vi)
	{
		cross.gameObject.SetActive(!vi);
		scope.gameObject.SetActive(vi);
	}
	//暂停
	void Pause()
	{
		Time.timeScale = 0;
		UI_Manager.Instance.PageTransition(UIState.Paused);
	}
	//受伤
	public void Hurt()
	{
		hurtAni.gameObject.SetActive(false);
		hurtAni.gameObject.SetActive(true);
	}

	#region mobile
	//点击暂停按钮
	public void OnPress_Pause()
	{
		Pause();
	}
	//按下射击按钮
	public void Event_FireBt_Press(GameObject go)
	{
		if(!mobile_canFire)
			return;
		if(Player.Instance.gun.isAuto)
		{
			Player.Instance.SetState(PlayerState.fireAuto,true);
		}
		else
		{
			Player.Instance.SetState(PlayerState.fire,true);
		}
	}
	//松开射击按钮
	public void Event_FireBt_Release(GameObject go)
	{
		if(Player.Instance.enumState == PlayerState.fireAuto)
		{
			Player.Instance.state.loop = false;
		}
	}
	//点击换弹夹按钮
	public void OnPress_Reload()
	{
		if(!mobile_canReload)
			return;

		if(Player.Instance.gun.ammo < Player.Instance.gun.ammoMax)
			Player.Instance.SetState(PlayerState.reload,true);
	}
	//点击瞄准按钮
	public void OnPress_Scope()
	{
		if(!mobile_canScope)
			return;
		if(!Player.Instance.gun.hasScope)
			return;
		Player.Instance.SetScope(!Player.Instance.scoping);
	}
	//点击换枪按钮
	public void OnPress_Swap()
	{
		if(!mobile_canSwap)
			return;
		Player.Instance.SetState(PlayerState.swap,true);
	}
	//点击跳跃按钮
	public void OnPress_Jump()
	{
		if(!mobile_canJump)
			return;
		Player.Instance.state.Jump();
	}
	#endregion
}
