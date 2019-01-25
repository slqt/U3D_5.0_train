using UnityEngine;
using System.Collections;

public class Reload :PlayerStateBase {
	
	public Reload (HumanBase _human):base(_human)
	{
		animtionName = "reload";
		loop = false;
		resetScope = true;
	}
	
	public override bool Check ()
	{
		return base.Check ();
	}
	public override bool Condition ()
	{
		#if mobile
		return false;
		#else
		return Input.GetKeyDown(KeyCode.R) && player.gun.ammo < player.gun.ammoMax;
		#endif
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
		player.gun.Reload();
		Exit();
	}

	public override void Exit ()
	{
		base.Exit ();
		player.SetState(PlayerState.idle);
	}
}

