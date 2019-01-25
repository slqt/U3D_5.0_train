using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum EnemyState
{
	patrol=0,
	chase=1,
	shoot=2,
	fall=3
}
public class Enemy :HumanBase {
	public List<Transform> patrolPoints;
	public Transform rayOrigin;
	public List<Transform> searchDirPoints;
	public NavMeshAgent agent{get;set;}
	public Player target{get;set;}
	public EnemyState enumState;
	public ParticleSystem sparkPS;
	void Awake()
	{
		Init();
	}
	void Update()
	{
		if(UI_Manager.Instance.currentState!= UIState.Gaming)
			return;
		state.Update();
//		Debug.Log("state:"+state);
	}

	public override void Init ()
	{
		base.Init ();
		agent = GetComponent<NavMeshAgent>();
		collider = GetComponent<Collider>();
		animation = GetComponent<Animation>();
		states.Add( new Patrol(this));
		states.Add( new Chase(this));
		states.Add( new Shoot(this));
		states.Add( new Fall(this));

		SetState(EnemyState.patrol);
	}
	
	public override void BeShot (int dmg)
	{
		base.BeShot (dmg);
	}
	
	public override void Die ()
	{
		base.Die ();
		SetState (EnemyState.fall);
	}

	public EnemyState stateBreforeHurt;
	public override void Hurt ()
	{
		base.Hurt ();
		PlayAnimation("HitTorsoFront");
		stateBreforeHurt = enumState;
		if(agent.enabled)
			agent.Stop();
		Invoke("AfterHurt",animation["HitTorsoFront"].length+0.05f);
	}
	void AfterHurt()
	{
		if(enumState == stateBreforeHurt)
		{
			SetState(stateBreforeHurt);
		}
	}
	
	public override void PlayAnimation (string aniName)
	{
		base.PlayAnimation (aniName);
	}

	public StateBase GetState(EnemyState state)
	{
		return states[(int)state];
	}
	
	public void SetState(EnemyState _state)
	{
		enumState = _state;
		state = states[(int)_state];
		state.Enter();
	}
}

