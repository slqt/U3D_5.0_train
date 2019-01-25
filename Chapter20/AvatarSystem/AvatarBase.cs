using UnityEngine;
using System.Collections.Generic;

public delegate void ActionDelegate();

public class AvatarBase : GameBase {
	
	public float radius;
	public float height;
	public float fightDistance;
	public float speed;
	private int direction = 1;

	private Vector3 pos;

	public ActionDelegate UpdateHandle = null;
	public AvatarData targetData = null;

	public Sequence skillSequence = new Sequence();

	public override void Init ()
	{
		base.Init ();

		speed = 1.2f;
		radius = 0.5f;

		//init skill sequence

		OnMove();

	}

	public override void UpdateBehaviour ()
	{
		base.UpdateBehaviour ();

		if(UpdateHandle != null) {
		
			UpdateHandle();
		
		}

	}

	public void None() {
	
		UpdateHandle = null;
	
	}

	public void OnIdle() {

		UpdateHandle = IdleUpdate;

	}

	private void IdleUpdate() {
	}

	public void OnMove() {
	
		UpdateHandle = MoveUpdate;
	
	}

	private void MoveUpdate() {

		if(targetData == null || targetData.avatar == null) {
			
			targetData = AvatarManager.Instance.GetTarget(this);
			
		}else{

			if(Mathf.Abs(targetData.avatar.tran.position.x - tran.position.x)- 
			   (radius + targetData.avatar.radius) < fightDistance) {
				
				//Fight
				OnFight();
				
			}else{
			
				//Move

				if(targetData.avatar.tran.position.x > tran.position.x) direction = 1;
				else direction = -1;

				pos = tran.position;
				pos.x += Time.deltaTime * speed * direction;
				tran.position = pos;
			
			}
			
		}

	}

	public void OnFight() {

		//UpdateHandle = FightUpdate;

		//Fight
		skillSequence.Update();

	}

//	private void FightUpdate() {
//
//		//Fight
//		skillSequence.Update();
//
//	}

	public void OnDead() {
	
		UpdateHandle = DeadUpdate;

		Remove();
	
	}

	private void DeadUpdate() {
	

	
	}

}