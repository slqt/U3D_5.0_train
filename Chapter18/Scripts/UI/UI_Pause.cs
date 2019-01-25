using UnityEngine;
using System.Collections;

public class UI_Pause : MonoBehaviour {
	void Update () {
		#if !mobile
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			UnPause();
		}
		#endif
	}
	void UnPause()
	{
		Time.timeScale = 1;
		UI_Manager.Instance.PageTransition(UIState.Gaming);
	}

	public void OnPress_UnPause()
	{
		UnPause();
	}
}
