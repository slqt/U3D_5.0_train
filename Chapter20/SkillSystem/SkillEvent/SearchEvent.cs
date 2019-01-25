using UnityEngine;
using System.Collections.Generic;

public class SearchEvent : SkillEventBase {

	private Dictionary<string, ActionDelegate> searchAction = new Dictionary<string, ActionDelegate>();

	private Dictionary<string, ActionDelegate> searchRange = new Dictionary<string, ActionDelegate>();

	private AvatarData avatar = null;
	private int avatarId;
	private string[] dataArray;

	private List<AvatarData> searchList = null;

	private List<int> searchID = new List<int>();

	public SearchEvent() {
	
		searchAction.Add("enemy", Enemy);
		searchAction.Add("friend", Friend);
		searchAction.Add("all", All);

		searchRange.Add("None", None);
		searchRange.Add("first", First);
		searchRange.Add("circle", Circle);
	
	}

	public override void Do (string parameter, int _avatarId)
	{
		base.Do (parameter, avatarId);
		avatarId = _avatarId;
		avatar = AvatarManager.Instance.Find(avatarId);

		if(avatar != null) {
		
			searchID.Clear();
			dataArray = parameter.Split(' ');
			searchAction[dataArray[0]]();
			searchRange[dataArray[1]]();

			if(searchID != null)
				avatar.SetEnmey(searchID);
		
		}

	}


	private void Enemy() {

		AvatarType _searchType = AvatarType.Player;

		if(avatar.avatarType == AvatarType.Player) {
		
			_searchType = AvatarType.Enemy;
		
		}

		searchList = AvatarManager.Instance.Get(_searchType);
	
	}

	private void Friend() {

		searchList = AvatarManager.Instance.Get(avatar.avatarType);
	
	}

	private void All() {
	
	}

	private void None() {
	
		for(int i = 0; i < searchList.Count; i++) {
		
			searchID.Add(searchList[i].id);
		
		}
	
	}

	private void First() {

		float min_distance = 1000;
		float current_distance = 0;
		AvatarData _avatar = null;
	
		for(int i = 0; i < searchList.Count; i++) {
		
			current_distance = Vector3.Distance(searchList[i].avatar.tran.position, avatar.avatar.tran.position);
			if(current_distance < min_distance) {
			
				min_distance = current_distance;
				_avatar = searchList[i];
			
			}
		
		}

		if(_avatar != null) {
		
			searchID.Add(_avatar.id);
		
		}

	}

	private void Circle() {

		float radius = 0;
		float.TryParse(dataArray[2], out radius);

		for(int i = 0; i < searchList.Count; i++) {
			
			float current_distance = Vector3.Distance(searchList[i].avatar.tran.position, avatar.avatar.tran.position);
			if(current_distance < radius) {

				searchID.Add(searchList[i].id);

			}
			
		}
	
	}

}
