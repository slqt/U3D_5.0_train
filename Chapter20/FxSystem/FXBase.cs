using UnityEngine;
using System.Collections;

public class FXBase : GameBase {

	public float totalTime = 0;
	public bool isStart = false;
	
	public void OnStart(float _time) {
	
		isStart = true;
		totalTime = _time;
	
	}

	void OnEnd() {
	
		isStart = false;
		Remove();
	
	}

	void Update() {
	
		if(isStart) {
		
			totalTime -= Time.deltaTime;

			if(totalTime <= 0) {
			
				OnEnd();
			
			}
		
		}
	
	}

}
