using UnityEngine;
using System.Collections;

public class EnemyStateBase :StateBase {
	protected Enemy enemy;
	public float enterDistance;
	public float leaveDistance;
	public EnemyStateBase (HumanBase _human):base(_human)
	{
		enemy = (Enemy)_human;
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
		base.Enter();
		enemy.state = this;
		CleanPreState();
	}
	
	public override void Update ()
	{
		base.Update ();
	}
	
	public override void Exit ()
	{
		base.Exit ();
	}

	void CleanPreState()
	{
		if(enemy.agent.enabled)
			enemy.agent.Stop();
		enemy.agent.enabled = false;
	}
}
