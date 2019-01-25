using UnityEngine;
using System.Collections.Generic;

public class SkillSystem : Singleton<SkillSystem> {

	public Dictionary<string, ISkillEvent> ActionDic = new Dictionary<string, ISkillEvent>(); 

	public SkillSystem() {
	
		ActionDic.Add("animation", new AnimEvent());
		ActionDic.Add("sound", new SoundEvent());
		ActionDic.Add("search", new SearchEvent());
		ActionDic.Add("buff", new SearchEvent());
		ActionDic.Add("damage", new DamageEvent());
		ActionDic.Add("shoot", new ShootEvent());
		ActionDic.Add("fx", new FXEvent());
	
	}

	public void DoEvent(string strEvent, string skillInfo, int _avatarId) {
	
		if(ActionDic.ContainsKey(strEvent)){
		
			ActionDic[strEvent].Do(skillInfo, _avatarId);
		
		}else{
		
			Debug.LogError("Bad event string : " + strEvent);
		
		}
	
	}

}