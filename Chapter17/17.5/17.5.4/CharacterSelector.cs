using UnityEngine;
using System.Collections;

public class CharacterSelector : MonoBehaviour {
	//摄像机
	public Camera cam;
	//上次选择的角色
	private GameObject lastSelectedChar;
	void Update()
	{
		//计算机：监听到点击鼠标左键
		//移动设备：监听到手指点击屏幕
		if (Input.GetMouseButtonDown (0)) {
			//将点击屏幕的位置转化为一条从相机射出的射线
			Ray ray =	cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			//射出射线并将信息输出至hitInfo
			if(Physics.Raycast(ray,out hitInfo))
			{
				//如果射线碰到了碰撞体,并且碰撞体对象的层为Player,即角色
				if(hitInfo.collider!=null && hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
				{
					//如果有上次选取角色并且不是将要选择的角色，则取消选取
					if(lastSelectedChar!=null && lastSelectedChar.GetInstanceID() != hitInfo.collider.gameObject.GetInstanceID())
					{
						lastSelectedChar.transform.FindChild("Projector").GetComponent<Projector>().enabled = false;
					}
					//选取并激活光圈
					hitInfo.collider.transform.FindChild("Projector").GetComponent<Projector>().enabled = true;
					//记录选取的角色
					lastSelectedChar = hitInfo.collider.gameObject;
				}
			}
		}
	}
}
