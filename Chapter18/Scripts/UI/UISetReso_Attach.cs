using UnityEngine;
using System.Collections;
//适配类型枚举
public enum ResolutionType
{
	position,
	normal,
	stretch
}
public class UISetReso_Attach : MonoBehaviour {
	//在UI对象上通过设置type参数指定适配类型
	public ResolutionType type;
}
