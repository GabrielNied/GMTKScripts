using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int lifePrincipal = 1;

    public bool bossMorto = false;

    public enum GameState
    {
        Readying,
        Playing,
        GameOver
    }

    public GameState currentState = GameState.Playing;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (lifePrincipal <= 0)
        {
            Lose();
        }

        if (bossMorto)
        {
            Victory();
        }
	}

    public void Victory()
    {
        GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneSwapper>().animateExit("Victory");
    }

    public void Lose()
    {
        GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneSwapper>().animateExit("Lose");
    }

}
