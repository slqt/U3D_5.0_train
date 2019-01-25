using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapPanel : HudBase {

	public Button btnBack;
	public MapButton mapItem;
	public GameObject objMapItem;
	public Transform tranMapRoot;
	public RectTransform rectTransform;
	private float MaxLength = 1000;
	private float space = 50;
	private int count;

	public override void Init ()
	{
		base.Init ();

		btnBack.onClick.AddListener(OnClick_Back);

	}
	
	public override void OnShow ()
	{
		base.OnShow ();

		Vector3 _pos = mapItem.transform.position;
		count = SaveData.Instance.LevelList.Count;
		_pos.x = 50;

		MaxLength = (space + count * space) * count / 2;

		if(rectTransform.sizeDelta.x < MaxLength) {
			
			Vector2 vec2 = rectTransform.sizeDelta;
			vec2.x = MaxLength;
			rectTransform.sizeDelta = vec2;
			
		}

		for(int i = 0; i < count; i++) {
			
			GameObject obj = GameObject.Instantiate(objMapItem) as GameObject;
			obj.SetActive(true);
			obj.transform.parent = tranMapRoot;
			
			_pos.x += 50 * i;
			
			_pos.y = Mathf.Sin(_pos.x) * 100;
			
			obj.transform.localPosition = _pos;
			
			obj.GetComponent<MapButton>().Init(i);
			
		}

	}

	private void OnClick_Back() {
	
		HudManager.Instance.Show(HudType.Menu_Panel);
	
	}
}
