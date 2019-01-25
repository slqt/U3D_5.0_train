using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Patrol :EnemyStateBase {
	int id =0;
	public Patrol (HumanBase _human):base(_human)
	{
		animtionName = "Walk";
		audioClipName_repeat = "npc_step1";
		audioTimerRepeat_Max = 0.5f;
		audioVolume = 0.5f;
	}
	
	public override bool Check ()
	{
		return base.Check ();
	}
	public override bool Condition ()
	{
		return base.Condition();
	}
	
	public override void Enter ()
	{
		base.Enter ();
		SetPatrol();
		timer = timerMax;
	}
	
	public override void Update ()
	{
		base.Update();
		if(!enemy.agent.enabled)
		{
			if(timer>0)
			{
				timer -= Time.deltaTime;
			}
			else
			{
				TimerEnd();
			}
		}

		if(Vector3.Distance(enemy.transform.position,enemy.agent.destination)<0.1f)
		{
			enemy.animation.Play("Idle2");
			enemy.agent.Stop();
			enemy.agent.enabled = false;
		}
		if(enemy.GetState(EnemyState.chase).Check())
		{
			enemy.enumState = EnemyState.chase;
			Exit();
			return;
		}
	}
	
	public override void TimerEnd ()
	{
		SetDesination();
	}
	public override void Exit ()
	{
		base.Exit ();
	}

	void SetPatrol()
	{
		enemy.agent.enabled = true;
		enemy.agent.speed = 0.8f;
		enemy.agent.angularSpeed = 360;
		SetDesination();
	}

	
	void SetDesination()
	{
		enemy.agent.enabled = true;
		enemy.PlayAnimation(animtionName);                        
		enemy.agent.SetDestination(enemy.patrolPoints[id].position);
		NextPoint();
		timer = Random.Range(4f,5f);
	}
	
	void NextPoint()
	{
		id++;
		id%=enemy.patrolPoints.Count;
	}
}
