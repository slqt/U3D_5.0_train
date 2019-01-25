using UnityEngine;
using System.Collections;

public class C_3_5_4_2 : MonoBehaviour {
	//消息发送后,ShowNumber函数被自动调用
	void ShowNumber(int number)
	{
		Debug.Log("收到的数字是"+number);
	}
}
