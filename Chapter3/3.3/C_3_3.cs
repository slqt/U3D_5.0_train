using UnityEngine;
using System.Collections;

public class C_3_3 : MonoBehaviour {
	string text="";
	string myName="";
	void OnGUI()
	{
		//用标签显示文本
		GUILayout.Label("请输入你的名字：");
		//用文本区域输入名字
		text = GUILayout.TextField(text);
		//
		if(GUILayout.Button("提交"))
		{
			myName = text;
		}
		//当myName不为空的时候，说明我们已经提交了名字，则显示名字
		if( !string.IsNullOrEmpty(myName))
		{
			GUILayout.Label("提交成功，名字："+myName);
		}
	
	}
}
