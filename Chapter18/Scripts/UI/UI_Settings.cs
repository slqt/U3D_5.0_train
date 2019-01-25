using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_Settings : MonoBehaviour {
	public Slider slider_music;
	public Slider slider_sound;

	public void OnPress_Close()
	{
		UI_Manager.Instance.Sound_Click();
		Debug.Log("OnPress_Close");
		UI_Manager.Instance.PageTransition( UIState.Main);
	}
}
