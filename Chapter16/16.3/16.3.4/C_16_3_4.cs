using UnityEngine;
using System.Collections;

public enum HockeyGameState
{
	lobby,
	idle,
	gaming,
	end
}
public class C_16_3_2 : MonoBehaviour {
	public GameObject prefab_player;
	public GameObject prefab_ball;
	
	public Camera cam;
	private HockeyGameState state = HockeyGameState.lobby;
	private string IP = "10.234.41.123";
	private int Port = 1000;
	
	private  Vector3 respawn_center = new Vector3(0f,0.25f,0f);
	private  Vector3 respawn_top = new Vector3(0f,0.25f,2f);
	private  Vector3 respawn_bot = new Vector3(0f,0.25f,-2f);
	private Rigidbody playerMe;
	private Rigidbody ball;
	private bool result;
	private NetworkView networkView ;

	private float bound_x = 3.4f;
	private float bound_z = 4.78f;
	private Vector3 lastPos;
	public static float force2Ball;
	void Awake()
	{
		networkView = GetComponent<NetworkView>();
	}
	void OnGUI()
	{
		switch(state)
		{
		case HockeyGameState.lobby:
			OnLobby();
			break;
		case HockeyGameState.idle:
			OnIdle();
			break;
		case HockeyGameState.gaming:
			OnGaming();
			break;
		case HockeyGameState.end:
			OnEnd();
			break;
		}
	}
	
	void OnLobby()
	{
		if(GUILayout.Button("建立服务器"))
		{
			NetworkConnectionError error  = Network.InitializeServer(10,Port,false);
			if(error == NetworkConnectionError.NoError)
				state = HockeyGameState.idle;
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
				state = HockeyGameState.idle;
		}
	}	
	
	void OnIdle()
	{
		if(Network.isServer)
		{
			if(Network.connections.Length==1)
			{
				if(GUILayout.Button("Start!"))
				{
					StartGaming();
				}
			}
			else
			{
				GUILayout.Label("等待玩家加入");
			}
		}
		else
		{
			GUILayout.Label("等待主机开始游戏");
		}
	}


	void Update()
	{
		if(state!= HockeyGameState.gaming)
			return;


		if(Input.GetMouseButton(0))
		{
			Vector3 point = cam.ScreenToWorldPoint(Input.mousePosition);
			float x  = Mathf.Clamp(point.x,-bound_x,bound_x);
			float z  = Mathf.Clamp(point.z,-bound_z,bound_z);
			Vector3 newPos = new Vector3(x,0.25f,z);
				
				playerMe.MovePosition(Vector3.Lerp(playerMe.position,newPos, 0.3f));
				
				force2Ball = ( playerMe.position  - lastPos).magnitude * 100f;
	
		}
		lastPos = playerMe.position;


		if(Network.isServer)
		{
			if(playerMe.position.z> respawn_center.z)
				playerMe.position = new Vector3(playerMe.position.x,
				                                playerMe.position.y,
				                                respawn_center.z - 0.2f);
			
			if(ball.transform.position.z <-6 || ball.transform.position.z >6)
			{
				Network.Destroy(ball.GetComponent<NetworkView>().viewID);
				state = HockeyGameState.end;
				
				bool isHostWin = ball.transform.position.z>0;
				GameEnd(isHostWin);
				networkView.RPC("GameEnd", RPCMode.Others,!isHostWin);
			}
		}
		else
		{
			if(playerMe.position.z < respawn_center.z)
				playerMe.position = new Vector3(playerMe.position.x,
				                                playerMe.position.y,
				                                respawn_center.z + 0.2f);
		}

	}
	void OnGaming()
	{
		//		float x = Input.GetAxis("Horizontal");
		//		float y = Input.GetAxis("Vertical");
		//		playerMe.MovePosition(new Vector3(x,0,y));
	}
	
	void OnEnd()
	{
		if(result)
			GUILayout.Label("You Win!");
		else
			GUILayout.Label("You Lose!");
		
		
		if(Network.isServer && Network.connections.Length==1)
		{
			if(GUILayout.Button("Start Again!"))
			{
				StartGaming();
			}
		}
	}
	void StartGaming()
	{
		state = HockeyGameState.gaming;
		
		GameObject obj = null;
		obj = Network.Instantiate(prefab_player,respawn_bot,Quaternion.identity,0) as GameObject;
		playerMe = obj.GetComponent<Rigidbody>();
		obj = Network.Instantiate(prefab_ball,respawn_center,Quaternion.identity,0) as GameObject;
		ball = obj.GetComponent<Rigidbody>();
		
		networkView.RPC("Client_StartGaming", RPCMode.Others);
		//		if(Network.isServer)
		//			obj.GetComponent<Renderer>().material.color = Color.red;
		//		else
		//			obj.GetComponent<Renderer>().material.color = Color.green;
	}
	[RPC]
	void Client_StartGaming()
	{
		state = HockeyGameState.gaming;
		GameObject obj = null;
		obj = Network.Instantiate(prefab_player,respawn_top,Quaternion.identity,0) as GameObject;
		playerMe = obj.GetComponent<Rigidbody>();
	}
	
	[RPC]
	void GameEnd(bool win)
	{
		Network.Destroy(playerMe.GetComponent<NetworkView>().viewID);
		result = win;
		state = HockeyGameState.end;
	}
}

