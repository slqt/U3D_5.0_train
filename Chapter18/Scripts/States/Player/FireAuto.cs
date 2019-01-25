using UnityEngine;
using System.Collections;

public class FireAuto :PlayerStateBase {
	public FireAuto (HumanBase _human):base(_human)
	{
		animationSpeed = 5;
		animtionName = "fire";
		loop = true;
		resetScope = true;
	}
	
	public override bool Check ()
	{
		return base.Check ();
	}
	public override bool Condition ()
	{
		return player.gun.isAuto && Input.GetMouseButtonDown(0);
	}
	
	public override void Enter ()
	{
		base.Enter ();
		loop = true;
		timer = timerMax;
		player.gun.SparkEffect(true);

		//可能因为没子弹而换弹匣换枪退出开火状态，所以需要在进入状态的最后一行代码
		player.Fire();
	}
	
	public override void Update ()
	{
		#if !mobile
		if(Input.GetMouseButtonUp(0))
		{
			loop = false;
		}
		#endif

		base.Update ();
		if(timer>0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			if(loop)
				FireOnce();
			else
			{
				Exit();
				player.SetState(PlayerState.idle);
			}
		}

		#if mobile
		UI_Gaming.Instance.mobile_canFire = false;
		UI_Gaming.Instance.mobile_canReload = true;
		UI_Gaming.Instance.mobile_canSwap = true;
		UI_Gaming.Instance.mobile_canJump = true;
		#else
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

		Moving();
		Turning();
	}
	
	public override void Exit ()
	{
		base.Exit ();
		player.gun.SparkEffect(false);
	}

	void FireOnce()
	{
		player.PlayAnimation(animtionName);
		player.Fire();
		timer = timerMax;
	}
}

