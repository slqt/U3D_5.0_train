using UnityEngine;
using System.Collections;

public interface ISkillEvent {

	void Do(string parameter, int avatarId);

}

public class SkillEventBase : ISkillEvent {

	protected AvatarBase avatar = null;

	public virtual void Do(string parameter, int avatarId) {
	

	
	}

}
