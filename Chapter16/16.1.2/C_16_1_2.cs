using UnityEngine;
using System.Collections;

public enum STATE
{
	idle,
	server,
	client
}
public class C_16_1_2 : MonoBehaviour {
	private STATE state = STATE.idle;
	private string IP = "127.0.0.1";
	private int Port = 1000;
	
	void OnGUI()
	{
		switch(state)
		{
		case STATE.idle:
			OnIdle();
			break;
		case STATE.server:
			OnServer();
			break;
		case STATE.client:
			OnClient();
			break;
		}
	}

	//idle状态，可以创建或连接服务器
	void OnIdle()
	{
		if(GUILayout.Button("建立服务器"))
		{
			NetworkConnectionError error  = Network.InitializeServer(10,Port,false);
			if(error == NetworkConnectionError.NoError)
				state = STATE.server;
		}
		GUILayout.Label("======================================");
		GUILayout.BeginHorizontal();
		GUILayout.Label("服务器IP地址:");
		IP = GUILayout.TextField(IP);
		GUILayout.EndHorizontal();
		if(GUILayout.Button("连接服务器"))
		{
			NetworkConnectionError error  = Network.Connect(IP,Port);
			if(error == NetworkConnectionError.NoError)
				state = STATE.client;
		}
	}	
	//server状态，显示客户端连接信息
	void OnServer()
	{
		GUILayout.Label("当前连接数量："+ Network.connections.Length);
		if(GUILayout.Button("断开服务器"))
		{
			Network.Disconnect();
			state = STATE.idle;
		}
	}
	//client状态
	void OnClient()
	{
		GUILayout.Label("已连接至服务器");
		if(GUILayout.Button("断开"))
		{
			Network.Disconnect();
			state = STATE.idle;
		}
	}
}

