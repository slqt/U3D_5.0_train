using UnityEngine;
using System.Collections;

public class GameManager : SingletonUnity<GameManager> {

	public int GameLevel = 0;
	public DifficultyType difficulty = DifficultyType.Normal;

	// Use this for initialization
	void Start () {

		DontDestroyOnLoad(this.gameObject);

		difficulty = (DifficultyType)PlayerPrefs.GetInt("difficulty");

		//load player data.
		DataBase.Instance.InitPlayer();

		//Load Level data.
		SaveData.Instance.Init();

		GameLevel = SaveData.Instance.currentLevel;

		//Load MainPanel
		HudManager.Instance.Show(HudType.Menu_Panel, HudStateType.Destroy);

	
	}

	public void OnBattle() {
	
		AvatarManager.Instance.Clear();
		
		//Load Scenes
		ChangeScene.loadHandler = Loaded;
		HudManager.Instance.Show(HudType.Loading_Panel);
		Application.LoadLevel("Battle");
	
	}

	public void OnBattle(int _level) {

		GameLevel = _level;
		SaveData.Instance.currentLevel = GameLevel;
	
		OnBattle();
	
	}

	public void OpenNextBattle() {
	
		GameLevel++;
	
	}

	public void Loaded() {
	
		//load scene.
		SceneLoader.Instance.LoadObject("scene_" + Random.Range(0, 5));

		//Open battle hud.
		HudManager.Instance.Show(HudType.Battle_Panel, HudStateType.Destroy);
		HudManager.Instance.AddShow(HudType.Tip_Panel);

		//load player.
		AvatarManager.Instance.CreatPlayer();

		//load monster.
		AvatarManager.Instance.CreatMonster(5);
	
	}                    

}


public enum DifficultyType {
	
	Normal,
	Medium,
	Hard,
	Extreme
}