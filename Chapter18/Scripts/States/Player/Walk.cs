using UnityEngine;
using System.Collections;

public class Walk :PlayerStateBase {
	
	public Walk (HumanBase _human):base(_human)
	{
		animtionName = "walking";
		loop = true;
		resetScope = false;

		audioClipName_repeat = "npc_step1";
		audioTimerRepeat_Max = 0.37f;
		audioVolume = 0.5f;
	}
	
	public override bool Check ()
	{
		return base.Check ();
	}
	public override bool Condition ()
	{
		#if mobile
		float moveX = PadManager.Instance.leftpad.GetAxis(PadAxis.Horizontal);
		float moveY =  PadManager.Instance.leftpad.GetAxis(PadAxis.Vertical);
		#else
		float moveX = Input.GetAxis("Horizontal");
		float moveY = Input.GetAxis("Vertical");
		#endif
		return (! player.GetState(PlayerState.run).Condition()) && (Mathf.Abs(moveX)>0.1f ||Mathf.Abs(moveY)>0.1f) ;
	}
	public override void Enter ()
	{
		base.Enter ();
	}
	
	public override void Update ()
	{
		base.Update ();

		#if mobile
		UI_Gaming.Instance.mobile_canFire = true;
		UI_Gaming.Instance.mobile_canReload = true;
		UI_Gaming.Instance.mobile_canSwap = true;
		UI_Gaming.Instance.mobile_canJump = true;
		#else
		if(player.GetState(PlayerState.fire).Check())
		{
			Exit();
			return;
		}
		if(player.GetState(PlayerState.fireAuto).Check())
		{
			Exit();
			return;
		}
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
		Jumping();
		#endif

		if(player.GetState(PlayerState.run).Check())
		{
			Exit();
			return;
		}
		if(!Condition())
		{
			Exit();
			player.SetState(PlayerState.idle);
		}

		Moving();
		Turning();
	}
	
	public override void Exit ()
	{
		base.Exit ();
	}
}

