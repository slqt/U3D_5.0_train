using UnityEngine;
using System.Collections;

public class C_3_5_4_1 : MonoBehaviour {
	public GameObject receiver;
	void Start () {
		//向本脚本所属的游戏对象发送ShowNumber消息并传递参数100
		receiver.SendMessage("ShowNumber",100,SendMessageOptions.DontRequireReceiver);
	}
}
