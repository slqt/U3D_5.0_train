using UnityEngine;
using System.Collections;

public class SingletonUnity<T> : MonoBehaviour where T : MonoBehaviour {
	
	protected static T instance = null;
	
	public static T Instance {
		
		get {
			
			if(instance == null) {
				
				instance = (T)FindObjectOfType(typeof(T));
				
				if(instance == null){
					
					Debug.LogError("An instance of" + typeof(T) + "is needed in the scene, but there is none.");
					
				}
				
			}
			
			return instance;
			
		}
		
	}
	
}