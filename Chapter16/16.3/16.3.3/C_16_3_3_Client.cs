using UnityEngine;
using System.Collections;

public class C_16_3_3_Client : MonoBehaviour {
	void Start()
	{
		//指定MasterServer的ip地址
		MasterServer.ipAddress = "10.234.41.123";
		//指定MasterServer的端口号
		MasterServer.port = 23466;
		MasterServer.RequestHostList("CrazyBalls");  
	}
	void OnGUI()
	{
		HostData[] data = MasterServer.PollHostList();  
		
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
