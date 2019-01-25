using UnityEngine;
using System.Collections.Generic;

public class Sequence {

	private SkillBase currentSkill = null;
	private List<SkillBase> coodingList = new List<SkillBase>();
	private List<SkillBase> ReadyList = new List<SkillBase>();

	public float timepoint = 0;

	public bool Stop { get; set; }

	public void Init() {
	
		Stop = false;
	
	}

	public void AddSkill(SkillBase _skillbase) {
	
		if(!coodingList.Contains(_skillbase)) {
		
			coodingList.Add(_skillbase);
		
		}else{
	
			Debug.LogError("Error, Add same skill!");

		}

	}


	// Update is called once per frame
	public void Update () {

		if(!Stop) {
	
			timepoint += Time.deltaTime;

			if(currentSkill != null) {

				if(currentSkill.isEnd) {

					if(!currentSkill.isBigSkill)
						AddCooding();

				}else{
				
					currentSkill.Update(timepoint);
				
				}

			}

			CoodingUpdate();
		}

	}

	void OnReady() {
	

	
	}

	public void Reset() {
		
		timepoint = 0;
		
		
	}

	void CoodingUpdate() {
		
		if(coodingList.Count <= 0) return;

		for(int i = coodingList.Count - 1; i >= 0; i--) {
		
			if(coodingList[i].currentCooding > 0) {
			
				coodingList[i].currentCooding -= Time.deltaTime;
			
			}else{
			
				if(!ReadyList.Contains(coodingList[i])) {

					ReadyList.Add(coodingList[i]);
					coodingList.RemoveAt(i);


				}else{
				
					//Debug.LogError("Error cooding!");
				
				}

			}
		
		}

		if(currentSkill == null && ReadyList.Count > 0) {
		
			OnSkill();
		
		}



	}

	void OnSkill(){

		currentSkill = ReadyList[0];

		for(int i = 1; i < ReadyList.Count; i++) {
		
			if(currentSkill.priority < ReadyList[i].priority) {
			
				currentSkill = ReadyList[i];
			
			}
		
		}

		currentSkill.StartSkill(timepoint);
	
	}

	void AddCooding(){
	
		coodingList.Add(currentSkill);
		currentSkill = null;
	
	}




}
