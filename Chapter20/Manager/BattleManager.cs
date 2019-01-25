using UnityEngine;
using System.Collections.Generic;

public class BattleManager : Singleton<BattleManager> {

	public void Judage(AvatarType avatarType) {
	
		List<AvatarData> avatarList = AvatarManager.Instance.Get(avatarType);

		if(avatarList == null || avatarList.Count == 0) {
		
			if(avatarType == AvatarType.Player) {
			
				HudManager.Instance.Show(HudType.Battle_Faill_Panel, HudStateType.Destroy);

				//Add exp
				DataBase.Instance.AddExp(10);
			
			}else{
			
				HudManager.Instance.Show(HudType.Battle_Win_Panel, HudStateType.Destroy);
				GameManager.Instance.OpenNextBattle();

				List<AvatarData> _avatarList = AvatarManager.Instance.Get (AvatarType.Player);

				int star = 0;
				star = _avatarList.Count - 2;
				if(star <= 0) star = 1;

				SaveData.Instance.Save(star);

				DataBase.Instance.AddExp(50);
			
			}
		
		}

	}

}
