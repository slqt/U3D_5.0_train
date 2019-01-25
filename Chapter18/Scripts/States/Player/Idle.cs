using UnityEngine;
using System.Collections;

public class Idle :PlayerStateBase {
	public Idle (HumanBase _human):base(_human)
	{
		animtionName = "idle";
		loop = true;
		resetScope = false;
	}
	
	public override bool Check ()
	{
		return base.Check ();
	}

	public override bool Condition ()
	{
		return base.Condition ();
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

		if(player.GetState(PlayerState.walk).Check())
		{
			Exit();
			return;
		}
		if(player.GetState(PlayerState.run).Check())
		{
			Exit();
			return;
		}
		Turning();
	}

	public override void Exit ()
	{
		base.Exit ();
	}
}

