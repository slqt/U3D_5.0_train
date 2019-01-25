using UnityEngine;
using System.Collections;

public class Fall :EnemyStateBase {
	public Fall (HumanBase _human):base(_human)
	{
		animtionName = "DieRoughShort";
		loop = false;

		audioClipName_animationSync = "die2";
	}
	
	public override bool Check ()
	{
		return base.Check ();
	}
	public override bool Condition ()
	{
		return false;
	}
	
	public override void Enter ()
	{
		timerMax = 3f;
		base.Enter ();
	}
	
	public override void Update ()
	{
		base.Update ();

		Debug.Log("Fall timer:"+timer);
	}
	
	public override void TimerEnd ()
	{
		GameObject.Destroy (enemy.gameObject);
	}
	public override void Exit ()
	{
		base.Exit ();
	}
}
