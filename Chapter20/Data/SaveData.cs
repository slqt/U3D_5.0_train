using UnityEngine;
using System.Collections.Generic;

public class SaveData : Singleton<SaveData> {

	private List<LevelData> levelList = new List<LevelData>();

	public int currentLevel = 0;

	public int maxlevel = 0;

	public List<LevelData> LevelList {
	
		get{ return levelList; }
	
	}

	public SaveData() {
	
		string data = PlayerPrefs.GetString(GameManager.Instance.difficulty.ToString() + "LevelData");
		
		string[] dataArray = data.Split(';');
		
		if(dataArray.Length > 0) {
			
			for(int i = 0; i < dataArray.Length; i++) {
				
				levelList.Add(new LevelData(dataArray[i]));
				
			}
			
			currentLevel = levelList.Count - 1;
			
		}else{
			
			NewLevel();
			currentLevel = 0;
			
		}

		maxlevel = currentLevel;
	
	}

	public void Init() {}


	public void Save(int star) {
	
		levelList[currentLevel].star = star;

		if(currentLevel == maxlevel) {
		
			NewLevel();
			maxlevel++;
		
		}

		currentLevel++;
		GameManager.Instance.GameLevel = currentLevel;

		Save();
	
	}

	public void Save() {
	
		string _saveString = "";

		for(int i = 0; i < levelList.Count; i++) {
		
			_saveString += levelList[i].GetString();

			if(i != levelList.Count-1){
			
				_saveString += ";";

			}
		
		}

		PlayerPrefs.SetString(GameManager.Instance.difficulty.ToString() + "LevelData", _saveString);
	
	}

	public void NewLevel() {
	
		LevelData _levelData = new LevelData(LevelList.Count + ",0");

		levelList.Add(_levelData);
	
	}

	public void Clear() {
	
		PlayerPrefs.SetString(GameManager.Instance.difficulty.ToString() + "LevelData", "");
	
	}


}

public class LevelData {

	public int id;
	public int star;

	public LevelData(string _data) {
	
		string[] _dataArray = _data.Split(',');

		if(_dataArray.Length > 0)
			int.TryParse(_dataArray[0], out id);
		else
			id = 0;

		if(_dataArray.Length > 1)
			int.TryParse(_dataArray[1], out star);
		else
			star = 0;
	
	}

	public string GetString() {
	
		return id.ToString() + "," + star.ToString();
	
	}

}
