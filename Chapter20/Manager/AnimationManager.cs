using UnityEngine;
using System.Collections;

public class AnimationManager : SingletonUnity<AnimationManager> {

	public void Play(Animator animator, string playname) {
	
		animator.Play(playname);
	
	}

}
