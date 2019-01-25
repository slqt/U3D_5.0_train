using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class C_9_9 : MonoBehaviour {
	//音量文本
	public Text volume;
	//滑动条
	public Slider slider;
	//默认音量
	float startVolume = 0.5f;
	
	void Start()
	{
		//开始时将默认音量赋予滑动条和文本
		volume.text = startVolume.ToString();
		slider.value = startVolume;
	}
	public void OnPress_Slider()
	{
		//当滑动滑动条时，将最新的值赋予volume，并设置为2位小数点的字符串
		volume.text = slider.value.ToString("f2");
	}
}
