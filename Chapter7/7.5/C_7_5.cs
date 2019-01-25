using UnityEngine;
using System.Collections;

public class C_7_5 : MonoBehaviour {
private	Vector3 direction = new Vector3(1,0,0);//前进及射线方向
	private	float distance = 1f;//射线距离

	void Update () {
		transform.position += direction * Time.deltaTime;//玩家向右移动
		Ray ray = new Ray(transform.position, direction);//新建射线
		RaycastHit info;//接受射线撞击信息的变量

		//发射射线方法1
		if (Physics.Raycast (ray,out info,distance) ){
			//发现障碍
			Debug.Log ("前方有障碍"+info.collider.name);
		}
		Debug.DrawLine(ray.origin,ray.origin + direction*distance);//用DrawLine显示射线

//		//发射射线方法2
//		if (Physics.Raycast (transform.position, new Vector3(1,0,0),1)) {
//			Debug.Log ("前方有障碍!");
//		}
//		//发射射线方法3
//		if (Physics.Raycast (transform.position,transform.forward,1)) {
//			Debug.Log ("前方有障碍!");
//		}
	}
}
