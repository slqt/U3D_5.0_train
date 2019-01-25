using UnityEngine;
using System.Collections;

public class GameBase : MonoBehaviour {

	public int id;
	public int GameId;

	public Transform tran = null;

	public static bool isUpdate = true;

	void Awake() {
	
		tran = this.transform;
	
	}

	void Start() {

		Init ();

	}

	void Update() {
	
		if(isUpdate) UpdateBehaviour();

		UpdateAlways();
	
	}

	public virtual void UpdateBehaviour(){}

	public virtual void UpdateAlways() {}
	
	public virtual void Init() {}

	public virtual void Remove() {
	
		Destroy(this.gameObject);
	
	}

}
