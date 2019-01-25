using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_16_2_2 : MonoBehaviour {
	private STATE state = STATE.idle;
	private string IP = "10.234.41.123";
	private int Port = 1000;
	
	private NetworkView networkView;
	private string myName="";
	//聊天消息记录
	private string textRecord="";
	//将要发送的消息
	private string textToSend="";
	//显示聊天记录的ScrollView的滚动位置
	private Vector2 scrollPos;
	//显示聊天室当前人员的ScrollView的滚动位置
	private Vector2 scrollPos_manlist;
	//储存NetworkViewID以及名字的字典
	private Dictionary<string,string> manList = new Dictionary<string, string>();
	//整个区域的宽度
	private float thewidth = 500f;
	//整个区域的高度
	private float theheight = 500f;
	//聊天室当前人员名字的链表
	private List<string> names = new List<string>();
	void Awake()
	{
		networkView = GetComponent<NetworkView>();
	}
	void OnGUI()
	{
		GUILayout.BeginVertical(GUILayout.Width(thewidth),GUILayout.Height(theheight));
		switch(state)
		{
		case STATE.idle:
			OnIdle();
			break;
		case STATE.server:
			Format_CutOffLine();
			Title();
			Format_CutOffLine();
			OnChat();
			Format_CutOffLine();
			OnServer();
			break;
		case STATE.client:
			Format_CutOffLine();
			Title();
			Format_CutOffLine();
			OnChat();
			Format_CutOffLine();
			OnClient();
			break;
		}
		GUILayout.EndVertical();
	}
	//显示标题
	void Title()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(thewidth*0.5f -10f);
		GUILayout.Label("聊天室");
		GUILayout.EndHorizontal();
	}
	//idle状态
	void OnIdle()
	{
		Format_CutOffLine();
		GUILayout.BeginHorizontal();
		GUILayout.Label("我的昵称:",GUILayout.Width(100));
		myName = GUILayout.TextField(myName,GUILayout.Width(thewidth-100));
		GUILayout.EndHorizontal();
		Format_CutOffLine();

		Format_Space();

		Format_CutOffLine();
		if(GUILayout.Button("建立聊天室",GUILayout.Width(thewidth)))
		{
			NetworkConnectionError error  = Network.InitializeServer(10,Port,false);
			if(error == NetworkConnectionError.NoError)
			{
				manList.Add(Network.player.ToString(),myName);
				Server_UpdateNameList();
				state = STATE.server;
			}
		}
		Format_CutOffLine();

		Format_Space();

		Format_CutOffLine();
		GUILayout.BeginHorizontal();
		GUILayout.Label("服务器IP地址:");
		IP = GUILayout.TextField(IP,GUILayout.Width(thewidth-100));
		GUILayout.EndHorizontal();
		if(GUILayout.Button("连接服务器",GUILayout.Width(thewidth)))
		{
			NetworkConnectionError error  = Network.Connect(IP,Port);
			if(error == NetworkConnectionError.NoError)
			{
				state = STATE.client;
			}
		}
		Format_CutOffLine();
	}
	//连接服务器
	void OnConnectedToServer() {
		Debug.Log("Connected to server");
		networkView.RPC("Server_Join", RPCMode.Server,myName);
	}
	//当有客户端断开服务器时被调用的函数
	void OnPlayerDisconnected(NetworkPlayer player) {
		string id = player.ToString();
		Debug.Log("Leaved:"+id);
		if(manList.ContainsKey(id))
		{
			networkView.RPC("SendText", RPCMode.All,manList[id]+"离开了聊天室");
			manList.Remove(id);
		}
		Server_UpdateNameList();
	}
	//server状态
	void OnServer()
	{
		GUILayout.Label("当前连接数量："+ Network.connections.Length);
		if(GUILayout.Button("关闭聊天室",GUILayout.Width(thewidth)))
		{
			Network.Disconnect();
			state = STATE.idle;
		}
	}
	//client状态
	void OnClient()
	{
		if(GUILayout.Button("离开聊天室",GUILayout.Width(thewidth)))
		{
			Network.Disconnect();
			state = STATE.idle;
		}
	}
	//共用的处理聊天的函数
	void OnChat()
	{
		GUILayout.BeginHorizontal();

			GUILayout.BeginVertical();
			scrollPos = GUILayout.BeginScrollView(scrollPos,GUILayout.Width(thewidth-100),GUILayout.Height(300));
			GUILayout.Label(textRecord);
			GUILayout.EndScrollView();
			GUILayout.EndVertical();

			GUILayout.BeginVertical();
			GUILayout.Label("~聊天室成员~");
			scrollPos_manlist = GUILayout.BeginScrollView(scrollPos_manlist,GUILayout.Width(100),GUILayout.Height(300));
			string txt ="";
			foreach(var s in names)
			{
				txt += s +"\n";
			}
			GUILayout.Label(txt);
			GUILayout.EndScrollView();
			GUILayout.EndVertical();

		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
			textToSend = GUILayout.TextField(textToSend,GUILayout.Width(thewidth-50));
			if(GUILayout.Button("发送",GUILayout.Width(50)))
			{
				networkView.RPC("SendText", RPCMode.All,myName+":"+textToSend);
				textToSend ="";
			}
		GUILayout.EndHorizontal();
	}
	//服务器向客户端发送人员名单及聊天记录，供客户端更新显示
	void Server_UpdateNameList()
	{
		string textToSend="";
		foreach(var item in manList)
		{
			textToSend += item.Value+"|";
		}
		networkView.RPC("RPC_GetNameList", RPCMode.All,textToSend);
		networkView.RPC("RPC_ChatRecordUpdate", RPCMode.All,textRecord);
	}
	//不同栏间的填充
	void Format_CutOffLine()
	{
		GUILayout.Label("===============================================================");
	}
	//标准间隔
	void Format_Space()
	{
		GUILayout.Space(50);
	}
	//当客户端连接成功时向服务器发送的消息
	[RPC]
	public void Server_Join(string client_name, NetworkMessageInfo info)
	{
		string id = info.sender.ToString();
		Debug.Log("Joined:"+id);
		networkView.RPC("SendText", RPCMode.All,client_name+"加入了聊天室");
		manList.Add(id,client_name);
		Server_UpdateNameList();
	}
	//发送文本
	[RPC]
	public void SendText(string content, NetworkMessageInfo info)
	{
		textRecord += content+"\n";
	}
	//人员名单
	[RPC]
	public void RPC_GetNameList(string content, NetworkMessageInfo info)
	{
		string[] ss = content.Split('|');
		names.Clear();
		names.AddRange(ss);
	}
	//聊天记录
	[RPC]
	public void RPC_ChatRecordUpdate(string content)
	{
		textRecord = content;
	}
}