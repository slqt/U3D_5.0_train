using UnityEngine;
using System.Collections.Generic;

public class SkillBase {

	public float totalTime;
	public float coodingTime;
	public int priority;
	public int difficulty;
	
	public float nextTime = 0;
	public float currentCooding = 0;
	public int avatarid;

	public int currentIndex = -1;
	private const int baseDatalength = 3;
	public float[] TimePointArray = null;
	public string[] EventArray = null;
	public string[] ParameterArray = null;

	public string skillData;

	public bool isEnd = false;
	
	private int loopCount = 0;
	private int backNumber = 0;

	private bool isIf = false;

	public bool isBigSkill = false;

	public SkillBase(string _info, int _avatarid) {

		skillData = _info;

		avatarid = _avatarid;

		Analyze();

	}

	public void StartSkill(float _currentTime) {
	
		isEnd = false;
		currentIndex = baseDatalength;
		nextTime = _currentTime;
		nextTime += TimePointArray[currentIndex];
	
	}

	public void Update(float _time) {
	
		if(isEnd) return;

		if(_time > nextTime) {
		
			OnEvent();
		
		}
	
	}

	public void OnEventEnd(){
	
		isEnd = true;

		//Increase energy.
		IncreaseEnergyMsg msg = new IncreaseEnergyMsg();
		msg.increase_energy = 100;
		MessengerManager<IncreaseEnergyMsg>.Involve("IncreaseEnergy" + avatarid, msg);
	
	}

	public void OnEvent() {
	
		if(isIf) {
		
			currentIndex++;
			isIf = false;
		
		}

		string _data = GetParameterOne();

		if(_data == "") {

			OnEventEnd();
			return;

		}

		//Debug.Log(" current index : " + currentIndex + "   " + skillData);
		string _type = EventArray[currentIndex - baseDatalength];

		if(_type == "if") {
		
			EventIf(_data);

		}else if(_type == "loop") {
		
			EventLoop(_data);
		
		}else{
		
			EventSkill(_type, _data);
		
		}

		nextTime += TimePointArray[currentIndex - baseDatalength];
	
	}

	public void EventSkill(string _type, string _data) {

		SkillSystem.Instance.DoEvent(_type, _data, avatarid);

	}

	public void EventIf(string _data) {

		string[] _dataArray = _data.Split(' ');

		if(_dataArray[0] == "HP") {
		
			AvatarData avatarData = AvatarManager.Instance.Find(avatarid);

			if(avatarData != null) {
			
				if(_dataArray[1] == "<") {
				
					if(_dataArray[2] == "percent") {
					
						float percent = 1;
						float.TryParse(_dataArray[3], out percent);

						percent /= 100;

						if((float)avatarData.properties.hp/avatarData.properties.maxhp < percent) {
						
							if(_dataArray[4] == "jump") {

								int jump = 0;
								int.TryParse(_dataArray[5], out jump);
								currentIndex += jump;
							
							}

						}else{
						
							isIf = true;
						
						}
					
					}
				
				}
			
			}
		
		}

	}

	public void EventLoop(string _data) {

		if(loopCount == -1) {
		
			loopCount--;
			currentIndex -= backNumber;
		
		}else{
		
			string[] _dataArray = _data.Split(' ');
			if(_dataArray.Length >= 2) {

				int.TryParse(_dataArray[0], out loopCount);

				if(int.TryParse(_dataArray[1], out backNumber)) {
				
					currentIndex -= backNumber;
				
				}
			}
		
		}

	}

	public void Analyze() {
	
		ParameterArray = skillData.Split('\n');

		if(ParameterArray.Length > 4) {
		
			AnalyzeBaseData(GetParameterOne());
			AnalyzeTimePoint(GetParameterOne());
			AnalyzeEvent(GetParameterOne());


		}else{
		
			Debug.Log("Error skill data : " + skillData);
		
		}
	
	}

	public string GetParameterOne() {
	
		currentIndex++;

		if(currentIndex < ParameterArray.Length) {
		
			return ParameterArray[currentIndex];
		
		}

		return "";
	
	}

	private void AnalyzeBaseData(string _baseString) {
	
		string[] _baseArray = _baseString.Split(' ');
		
		if(_baseArray.Length == 4) {
			
			float.TryParse(_baseArray[0], out totalTime);
			float.TryParse(_baseArray[1], out coodingTime);
			int.TryParse(_baseArray[2], out priority);
			int.TryParse(_baseArray[3], out difficulty);
			
		}else{

			Debug.Log("Error skill base data : " +_baseString);

		}
	
	}

	private void AnalyzeTimePoint(string _timeString) {
	
		string[] _timeArray = _timeString.Split(' ');

		if(_timeArray.Length == ParameterArray.Length - baseDatalength) {
		
			TimePointArray = new float[_timeArray.Length];

			for(int i = 0;  i < _timeArray.Length; i++) {
			
				float.TryParse(_timeArray[i], out TimePointArray[i]);
			
			}
		
		}
	
	}

	private void AnalyzeEvent(string _eventString) {
	
		EventArray = _eventString.Split(' ');
	
	}


}