using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemTip : GameBase {

	public Transform tranFollow = null;

	public float moveHight;
	public float scaleX;
	public float moveX;

	public Vector3 movePos;

	public bool isStart = false;

	public Text showText;

	private Camera mainCamera;
	private float smooth = 1;

	private float moveTime = 1;

	public Transform tranMove;
	public int direction = 1;

	public Color color_tip;

	public override void Init ()
	{
		base.Init ();

	}


	public virtual void OnShow(Transform _tranFollow, string _info) {
	
		tranFollow = _tranFollow;

		if(tranFollow == null) {

			OnEnd();

			return;

		}

		color_tip = Color.red;
		color_tip.a = 0;
		showText.color = color_tip;

		showText.text = _info;


		moveHight = Random.Range(50, 100);
		moveX = Random.Range(10, 30);

		tran.position = Camera.main.WorldToScreenPoint(tranFollow.position);

		movePos = Vector3.zero;
		//movePos.y = 10;

		tranMove.localPosition = movePos;

		moveTime = 0;

		direction = 1;
		if(Random.value > 0.5f) direction = -1;

		isStart = true;
	
	}

	public virtual void OnEnd() {
	
		isStart = false;
		this.gameObject.SetActive(false);
	
	}

	public override void UpdateBehaviour ()
	{
		base.UpdateBehaviour ();

		if(!isStart) return;

		if(tranFollow != null) {


			tran.position = Camera.main.WorldToScreenPoint(tranFollow.position);

			movePos = Vector3.zero;
			movePos.y = moveHight;
			movePos.x = direction * scaleX;
			movePos.y = (Mathf.Cos(movePos.x/moveX + Mathf.PI + direction * Mathf.PI / 3) + 1) * moveHight;

			movePos.y += 30;

			tranMove.localPosition = movePos;

			color_tip.a = Mathf.Cos(moveTime * 1.5f + Mathf.PI) + 0.9f;

			showText.color = color_tip;

			//tranMove.localPosition = Vector3.Lerp(tranMove.localPosition, movePos, Time.deltaTime * smooth);

			moveTime += Time.deltaTime;

			scaleX = moveTime * moveX;

			if(scaleX > (moveX * Mathf.PI + moveX * Mathf.PI/3)) {
			
				OnEnd();
			
			}

		
		}else{
		
			OnEnd();
		
		}

	}

}
