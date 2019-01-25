using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//延时播放音效
public class AudioDelay
{
	public string clipName;//音频名称
	public float delay;//延时时间
	public AudioDelay(string _clipName ,float _delay)
	{
		clipName = _clipName;
		delay = _delay;
	}
}
public class StateBase
{
	protected HumanBase human;//所属的HumanBase
	protected float moveFactor = 1;//移动系数
	protected float turnFactor = 1;//转向系数
	protected float jumpFactor = 1;//跳跃系数
	protected bool resetScope = false;//是否重置（关闭）瞄准镜
	public string animtionName;//动画名称
	protected float animationSpeed = 1;//动画播放速度

	public float audioVolume = 1;//音量
	public string audioClipName_repeat;//循环播放的音频名称
	public string audioClipName_animationSync;//与动画同时播放的音频名称
	public List<AudioDelay> audioDelays = new List<AudioDelay>();//延时音频列表

	public float audioTimerRepeat_Max = float.MinValue;//循环播放音频单次播放时长
	protected float audioTimerRepeat = float.MinValue;//循环播放音频当前剩余时间

	public bool loop;//是否是循环状态
	protected float timerMax;//状态时长
	public float timer;//状态剩余时间

	public StateBase(HumanBase _human)
	{
		human = _human;
		timer = float.MinValue;
	}
	//检查是否进入状态
	public virtual bool Check()
	{
		bool enter = Condition();
		if(enter)
			Enter();
		return enter;
	}
	//进入状态判断
	public virtual bool Condition()
	{
		return false;
	}
	//进入状态逻辑
	public virtual void Enter()
	{
		timer = float.MinValue; 
		audioTimerRepeat = float.MinValue;

		human.SetAnimationSpeed(animtionName,animationSpeed);
		human.PlayAnimation(animtionName);
		if(!loop)
		{
			timer = timerMax;
		}

		PlayAudioClip_Repeat();
	}
	//状态更新
	public virtual void Update()
	{
		AudioUpdate();
	}
	//状态倒计时结束
	public virtual void TimerEnd()
	{
		Exit();
	}
	//退出状态
	public virtual void Exit()
	{
		
	}
	//瞄准镜
	public virtual void Scoping()
	{
		
	}
	#region physics
	//移动
	public virtual void Moving()
	{
		
	}
	//转向
	public virtual void Turning()
	{
		
	}
	//执行跳跃
	public virtual void Jumping()
	{
		
	}
	//跳跃
	public virtual void Jump()
	{
		
	}
	//是否在空中
	public virtual bool InAir()
	{
		return false;
	}
	#endregion
	//更新音频
	public void AudioUpdate()
	{
		if(audioTimerRepeat>float.MinValue+1)
		{
			if(audioTimerRepeat>0)
			{
				audioTimerRepeat -= Time.deltaTime;
			}
			else
			{
				PlayAudioClip_Repeat();
			}
		}
	}
	//播放动画同步音频
	public void PlayAudioClip_Animation()
	{
		AudioManager.PlaySound(audioClipName_animationSync,1,true,human.transform,Vector3.zero,0);

		foreach(var item in audioDelays)
			AudioManager.PlaySound(item.clipName,1,true,
			                       human.transform,Vector3.zero,item.delay);
	}
	//播放循环音频
	protected void PlayAudioClip_Repeat()
	{
		if(string.IsNullOrEmpty(audioClipName_repeat))
			return;
		AudioManager.PlaySound(audioClipName_repeat,1,true,human.transform,Vector3.zero,0);
		audioTimerRepeat = audioTimerRepeat_Max;
	}
}