using UnityEngine;
using System.Collections;

public class Shoot :EnemyStateBase {
	public Shoot (HumanBase _human):base(_human)
	{
		animtionName = "Firing";
		enterDistance = 10;
		leaveDistance = 15;

		audioClipName_animationSync = "m4a1_unsil-1";
	}
	
	public override bool Check ()
	{
		return base.Check ();
	}
	public override bool Condition ()
	{
		return enemy.enumState == EnemyState.chase &&
			Vector3.Distance(enemy.transform.position,enemy.target.transform.position)<enterDistance;
	}
	
	public override void Enter ()
	{
		base.Enter ();
		timerMax = 3;
		ShootA();
	}
	
	public override void Update ()
	{
		base.Update ();

		if(timer>0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			TimerEnd();
		}

		Vector3 dir = (enemy.target.transform.position - enemy.transform.position);
		dir = new Vector3(dir.x,0,dir.z);
		dir.Normalize();
		enemy.transform.forward = dir;

		if(Vector3.Distance(enemy.transform.position,enemy.target.transform.position)>leaveDistance)
		{
			enemy.SetState(EnemyState.chase);
			Exit();
		}
	}
	
	public override void TimerEnd ()
	{
		ShootA();
	}
	public override void Exit ()
	{
		base.Exit ();
	}


	void ShootA()
	{
		ShootSetAnimation();
		Common.ShootRay(enemy.rayOrigin.position,enemy.transform.forward,30,"Player",Shot);
		enemy.PlayAnimation(animtionName);
		timer = timerMax;
		enemy.sparkPS.Play();
	}
	void Shot(RaycastHit info)
	{
		Debug.Log("Shot");
		Player p = info.collider.GetComponent<Player>();
		p.BeShot(enemy.power);
	}

	void ShootSetAnimation()
	{
		float angle = Vector3.Angle(enemy.transform.position,enemy.target.transform.position);
		Debug.Log("angle:"+angle);
	}
}

