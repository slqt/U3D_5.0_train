using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudBase : GameBase {

	public HudStateType state;
	public HudType _type;

	public float Z_deep = 0;

	public void OnEnable() {
	
		OnShow();
	
	}

	public void OnDisable() {
	
		OnClose();
	
	}

	public virtual void OnShow() {
	
	}

	public virtual void OnClose() {

		if(state == HudStateType.Hidden)
			this.gameObject.SetActive(false);
		else if(state == HudStateType.Destroy) {
			Remove();
		}

	}

	public virtual void OnRemove() {}

}

public enum HudStateType {

	None,
	Hidden,
	Show,
	Destroy,

}