using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {
	void OnGUI()
	{
		if (GUILayout.Button ("Play",GUILayout.Width(100),GUILayout.Height(100))) {
			//移动设备播放全屏视频
			Handheld.PlayFullScreenMovie ("minzufeng.mp4",Color.black,FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.Fill);		
		}
	}
}
