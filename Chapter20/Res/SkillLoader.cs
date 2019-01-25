using UnityEngine;
using System.Collections;

public class SkillLoader : LoaderBase<SkillLoader> {

	public SkillLoader() {
	
		loaderName = "SkillData/Skill_";
	
	}

	public string LoadSkill(string name) {
	
		return (Load(name) as TextAsset).text;
	
	}

}
