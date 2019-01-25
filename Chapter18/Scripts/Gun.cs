using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public string gunName;//名称
	public bool hasScope;//枪支是否有瞄准镜
	public int power;//威力
	public int ammoMax;//弹匣数
	public int clipMax;//弹匣子弹数
	public bool isAuto;//是否自动
	public Animation animation;//动画
	public int clip{get;set;}//当前子弹数量
	public int ammo{get;set;}//当前弹匣数量

	public string sound_equip;//
	public string sound_fire;//射击音效
	public string sound_clipout;//退出弹匣音效
	public string sound_clipin;//放进弹匣音效
	public float sound_clipin_delay;//放进弹匣音效播放延时
	public string sound_boltpull;//保险栓音效
	public float sound_boltpull_delay;//保险栓音效播放延时

	public ParticleSystem sparkPS;//射击特效
	public void Init()
	{
		ammo = ammoMax;
		clip = clipMax;
	}
	//设置瞄准镜
	public void SetScope(bool scope)
	{
		foreach(var item in GetComponentsInChildren<SkinnedMeshRenderer>())
		{
			item.enabled = !scope;
		}
		foreach(var item in GetComponentsInChildren<MeshRenderer>())
		{
			item.enabled = !scope;
		}
	}
	//换子弹
	public void Reload()
	{
		int can_refill = Mathf.Min(ammoMax,clip);
		int needRefill = ammoMax - ammo;
		int realFill = 0;
		if(needRefill >can_refill)
		{
			realFill = can_refill;
		}
		else
		{
			realFill = needRefill;
		}
		ammo += realFill;
		clip -= realFill;
	}
	//播放射击特效
	public void SparkEffect(bool ena)
	{
		if(sparkPS == null)
			return;
		if(ena)
		{
			sparkPS.Play();
		}
		else
		{
			sparkPS.Stop();
		}
	}
}
