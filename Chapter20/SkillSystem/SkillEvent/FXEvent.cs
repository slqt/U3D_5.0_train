using UnityEngine;
using System.Collections;

public class FXEvent : SkillEventBase {

	public override void Do (string parameter, int avatarId)
	{
		base.Do (parameter, avatarId);

		string[] _dataArray = parameter.Split(' ');

		AvatarData avatar = AvatarManager.Instance.Find(avatarId);

		if(avatar != null) {
		
			FxManager.Instance.ShowFx(_dataArray[0], avatar.avatar.tran);
		
		}

	}

}
