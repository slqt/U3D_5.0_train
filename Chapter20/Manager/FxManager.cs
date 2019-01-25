using UnityEngine;
using System.Collections;

public class FxManager : Singleton<FxManager> {

	public void ShowFx(string name, Vector3 pos) {
	
		FXBase _fxbas = FxLoader.Instance.Load<FXBase>(name);

		if(_fxbas != null) {
		
			_fxbas.tran.position = pos;
			_fxbas.OnStart(1);
		
		}
	
	}

	public void ShowFx(string name, Transform tranParent) {
	
		FXBase _fxbas = FxLoader.Instance.Load<FXBase>(name);

		if(_fxbas != null) {
		
			_fxbas.tran.parent = tranParent;
			_fxbas.tran.localPosition = Vector3.zero;
			_fxbas.OnStart(1);
		
		}
	
	}

}
