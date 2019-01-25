using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class C_10_3 : MonoBehaviour {
	public GameObject page_create;//创建角色页面
	public InputField inputField_name;//名字输入
	public Toggle toggle_warrior;//选择按钮:战士
	
	public GameObject page_start;//剧情页面
	public Text start_text;//剧情文字

	string[] OccupationCn = {"战士","魔法师"};//职业中文名
	void Start()
	{
		if(!DataManager.CharData_Instance.created)
		{
			//如果尚未创建角色，则显示创建角色页面
			page_create.SetActive(true);
			page_start.SetActive(false);
		}
		else
		{
			//已经创建角色，则显示剧情页面
			StartGame();
		}
	}

	/// <summary>
	/// 点击创建按钮
	/// </summary>
	public void On_Create()
	{
		//如果玩家没有填写名字，可以提醒玩家
		if(string.IsNullOrEmpty(inputField_name.text))
		{
			Debug.Log("尚未填写名字");
			return;
		}

		//创建角色
		DataManager.CharData_Instance.created = true;//设置已创建标记
		DataManager.CharData_Instance.name = inputField_name.text;//保存名字

		//保存职业
		if(toggle_warrior.isOn)
			DataManager.CharData_Instance.occupation = 0;
		else
			DataManager.CharData_Instance.occupation = 1;
		//保存数据
		DataManager.Save();
		//显示剧情页面
		StartGame();
	}

	/// <summary>
	/// 显示剧情页面
	/// </summary>
	void StartGame()
	{
		page_create.SetActive(false);//隐藏创建角色页面
		page_start.SetActive(true);//显示剧情页面

		//获取职业中文名
		string occCn = OccupationCn[DataManager.CharData_Instance.occupation];
		//剧情文字
		start_text.text = string.Format(
			"\t在这片美丽富饶的土地上，有\n一位名叫{0}的青年，职业是\n{1}...",DataManager.CharData_Instance.name,
			occCn);
	}
}
