using UnityEngine;
using System.Collections;

public class C_16_3_2_Client : MonoBehaviour {
	void Start()
	{
		//请求CrazyBalls游戏
		MasterServer.RequestHostList("CrazyBalls");  
	}
	void OnGUI()
	{
		//得到所有服务器
		HostData[] data = MasterServer.PollHostList();  
		//显示服务器列表，并可加入
		foreach (HostData hostData in data)  
		{
			GUILayout.BeginHorizontal();  
			string text = hostData.gameName + "      房间人数：" + hostData.connectedPlayers + "/" + hostData.playerLimit; 
			GUILayout.Label(text);  
			string hostInfo = "";  
			foreach (string hostIp in hostData.ip)  
			{  
				hostInfo += hostIp + ":" ;  
			} 
			hostInfo += hostData.port; 
			if(GUILayout.Button("加入"))  
			{  
				Network.Connect(hostData);  
			}  
			GUILayout.EndHorizontal();
		}
	}
}
