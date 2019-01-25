using UnityEngine;
using System.Collections;

public class KeepInAllLevels : MonoBehaviour {
	void Awake()
	{
		//在切换场景时不清除此游戏对象
		DontDestroyOnLoad(gameObject);
	}
}
