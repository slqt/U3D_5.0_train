using UnityEngine;
using System.Collections;

public class Dead :PlayerStateBase {

	public Dead (HumanBase _human):base(_human)
	{
		animtionName = "deactivate";
		loop = true;
		resetScope = true;

		audioClipName_animationSync = "die1";
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
		UI_Gaming.Instance.mobile_canFire = false;
		UI_Gaming.Instance.mobile_canReload = false;
		UI_Gaming.Instance.mobile_canSwap = false;
		UI_Gaming.Instance.mobile_canJump = false;
		#else
		#endif
	}
	
	public override void Exit ()
	{
		base.Exit ();
	}
}

