using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour {

    public float transitionSpeed = 1f;

    private string sceneName;
	private Animator anim;

	void Start () {
		anim = this.gameObject.GetComponent <Animator> ();
		anim.speed = transitionSpeed;
	}

	public void animateExit (string name) {
		sceneName = name;
		anim.Play ("FadeOut");
	}

	// This is an Event called by the Animation FadeOut.
	private void changeScene () {
		SceneManager.LoadScene (sceneName);
	}
}
