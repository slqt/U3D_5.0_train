using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//主角的状态枚举
public enum PlayerState
{
	idle=0,	//空闲
	walk=1,//行走
	run=2, //奔跑
	fire=3,//单发射击
	fireAuto=4,//自动射击
	reload=5,//换弹匣
	swap=6,//换枪
	dead=7//死亡
}
public class Player :HumanBase {
	public static Player Instance;
	//下一支枪的ID，用于换枪
	int NextGunID
	{
		get
		{
			return (gunId+1)%guns.Count;
		}
	}
	public List<Gun> guns = new List<Gun>();
	public int gunId{get;set;}
	public Gun gun{get;set;}
	public Camera cam;
	public Rigidbody rigidbody{get;set;}
	public bool scoping{get;set;}
	public Transform turnRoot;
	public PlayerState enumState;
	void Awake()
	{
		Init();
	}
	void Update()
	{
		if(UI_Manager.Instance.currentState!= UIState.Gaming)
			return;
		state.Update();
	}
	#region override
	public override void Init ()
	{
		Instance = this;
		base.Init ();
		rigidbody = GetComponent<Rigidbody>();
		//将所有状态加入链表
		states.Add( new Idle(this));
		states.Add( new Walk(this));
		states.Add( new Run(this));
		states.Add( new Fire(this));
		states.Add( new FireAuto(this));
		states.Add( new Reload(this));
		states.Add( new Swap(this));
		states.Add( new Dead(this));
		//初始化所有枪支
		foreach(var item in guns)
		{
			item.Init();
		}
		//设置初始枪支为手枪
		SetGun(0);
		//关闭瞄准镜
		scoping = false;
		//设置初始状态为空间
		SetState(PlayerState.idle,true);
	}

	public override void BeShot (int dmg)
	{
		base.BeShot (dmg);
	}

	public override void Die ()
	{
		base.Die ();
		UI_Manager.Instance.PageTransition(UIState.Gameover);
		SetState(PlayerState.dead,true);
	}

	public override void Hurt ()
	{
		base.Hurt ();
		UI_Gaming.Instance.Hurt ();

		AudioManager.PlaySound("breathe1",1,true,transform,Vector3.zero,0);
	}

	public override void SetAnimationSpeed(string aniName,float spd)
	{
		animation[aniName+ "-"+gun.gunName].speed = spd;
	}

	public override void PlayAnimation (string aniName)
	{
		base.PlayAnimation (aniName+ "-"+gun.gunName);
	}


	#endregion
	//开启瞄准镜
	public void SetScope(bool _scoping)
	{
		scoping = _scoping;
		if(!scoping)
		{
			//显示瞄准镜内视图
			UI_Gaming.Instance.SetScope(false);
			//隐藏枪支模型
			gun.SetScope(false);
			//设置摄像机FOV来观察更远的距离
			cam.fieldOfView = 60f;
		}
		else
		{
			UI_Gaming.Instance.SetScope(true);
			gun.SetScope(true);
			cam.fieldOfView = 18f;
		}
	}
	
	public void Fire()
	{
		if(gun.ammo>0)
		{
			gun.ammo--;
			Shoot();
		}
		else
		{
			if(gun.clip>0)
			{
				SetState(PlayerState.reload,true);
			}
			else
			{
				SetState(PlayerState.swap,true);
			}
		}
	}
	public override void Shoot()
	{
		//发射射线判断是否命中敌人
		Common.ShootRay(turnRoot.position,turnRoot.forward,1000,"Enemy",Shot);
	}

    void Shot(RaycastHit info)
    {
		Debug.Log("Shot");
		Enemy enemy = info.collider.GetComponent<Enemy>();
		enemy.BeShot(power);
	}
	//得到枚举对应
	public StateBase GetState(PlayerState state)
	{
		return states[(int)state];
	}
	//设置状态
	public void SetState(PlayerState _state = PlayerState.idle,bool doExit = false)
	{
		enumState = _state;
		if(state!=null && doExit)
			state.Exit();
		state = states[(int)_state];
		state.Enter();
	}
	public void Swap()
	{
		SetGun(NextGunID);
	}
	void SetGun(int id)
	{
		gunId = id;
		for(int i=0;i<guns.Count;i++){
			if(i==id)
			{
				guns[i].gameObject.SetActive(true);
				gun = guns[i];
				animation = gun.animation;
			}
			else
			{
				guns[i].gameObject.SetActive(false);
			}
		}

		power = gun.power;
	
		AudioManager.PlaySound(gun.sound_equip,1,true,transform,Vector3.zero,0);

		GetState(PlayerState.fire).audioClipName_animationSync = gun.sound_fire;
		GetState(PlayerState.fireAuto).audioClipName_animationSync = gun.sound_fire;

		GetState(PlayerState.reload).audioClipName_animationSync = gun.sound_clipout;
		GetState (PlayerState.reload).audioDelays.Clear ();
		if (!string.IsNullOrEmpty (gun.sound_clipin)) {
			GetState (PlayerState.reload).audioDelays.Add(
				new AudioDelay(gun.sound_clipin,gun.sound_clipin_delay));
		}
		if (!string.IsNullOrEmpty (gun.sound_boltpull)) {
			GetState (PlayerState.reload).audioDelays.Add(
				new AudioDelay(gun.sound_boltpull,gun.sound_boltpull_delay));
		}
	}	
}
