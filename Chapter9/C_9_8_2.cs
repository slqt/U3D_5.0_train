using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class C_9_8_2 : MonoBehaviour {
	private ToggleGroup toggleGroup;
	void Awake()
	{
		//得到Toggle组
		toggleGroup = GetComponent<ToggleGroup>();
	}
	//当选择Toggle后，在控制台输出所选Toggle的信息
	public void OnPress_Toggle()
	{
		Toggle selected = null;
		foreach(Toggle item in toggleGroup.ActiveToggles())
		{
			selected = item;
			break;
		}
		Debug.Log("选择了："+selected.transform.FindChild("Label").GetComponent<Text>().text);
	}
}
