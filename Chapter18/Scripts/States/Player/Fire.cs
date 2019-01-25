using UnityEngine;
using System.Collections;

public class Fire :PlayerStateBase {
	public Fire (HumanBase _human):base(_human)
	{
		animtionName = "fire";
		loop = false;
		resetScope = false;
	}
	
	public override bool Check ()
	{
		return base.Check ();
	}
	public override bool Condition ()
	{
		return !player.gun.isAuto && Input.GetMouseButtonDown(0);
	}
	
	public override void Enter ()
	{
		base.Enter ();
		player.gun.SparkEffect(true);
		player.Fire();
	}
	
	public override void Update ()
	{
		base.Update ();

		#if mobile
		//移动设备通过按钮操作，设置bool值表明此状态是否能执行各个操作
		UI_Gaming.Instance.mobile_canFire = false;
		UI_Gaming.Instance.mobile_canReload = true;
		UI_Gaming.Instance.mobile_canSwap = true;
		UI_Gaming.Instance.mobile_canJump = true;
		#else
		//计算机设置通过键盘鼠标设备,这里直接调用其他状态的Check函数调用键盘鼠标的对应接口
		if(player.GetState(PlayerState.swap).Check())
		{
			Exit();
			return;
		}
		if(player.GetState(PlayerState.reload).Check())
		{
			Exit();
			return;
		}
		#endif

		Moving();
		Turning();
		Jumping();
	}

	public override void TimerEnd ()
	{
		base.TimerEnd ();
	}
	public override void Exit ()
	{
		base.Exit ();
		player.gun.SparkEffect(false);
		player.SetState(PlayerState.idle);
	}
}