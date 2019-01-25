using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Tip_Panel : HudBase {

	public static Tip_Panel Instance = null;

	public List<ItemTip> listText;

	public GameObject objItem;

	public int deep = 10;
	public int number = 0;
	

	public override void Init ()
	{
		base.Init ();

		Instance = this;

		listText = new List<ItemTip>();

		for(int i = 0; i < deep; i++) {
		
			GameObject _objItem = GameObject.Instantiate(objItem) as GameObject;

			_objItem.transform.parent = this.tran;

			ItemTip _item = _objItem.GetComponent<ItemTip>();

			listText.Add(_item);
		
			_objItem.SetActive(false);

		
		}

	}

	public virtual void ShowTip(AvatarData _avatarData, string _info) {
	
		if(_avatarData.avatar != null && _avatarData.avatar.tran != null) {

			listText[number].gameObject.SetActive(true);

			listText[number].OnShow(_avatarData.avatar.tran, _info);

			number++;

			if(number >= deep) {
			
				number = 0;
			
			}

		
		}
	
	}


	public override void OnClose ()
	{
		Instance = null;
		base.OnClose ();
	}

}
