using UnityEngine;
using System.Collections;

public class Chase :EnemyStateBase {
	public Chase (HumanBase _human):base(_human)
	{
		animtionName = "RunLoop";
		enterDistance = 25;
		leaveDistance = 30;

		audioClipName_repeat = "npc_step3";
		audioTimerRepeat_Max = 0.3f;
		audioVolume = 1f;
	}
	
	public override bool Check ()
	{
		return base.Check ();
	}
	public override bool Condition ()
	{
		bool condi = false;
		foreach(var p in enemy.searchDirPoints)
		{
			if(Common.ShootRay(enemy.rayOrigin.position,( p.position - enemy.rayOrigin.position).normalized,
			                   enterDistance,"Player",FoundPlayer))
			{
				condi = true;
				break;
			}
		}
		return condi;
	}

	void FoundPlayer(RaycastHit info)
	{
		enemy.target = info.collider.GetComponent<Player>();
	}
	
	public override void Enter ()
	{
		base.Enter ();
		Run_SetDestination();
		timer = timerMax;
	}
	
	public override void Update ()
	{
		base.Update();
		if(timer>0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			TimerEnd();
		}

		if(enemy.GetState(EnemyState.shoot).Check())
		{
			enemy.enumState = EnemyState.shoot;
			Exit();
			return;
		}

		if(Vector3.Distance(enemy.transform.position,enemy.target.transform.position)>leaveDistance)
		{
			enemy.SetState(EnemyState.patrol);
			Exit();
		}
	}
	
	public override void TimerEnd ()
	{
		Run_SetDestination();
	}
	public override void Exit ()
	{
		base.Exit ();
	}

	void Run_SetDestination()
	{
		//	Debug.Log (Time.time+"  Run_SetDestination");
		enemy.agent.enabled = true;
		enemy.agent.speed = 3f;
		enemy.agent.angularSpeed = 360;
		enemy.agent.enabled = true;
		enemy.agent.SetDestination(enemy.target.transform.position);
		timer = 1;
	}
}
