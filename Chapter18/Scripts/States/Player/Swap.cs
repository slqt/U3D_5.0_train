using UnityEngine;
using System.Collections;

public class Swap :PlayerStateBase {
	
	public Swap (HumanBase _human):base(_human)
	{
		animtionName = "deactivate";
		loop = false;
		resetScope = true;
	}
	
	public override bool Check ()
	{
		return base.Check ();
	}
	public override bool Condition ()
	{
		return Input.GetKeyDown(KeyCode.Q);
	}
	
	public override void Enter ()
	{
		base.Enter ();
	}
	
	public override void Update ()
	{
		base.Update ();

		#if mobile
		UI_Gaming.Instance.mobile_canFire = false;
		UI_Gaming.Instance.mobile_canReload = false;
		UI_Gaming.Instance.mobile_canSwap = false;
		UI_Gaming.Instance.mobile_canJump = false;
		#else
		#endif

		Moving();
		Turning();
	}

	public override void TimerEnd ()
	{
		base.TimerEnd ();
		player.SetState(PlayerState.idle);
		player.Swap();
	}
	
	public override void Exit ()
	{
		base.Exit ();
	}
}

