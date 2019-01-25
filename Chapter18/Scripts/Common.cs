using UnityEngine;
using System.Collections;

public class Common {
	public static bool ShootRay(Vector3 orgin,Vector3 dir,float dis,string tag,System.Action<RaycastHit> callback)
	{
		RaycastHit info;
		if(Physics.Raycast(orgin,dir,out info,dis))
		{
			if(info.collider.tag == tag)
			{
				if(callback!=null)
					callback(info);
				return true;
			} 
		}
		return false;
	}
}
