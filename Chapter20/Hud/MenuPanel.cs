using UnityEngine;
using System.Collections;

public class MenuPanel : HudBase {

	public override void Init ()
	{

	}

	public override void OnShow ()
	{
		base.OnShow ();
	}

	public override void OnClose ()
	{
		base.OnClose ();
	}

	public override void OnRemove ()
	{
		base.OnRemove ();
	}

	public void OnStart_Handler() {
	
		SoundManager.Instance.Play("Btn01");
		GameManager.Instance.OnBattle();
	
	}

	public void OnMap_Handler() {
	
		HudManager.Instance.Show(HudType.Map_Panel, HudStateType.Destroy);
	
	}

	public void OnSetting_Handler() {
	

	
	}

}
