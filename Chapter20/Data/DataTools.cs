using UnityEngine;
using System.Collections;

public class DataTools {

	//public int LevelFunction(                           

	public static int LevelExp(int _level) {

		return _level * 1000;

	}

	public static int GetExp(int _mlevel, int _targetLevel) {
	
		return Mathf.FloorToInt(_targetLevel * 100 * ((float)_targetLevel / _mlevel));
	
	}

	public static float GetRandom() {
	
		return Random.value;
	
	}

	public static PropertyModel CreateRandomMonster() {

		int _dificulty = (int)GameManager.Instance.difficulty;
		int _level = GameManager.Instance.GameLevel;

		PropertyModel property = new PropertyModel();
		property.level = _level + _dificulty;
		property.star = Random.Range(0, _dificulty);
		property.quality = Random.Range(0, _dificulty * 3);

		property.name = "monster_" + Random.Range(0, 15);

		property.hp += property.hp + property.level * 100 + property.star * 500 + property.quality * 300;
		property.maxhp = property.hp;
		property.attack += property.level * 10;
		property.cirt += property.level * 0.001f;
		property.crit_rate += property.level * 0.001f;
		property.armor += property.level * 20;
		property.dodge_rate += property.level * 0.001f;
		property.armor += property.level * 1;

		property.skillCount = Random.Range(1, 3);
		
		return property;
		
	}

	public static PropertyModel GetProperty(PropertyModel property) {

		//property.exp = PlayerPrefs.GetInt("Exp_" + property.id);
		property.level = property.exp / 100;
		property.hp += property.hp + property.level * 100 + property.star * 500 + property.quality * 300;
		property.maxhp = property.hp;
		property.attack += property.level * 10;
		property.cirt += property.level * 0.001f;
		property.crit_rate += property.level * 0.001f;
		property.armor += property.level * 7;
		property.dodge_rate += property.level * 0.001f;
		property.armor += property.level * 1;

		return property;
	
	}

	public static int GetOutPutAtk(PropertyModel property) {

		int outputAtk = property.attack;

		if(GetRandom() < property.crit_rate) {

			outputAtk += Mathf.FloorToInt(property.attack * property.cirt);
		
		}else{
		
			outputAtk *= 2;
		
		}

		return outputAtk;
	
	}

	public static int GetDamage(PropertyModel property, int outputAtk) {
	
		int damage = 0;

		if(GetRandom() > property.dodge_rate) {
		
			damage = outputAtk - property.armor;
		
		}

		if(damage <= 0) {
		
			damage = 1;
		
		}

		return damage;
	
	}

}
