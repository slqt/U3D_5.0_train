using UnityEngine;
using System.Collections.Generic;

public class DataBase : SingletonUnity<DataBase> {

	public PlayerData playerData;

	public List<TextAsset> skillInfo;

	public void InitPlayer() {
	
		for(int i = 0; i < playerData.heroData.Count; i++) {

			PropertyModel _property = playerData.heroData[i];
			
			_property.exp = PlayerPrefs.GetInt("Exp_" + _property.id);

			playerData.heroData[i] = _property;

			
		}
	
	}

	public void AddExp(int _exp) {
	
		for(int i = 0; i < playerData.heroData.Count; i++) {
		

			PropertyModel _property = playerData.heroData[i];

			_property.exp += _exp;

			playerData.heroData[i] = _property;

			PlayerPrefs.SetInt("Exp_" + _property.id, _property.exp);
		
		}
	
	}

}
