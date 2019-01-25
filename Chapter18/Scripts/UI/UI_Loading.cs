using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Loading : MonoBehaviour {
	public string level2Load;
	public Slider slider;
	private AsyncOperation op;
	private float progress;
	private float speed = 0.5f;
	void Start()
	{
		op = Application.LoadLevelAsync(level2Load);
		progress = 0;
		slider.value = progress;
	}

	void Update()
	{
		if(progress<op.progress)
		{
			progress += speed * Time.deltaTime;
		}
		slider.value = progress;

		if(progress>=1)
		{
			UI_Manager.Instance.PageTransition( UIState.Gaming);
		}
	}
}
