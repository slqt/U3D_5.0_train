using UnityEngine;
using System.Collections.Generic;

public class AvatarManager : Singleton<AvatarManager> {

	private Dictionary<int, AvatarData> avatarDic = new Dictionary<int, AvatarData>();

	public void Clear() {
	
		avatarDic.Clear();
	
	}

	public void CreatPlayer() {
	
		List<PropertyModel> propertyList = DataBase.Instance.playerData.heroData;

		float space = 1.3f;
		Vector3 pos = new Vector3(-1, -0.7f, 0);
		
		for(int i = 0; i < propertyList.Count; i++) {
		
			AvatarData _avatarData = new AvatarData(propertyList[i]);

			if(_avatarData != null) {

				_avatarData.CreateView(pos);

				pos.x -= 0.8f;
				space *= -1;
				pos.y += space + Random.Range(0, 0.3f) * space * -1;
				
				Add(_avatarData);
			
			}
		
		}
	
	}

	public void CreatMonster(int count) {

		float space = 1.3f;
		Vector3 pos = new Vector3(1, -0.7f, 0);

		for(int i = 0; i < count; i++) {
		
			AvatarData _avatarData = new AvatarData();

			//Create view.
			if(_avatarData != null) {

				_avatarData.CreateView(pos);

				pos.x += 0.8f;
				space *= -1;
				pos.y += space + Random.Range(0, 0.3f) * space * -1;

				Add(_avatarData);
			
			}
		
		}
	
	}

	public void Add(AvatarData _avatarData) {
	
		if(!avatarDic.ContainsKey(_avatarData.id)) {
		
			avatarDic.Add(_avatarData.id, _avatarData);
		
		}
	
	}

	public void Remove(int id) {
	
		if(avatarDic.ContainsKey(id)) {
		
			avatarDic.Remove(id);
		
		}
	
	}

	public AvatarData Find(int id) {
	
		if(avatarDic.ContainsKey(id)) {
		
			return avatarDic[id];
		
		}

		Debug.Log("None avatar id:" + id);

		return null;
	
	}

	public AvatarData GetTarget(AvatarBase avatar) {

		AvatarData _avatarData = Find(avatar.id);

		if(_avatarData == null) return null;
	
		List<AvatarData> _avatarList = new List<AvatarData>(avatarDic.Values);
		
		if(_avatarList.Count <= 0) return null;

		AvatarType _avatarType = AvatarType.None;
		if(_avatarData.avatarType == AvatarType.Player) _avatarType = AvatarType.Enemy;
		else _avatarType = AvatarType.Player;

		AvatarData targetData = null;
		float minDistance = 10000;
		
		for(int i = 0; i < _avatarList.Count; i++) {
			
			if(_avatarList[i].avatarType == _avatarType) {
				
				float distance = Mathf.Abs(_avatarData.avatar.tran.position.x - _avatarList[i].avatar.tran.position.x) - 
								(_avatarData.avatar.radius + _avatarList[i].avatar.radius);


				if(distance < minDistance) {
					minDistance = distance;
					targetData = _avatarList[i];
				}
				
			}
			
		}
		
		return targetData;
	
	}

	public List<AvatarData> Get(AvatarType _avatarType) {
	
		List<AvatarData> _avatarList = new List<AvatarData>(avatarDic.Values);

		if(_avatarList.Count <= 0) return null;

		List<AvatarData> _typeList = new List<AvatarData>();

		for(int i = 0; i < _avatarList.Count; i++) {
		
			if(_avatarList[i].avatarType == _avatarType) {
			
				_typeList.Add(_avatarList[i]);
			
			}
		
		}

		return _typeList;
	
	}


}
