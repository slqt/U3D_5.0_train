using UnityEngine;
using System.Collections;

public class PlayerStateBase :StateBase {
	protected Player player;
	public PlayerStateBase (HumanBase _human):base(_human)
	{
		player = (Player)_human;
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
		player.state = this;
		//状态倒计时 = 动画时长/播放速度
		timerMax = player.animation.GetClip( animtionName+ "-"+player.gun.gunName).length / animationSpeed;
		base.Enter ();

		if(resetScope)
			player.SetScope(false);
	}

	public override void Update ()
	{
		base.Update ();

		if(!loop)
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

		#if mobile
		UI_Gaming.Instance.mobile_canScope = !resetScope;
		#else
		if(!resetScope)
			Scoping();
		#endif
	}

	public override void Exit ()
	{
		base.Exit ();
	}

	public override void Moving()
	{
		#if mobile
		float moveX = PadManager.Instance.leftpad.GetAxis(PadAxis.Horizontal);
		float moveY =  PadManager.Instance.leftpad.GetAxis(PadAxis.Vertical);
		#else
		float moveX = Input.GetAxis("Horizontal");
		float moveY = Input.GetAxis("Vertical");
		#endif

		if(Mathf.Abs(moveX)<0.1f &&Mathf.Abs(moveY)<0.1f)
			return;
		float pcgX = Mathf.Abs(moveX) /( Mathf.Abs(moveX)+Mathf.Abs(moveY));
		Vector3 movement =  
			player.transform.forward *(1-pcgX)* moveY * player.moveSpeed * moveFactor * Time.deltaTime
				+ player.transform.right * pcgX * moveX * player.moveSpeed * moveFactor * Time.deltaTime;
		player.rigidbody.MovePosition(player.rigidbody.position + movement);
	}
	public override void Turning()
	{
		float scoF = 1;
		if(player.scoping)
			scoF = 0.2f;
		#if mobile
		float asixX = PadManager.Instance.rightpad.movement.x * 0.01f;
		#else
		float asixX = Input.GetAxis ("Mouse X");
		#endif
		float eulerY = player.transform.eulerAngles.y+
			player.turnSpeed * turnFactor *Time.deltaTime*asixX * scoF;
		player.transform.eulerAngles = new Vector3(0,eulerY,0);

		#if mobile
		float asixY = -PadManager.Instance.rightpad.movement.y * 0.01f;
		#else
		float asixY = -Input.GetAxis ("Mouse Y");
		#endif

		float eulerX = player.turnRoot.localEulerAngles.x+
			player.turnSpeed * turnFactor *Time.deltaTime*asixY * scoF;
		if(eulerX<180)
		{
			if(eulerX>90)
				eulerX =90;
		}
		else
		{
			if(eulerX<270)
				eulerX = 270;
		}
		player.turnRoot.localEulerAngles = new Vector3(eulerX,0,0);
	}

	public override void Jumping()
	{
		if(Input.GetKeyDown(KeyCode.Space))
			Jump();
	}
	public override void Jump()
	{
		if(InAir())
			return;
		player.rigidbody.AddForce(Vector3.up * player.jumpForce * jumpFactor);
	}
	public override bool InAir ()
	{
		bool hitGround = false;
		//向下发射长度为2的射线判断是在空中还是地面
		foreach(var item in Physics.RaycastAll(player.transform.position,new Vector3(0,-1,0),2f))
		{
			if(item.collider.tag == "Terrain" || item.collider.tag == "Environment")
			{
				hitGround = true;
				break;
			}
		}
		return !hitGround;
	}

	public override void Scoping()
	{
		if(!player.gun.hasScope)
			return;
		//按下鼠标右键开启或关闭标准镜
		if(Input.GetMouseButtonDown(1))
			player.SetScope(!player.scoping);
	}
}
