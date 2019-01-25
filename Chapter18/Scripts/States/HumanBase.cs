using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//主角及敌人的基类
public class HumanBase : MonoBehaviour {
	public int hpMax;//最大生命值
	public int hp{get;set;}//当前生命值
	public int power;//威力，赋值为所使用的枪支的威力
	public float moveSpeed;//移动基础速度
	public float turnSpeed;//转向基础速度
	public float jumpForce;//跳跃基础力度
	public List<StateBase> states = new List<StateBase>();//主角或敌人所有状态的链表
	public StateBase state;//当前状态
	public Animation animation{get;set;}//动画
	public Collider collider{get;set;}//碰撞器
	//初始化
	public virtual void Init()
	{
		hp = hpMax;
		collider = GetComponent<Collider>();
	}
	//设置动画速度
	public virtual void SetAnimationSpeed(string aniName,float spd)
	{
		animation[aniName].speed = spd;
	}
	//播放动画
	public virtual void PlayAnimation(string aniName)
	{
		animation.Play(aniName);
		state.PlayAudioClip_Animation();
	}
	//开枪射击
	public virtual void Shoot()
	{
	
	}
	//被击中
	public virtual void BeShot(int dmg)
	{

		hp -= dmg;

		if(hp>0)
		{
			Hurt();
		}
		else
		{
			Die();
		}
	}
	//受伤逻辑
	public virtual void Hurt()
	{

	}
	//死亡逻辑
	public virtual void Die()
	{
		collider.enabled = false;
	}
}
