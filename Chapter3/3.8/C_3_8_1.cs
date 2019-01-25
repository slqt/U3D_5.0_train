using UnityEngine;
using System.Collections;

public class C_3_8_1 : MonoBehaviour {
	void Update()
	{
		//按下键盘A键
		if(Input.GetKeyDown(KeyCode.A))
		{
			//...
		}
		//按住键盘A键
		if(Input.GetKey(KeyCode.A))
		{
			//...
		}
		//抬起键盘A键
		if(Input.GetKeyUp(KeyCode.A))
		{
			//...
		}

		//按下键盘左Shift键
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			//...
		}
		//按住键盘左Shift键
		if(Input.GetKey(KeyCode.LeftShift))
		{
			//...
		}
		//抬起键盘左Shift键
		if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			//...
		}

		//按下鼠标左键
		if(Input.GetMouseButtonDown(0))
		{
			//...
		}
		//按住鼠标左键
		if(Input.GetMouseButton(0))
		{
			//...
		}
		//抬起鼠标左键
		if(Input.GetMouseButtonUp(0))
		{
			//...
		}

		//按下鼠标右键
		if(Input.GetMouseButtonDown(1))
		{
			//...
		}
		//按住鼠标右键
		if(Input.GetMouseButton(1))
		{
			//...
		}
		//抬起鼠标左键
		if(Input.GetMouseButtonUp(1))
		{
			//...
		}
	}
}
