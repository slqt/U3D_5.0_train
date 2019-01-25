using UnityEngine;
using System.Collections.Generic;

public class HudManager : SingletonUnity<HudManager> {

	private Dictionary<int, HudBase> hudDic = new Dictionary<int, HudBase>();

	public List<int> openList = new List<int>();

	private Transform tran;

	void Awake() {
	
		tran = this.transform;
	
	}

	public void CloseAll(){
		
		//Close all
		int _tempid = 0;
		for(int i = 0; i < openList.Count; i++) {
		
			_tempid = openList[i];
			if(hudDic.ContainsKey(_tempid)) {
			
				hudDic[_tempid].gameObject.SetActive(false);

				if(hudDic[_tempid].state == HudStateType.Destroy) {

					hudDic.Remove(_tempid);
					
				}
			
			}

		
		}
		
	}

	public void Show(HudType _hudType, HudStateType _state = HudStateType.Hidden) {

		CloseAll();
		int id = GetPanel(_hudType);
		hudDic[id].state = _state;
		hudDic[id].gameObject.SetActive(true);
		openList.Add(id);
	
	}

	public void AddShow(HudType _hudType, HudStateType _state = HudStateType.Destroy) {
	
		int id = GetPanel(_hudType);
		hudDic[id].state = _state;
		hudDic[id].gameObject.SetActive(true);
		openList.Add(id);
	
	}

	public int GetPanel(HudType _hudType) {

		//find exsit.
		foreach(KeyValuePair<int, HudBase> tempHud in hudDic) {
		
			if(tempHud.Value._type == _hudType) {
			
				return tempHud.Key;
			
			}
		
		}

		int id = InstanceHepler.Get();

		HudBase _hudbase = HudLoader.Instance.Load<HudBase>(_hudType.ToString());
		_hudbase._type = _hudType;

		if(_hudbase != null) {

			_hudbase.tran.parent = tran;
			_hudbase.tran.localPosition = Vector3.zero;
			_hudbase.tran.localEulerAngles = Vector3.zero;
			_hudbase.tran.localScale = Vector3.one;

			if(!hudDic.ContainsKey(id)) {
			
				hudDic.Add(id, _hudbase);
			
			}else{
			
				Debug.Log("exsit instance id : " + id);
			
			}

		}else{
		
			id = -1;
		
		}

		return id;
	
	}

}

public enum HudType {

	None,
	Menu_Panel,
	Setting_Panel,
	Battle_Panel,
	Battle_Faill_Panel,
	Battle_Win_Panel,
	Loading_Panel,
	Map_Panel,
	Tip_Panel,

}
