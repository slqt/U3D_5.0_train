using UnityEngine;
using System.Collections;

public class DamageEvent : SkillEventBase {

	public override void Do (string parameter,  int avatarId)
	{
		base.Do (parameter, avatarId);

		AvatarData _avatar = AvatarManager.Instance.Find(avatarId);

		if(_avatar != null && _avatar.enemyList.Count > 0) {
		
			for(int i = 0; i < _avatar.enemyList.Count; i++) {
			
				AvatarData _tempAvatar = AvatarManager.Instance.Find(_avatar.enemyList[i]);

				if(_tempAvatar != null) {
				
					int outputatk = DataTools.GetOutPutAtk(_avatar.properties);

					_tempAvatar.OnHit(outputatk);
				
				}
			
			}
		
		}

	}
}
