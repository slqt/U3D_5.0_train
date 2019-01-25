using UnityEngine;
using System.Collections;

public class BattleWinPanel : HudBase {

	public void OnNextBattle() {
	
		SoundManager.Instance.Play("Btn01");
		GameManager.Instance.OnBattle();
	
	}

	public void OnHome() {
	
		HudManager.Instance.Show(HudType.Map_Panel, HudStateType.Destroy);
		Application.LoadLevel("Main");
	
	}


}
