using UnityEngine;
using System.Collections;

public enum BallGameState
{
	lobby,
	idle,
	gaming,
	lose
}
public class C_16_2_3 : MonoBehaviour {
	public GameObject prefab_ball;
	private BallGameState state = BallGameState.lobby;
	private string IP = "10.234.41.123";
	private int Port = 1000;
	private Rigidbody myBall;
	
	void OnGUI()
	{
		switch(state)
		{
		case BallGameState.lobby:
			OnLobby();
			break;
		case BallGameState.idle:
			OnIdle();
			break;
		case BallGameState.gaming:
			OnGaming();
			break;
		case BallGameState.lose:
			OnLose();
			break;
		}
	}
	//大厅状态逻辑
	void OnLobby()
	{
		if(GUILayout.Button("建立服务器"))
		{
			NetworkConnectionError error  = Network.InitializeServer(10,Port,false);
			if(error == NetworkConnectionError.NoError)
				state = BallGameState.idle;
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
				state = BallGameState.idle;
		}
	}	
	//空闲状态逻辑
	void OnIdle()
	{
		if(GUILayout.Button("Start!"))
		{
			StartGaming();
		}
	}
	//游戏状态逻辑
	void OnGaming()
	{
		//控制自己的球体
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		myBall.AddForce(new Vector3(x,0,y));

		//出界则失败
		if(myBall.transform.position.y <-5)
		{
			Network.Destroy(myBall.GetComponent<NetworkView>().viewID);
			state = BallGameState.lose;
		}
	}
	//失败状态逻辑
	void OnLose()
	{
		GUILayout.Label("You Lose!");
		if(GUILayout.Button("Start Again!"))
		{
			StartGaming();
		}
	}
	//开始游戏
	void StartGaming()
	{
		state = BallGameState.gaming;

		GameObject obj = Network.Instantiate(prefab_ball,new Vector3(0,1,0),Quaternion.identity,0) as GameObject;
		myBall = obj.GetComponent<Rigidbody>();
	}
}
