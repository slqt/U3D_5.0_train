using UnityEngine;
using System.Collections;

public class C_16_3_3_Server : MonoBehaviour {
	string gameName = "CrazyBalls";
	void Start()
	{
		//指定MasterServer的ip地址
		MasterServer.ipAddress = "10.234.41.123";
		//指定MasterServer的端口号
		MasterServer.port = 23466;
		Network.InitializeServer(32, 25003,false);  
		MasterServer.RegisterHost(gameName, "小苹果的房间", "来一起玩吧");  
	}
}
