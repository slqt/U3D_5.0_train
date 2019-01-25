using UnityEngine;
using System.Collections;

public class SetResolution: MonoBehaviour {
	//默认屏幕宽度
	public const float defaultScreenWidth = 1280f;
	//默认屏幕高度
	public const float defaultScreenHeight = 720f;
	//实际/默认 宽度比
	public static float XRatio;
	//实际/默认 高度比
	public static float YRatio;
	//XRatio与YRatio之间的较小值
	public static float minRatio;
	void Awake () {
		XRatio = Screen.width / defaultScreenWidth;
		YRatio = Screen.height / defaultScreenHeight;
		minRatio = XRatio < YRatio ? XRatio : YRatio;
	}
	//对UI界面进行适配
	public static void SetAResolution(Transform root)
	{		
		UISetReso_Attach[] objs = root.GetComponentsInChildren<UISetReso_Attach>(true);
		for(int i =0;  i< objs.Length;i++)
		{
			SetOne(objs[i]);
		}
	}
	//对设置了适配类型的对象进行适配
	public static void SetOne(UISetReso_Attach att)
	{
		if (att == null)
			return;
		//适配位置
		att.transform.localPosition = new Vector3(att.transform.localPosition.x *XRatio, att.transform.localPosition.y *YRatio, att.transform.localPosition.z);
		//适配缩放
		if(att.type == ResolutionType.normal)
		{
			//等比缩放
			att.transform.localScale = new Vector3(att.transform.localScale.x * minRatio , att.transform.localScale.y * minRatio, 1f);
		}else if(att.type == ResolutionType.stretch)
		{
			//拉伸
			att.transform.localScale = new Vector3(att.transform.localScale.x * XRatio, att.transform.localScale.y * YRatio , 1f);
		}
	}
}
