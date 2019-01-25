using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapButton : MonoBehaviour {

	public int id;
	public Button button;
	public Text tx_Level;
	public Image[] stars;

	// Use this for initialization
	void Start () {
		
		button = this.GetComponent<Button>();
		button.onClick.AddListener(OnBattle);
		
	}

	public void Init(int _id) {
	
		id = _id;
		tx_Level.text = id.ToString();

		LevelData mapInfo = SaveData.Instance.LevelList[id];

		for(int i = 0; i < stars.Length; i++) {
		
			if(i+1 <= mapInfo.star) {
			
				stars[i].color = Color.white;
			
			}else{
			
				stars[i].color = new Color(1, 1, 1, 0.3f);
			
			}
		
		}
	
	}

	void OnBattle() {
	
		GameManager.Instance.OnBattle(id);
	
	}
}
