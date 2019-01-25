using UnityEngine;
using System.Collections;
//通过枚举定义轴向，可通过调用GetAxis函数获得轴向的值
public enum PadAxis
{
	Horizontal,
	Vertical
}
//操纵杆
public class Pad
{
	public Vector2 topLeft;//判定区域的左上角顶点
	public Vector2 bottomRight;//判定区域的右下角顶点
	public float range;//圆钮移动范围半径
	public GameObject pad;//圆盘
	public GameObject ball;//圆钮

	public int fingerId = -1;//操作此操纵杆的手指id
	public Vector2 centerPos;//中心点位置
	public Vector2 lastPos;//圆钮上一帧的位置

	public Vector2 movement;//手指在本帧移动的向量
	private Vector2 offsetUI;//UI坐标系 与 屏幕 坐标系的偏差
	public Pad(Vector2 _topLeft,Vector2 _bottomRight,GameObject _pad,GameObject _ball,float _range)
	{
		topLeft = _topLeft;
		bottomRight = _bottomRight;
		range = _range;
		pad = _pad;
		ball = _ball;

		offsetUI = new Vector2(- ((float)Screen.width) *0.5f,- ((float)Screen.height) *0.5f);
	}
	public void Update()
	{
		UpdateInput();
		UpdateUI();
	}
	void UpdateInput()
	{
		if(fingerId == -1)
		{
			foreach(var touch in Input.touches)
			{
				if(touch.phase == TouchPhase.Began && TouchInRegion(touch))
				{
					movement = Vector2.zero;
					fingerId = touch.fingerId;
					centerPos = touch.position;
					lastPos = touch.position;
				}
			}
		}
		else
		{
			foreach(var touch in Input.touches)
			{
				if(touch.fingerId == fingerId)
				{
					if(touch.phase == TouchPhase.Canceled ||touch.phase == TouchPhase.Ended)
					{
						movement = Vector2.zero;
						fingerId = -1;
					}
					else
					{
						movement = touch.position - lastPos;
						lastPos = touch.position;
						if(range>0)
							lastPos = ClampPos(lastPos);
					}
				}
			}
		}
	}
	void UpdateUI()
	{
		if(pad==null ||ball == null)
		{
			return;
		}
		if(fingerId == -1)
		{
			pad.gameObject.SetActive(false);
			ball.gameObject.SetActive(false);
			return;
		}

		pad.gameObject.SetActive(true);
		ball.gameObject.SetActive(true);
		pad.transform.localPosition = ScreenPos2UIPos(centerPos);
		ball.transform.localPosition =  ScreenPos2UIPos(lastPos);
	}

	Vector3 ScreenPos2UIPos(Vector2 pos)
	{
		Vector2 output = pos + offsetUI;
		return new Vector3(output.x,output.y,0);
	}

	bool TouchInRegion(Touch touch)
	{
		return touch.position.x > topLeft.x && touch.position.x <bottomRight.x &&
			touch.position.y < topLeft.y && touch.position.y > bottomRight.y;
	}

	Vector2 ClampPos(Vector2 pos)
	{
		Vector2 dir = pos - centerPos;
		if(dir.magnitude <range)
		{
			return pos;
		}
		else
		{
			dir.Normalize();
			return centerPos + dir * range;
		}
	}

	public float GetAxis(PadAxis axis)
	{
		if(fingerId == -1)
			return 0;
		if(axis == PadAxis.Horizontal)
		{
			return (lastPos.x - centerPos.x)/range;
		}
		else
		{
			return (lastPos.y - centerPos.y)/range;
		}
	}

	public void Clear()
	{
		fingerId = -1;
		if(pad !=null && ball != null)
		{
			pad.gameObject.SetActive(false);
			ball.gameObject.SetActive(false);
		}
	}
}
public class PadManager : MonoBehaviour {
	public static PadManager Instance;
	public GameObject leftpad_pad;
	public GameObject leftpad_ball;

	public Pad leftpad{get;set;}
	public Pad rightpad{get;set;}
	void Awake()
	{
		Instance = this;
		Init();
	}
	public void Init()
	{
		leftpad = new Pad(new Vector2(0,((float)Screen.height)),new Vector2(((float)Screen.width) *0.5f,0),leftpad_pad,leftpad_ball,100);
		rightpad = new Pad(new Vector2(((float)Screen.width) *0.5f,((float)Screen.height)),new Vector2(((float)Screen.width),0),null,null,0);
	}
	void Update()
	{
		if(UI_Manager.Instance.currentState != UIState.Gaming)
		{
			leftpad.Clear();
			rightpad.Clear();
			return;
		}
		leftpad.Update();
		rightpad.Update();
	}
}
