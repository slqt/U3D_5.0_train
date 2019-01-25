using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattlePanel : HudBase {

	public Text tx_Time;
	public Text tx_level;
	private float time;
	private int second;
	private int minute;
	public string secondStr;
	public string minuteStr;

	public GameObject objSkillBtn;
	public int indexBtn = 0;

	public override void OnShow ()
	{
		base.OnShow ();

		time = 0;

		tx_level.text = "Level:" + GameManager.Instance.GameLevel;

		MessengerManager<InitSkillMsg>.AddListener("InitBigSkill", InitSkillBotton);

	}

	public override void OnClose ()
	{
		base.OnClose ();

		MessengerManager<InitSkillMsg>.RemoveListener("InitBigSkill", InitSkillBotton);
	}

	public override void UpdateBehaviour ()
	{
		base.UpdateBehaviour ();

		TimeShow();

	}

	private void TimeShow() {
	
		time += Time.deltaTime;
		
		second = Mathf.FloorToInt(time%60);
		secondStr = second.ToString();
		if(second < 10) secondStr = "0" + secondStr;
		
		minute = Mathf.FloorToInt(time/60);
		minuteStr = minute.ToString();
		if(minute < 10) minuteStr = "0" + minuteStr;
		
		tx_Time.text = minuteStr + ":" +  secondStr;
	
	}

	public void InitSkillBotton(InitSkillMsg msg) {
	
		GameObject _objSkillBtn = null;

		if(indexBtn == 0) {
		
			_objSkillBtn = objSkillBtn;

		
		}else{
		
			_objSkillBtn = GameObject.Instantiate(objSkillBtn) as GameObject;

			Vector3 _pos = objSkillBtn.transform.localPosition;

			_pos.x += indexBtn * 90;

			_objSkillBtn.transform.parent = objSkillBtn.transform.parent;
			_objSkillBtn.transform.localPosition = _pos;
			_objSkillBtn.transform.localScale = Vector3.one;
		
		}

		_objSkillBtn.GetComponent<SkillButton>().Init(msg);

		indexBtn++;
	
	}
	

}
