using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AvatarData {

	public int id = 0;
	public bool isDead = false;
	public AvatarType avatarType = AvatarType.None;
	public AvatarBase avatar = null;
	public PropertyModel properties;
	public List<SkillBase> skillList = new List<SkillBase>();
	public List<int> enemyList = new List<int>();

	public SkillBase bigSkill;

	public AvatarData(PropertyModel _property) {
	
		id = InstanceHepler.Get();
		avatarType = AvatarType.Player;
		properties = DataTools.GetProperty(_property);

		//Init skillsqence.
		for(int i = 1; i <= properties.skillCount; i++) {
			
			string _skilldata = SkillLoader.Instance.LoadSkill(properties.id + "_" + i);

			if(i < properties.skillCount)
				skillList.Add(new SkillBase(_skilldata, id));
			else {

				bigSkill = new SkillBase(_skilldata, id);
				InitSkillMsg msg = new InitSkillMsg();
				msg.avatarid = id;
				MessengerManager<InitSkillMsg>.Involve("InitBigSkill", msg);
				MessengerManager.AddListener("skill" + id, ShowBigSkill);
				MessengerManager<IncreaseEnergyMsg>.AddListener("IncreaseEnergy" + id, IncreaseEnergy);
			
			}
			
		}
	
	}

	public AvatarData() {
	
		id = InstanceHepler.Get();
		avatarType = AvatarType.Enemy;
		properties = DataTools.CreateRandomMonster();

		//Init skillsqence.
		for(int i = 0; i < properties.skillCount; i++) {

			int _id = 0;//Random.Range(0, 5);
			string _skilldata = SkillLoader.Instance.LoadSkill(_id.ToString());
			skillList.Add(new SkillBase(_skilldata, id));
			
		}

	}

	public void CreateView(Vector3 pos) {
	
		avatar = AvatarLoader.Instance.Load<AvatarBase>(properties.name);
		avatar.fightDistance = Mathf.Abs(pos.x) - Random.Range(0.0f, 1.0f);
		if(avatar.fightDistance < 0) avatar.fightDistance = Random.Range(0.1f, 0.3f);
		avatar.id = id;
		avatar.GameId = properties.id;
		pos.z = pos.y;
		avatar.tran.position = pos;

		//Init skillsqence.
		for(int i = 0; i < skillList.Count; i++) {

			avatar.skillSequence.AddSkill(skillList[i]);
			
		}
	
	}

	public void OnHit(int _outputAtk) {
	
		int damage = DataTools.GetDamage(properties, _outputAtk);

		properties.hp -= damage;

		Tip_Panel.Instance.ShowTip(this, "-" + damage.ToString());

		if(properties.hp <= 0) {
		
			isDead = true;
			avatar.OnDead();
			AvatarManager.Instance.Remove(id);

			BattleManager.Instance.Judage(avatarType);
		
		}
	
	}

	public void SetEnmey(List<int> _enmeyList) {
	
		enemyList.Clear();

		for(int i = 0; i < _enmeyList.Count; i++) {
		
			enemyList.Add(_enmeyList[i]);
		
		}
	
	}

	public void ShowBigSkill() {
	
		properties.energy = 0;
		AddEnergyMsg msg = new AddEnergyMsg();
		msg.energy = properties.energy;
		MessengerManager<AddEnergyMsg>.Involve("addEnergy" + id, msg);
	
	}

	public void IncreaseEnergy(IncreaseEnergyMsg msg) {
	
		properties.energy += msg.increase_energy;

		AddEnergyMsg _msg = new AddEnergyMsg();
		_msg.energy = properties.energy;
		MessengerManager<AddEnergyMsg>.Involve("addEnergy" + id, _msg);
	
	}
	                       

}

public enum AvatarType {

	None,
	Player,
	Enemy,
	Boss,

}

[System.Serializable]
public struct PropertyModel {

	public int id;
	public string name;
	public int exp;
	public int level;
	public int point;
	public int star;
	public int quality;
	public int hp;
	public int attack;
	public int armor;
	public float cirt;		  // cirt >= 0;
	public float crit_rate;   // 0 >= crite_rate <= 1.0
	public float dodge_rate;  // 0 >= dodage_rate <= 1.0
	public float speed;
	public float fightDistance;
	public int energy;
	public int skillCount;
	public int maxhp;

}

public struct DamageData {

	public int damage;

}
