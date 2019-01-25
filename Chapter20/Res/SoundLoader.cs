using UnityEngine;
using System.Collections;

public class SoundLoader : LoaderBase<SoundLoader> {

	public SoundLoader() {
		
		loaderName = "Sound/";
		
	}

	public AudioClip LoadSound(string name) {
	
		return Load(name) as AudioClip;
	
	}


}
