using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SkillButton : MonoBehaviour {

	public Button button;

	public int avatarid = 0;

	public int id;

	public Image image_icon;
	public Image image_energy;

	// Use this for initialization
	void Start () {
	
		button = this.GetComponent<Button>();
		button.onClick.AddListener(UseSkill);

	}

	public void Init(InitSkillMsg msg) {
	
		avatarid = msg.avatarid;
		MessengerManager<AddEnergyMsg>.AddListener("addEnergy" + avatarid, UpdateEnergy);
	
	}

	public void UseSkill() {
	
		if(image_energy.fillAmount == 1)
			MessengerManager.Involve("skill" + avatarid);
	
	}

	void UpdateEnergy(AddEnergyMsg _msg) {
	
		image_energy.fillAmount = 1 - (float)_msg.energy/1000;
	
	}

	void OnDestroy() {


		MessengerManager<AddEnergyMsg>.RemoveListener("addEnergy" + avatarid, UpdateEnergy);
	
	}

}
