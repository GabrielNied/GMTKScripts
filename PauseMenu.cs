using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

	private GameObject menu, tempo, gameManager;
	private bool paused = false, despausou = false;
	private float tempoVolta = 1;

	public List<GameObject> botoes;
	private GameObject criado, canvas;
	private Transform botao;

	private int opcoes, posicao;

    [SerializeField]
    private bool mouseOver = false;
    void Start ()
	{
		menu = GameObject.Find ("Canvas/PauseMenuManager/MenuPanel");
		tempo = GameObject.Find ("Canvas/PauseMenuManager/TempoVolta");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        menu.SetActive (false);
		tempo.SetActive (false);

		canvas = GameObject.Find ("Canvas/PauseMenuManager");
		botao = canvas.transform.Find ("MenuPanel");

		for (int i = 0; i < botao.childCount; i++){
			GameObject filhoBotao = botao.transform.GetChild (i).gameObject;
			botoes.Add(filhoBotao);
			opcoes++;
		}
	}



	void Update ()
	{

		if (gameManager.GetComponent<GameManager>().currentState == GameManager.GameState.Playing) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				paused = !paused;
				if (!paused) {
					despausou = true;
				}
			}

			if (paused) {
				menu.SetActive (true);
				tempo.SetActive (false);
				Time.timeScale = 0;
				tempoVolta = 0.6f;
				AudioListener.pause = true;
				PosicoesMenu ();
			}

			if (!paused) {
				menu.SetActive (false);
				AudioListener.pause = false;
			}

			if (despausou && !paused) {
				Volta ();
			}
		}
	}

	void PosicoesMenu(){

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (posicao < opcoes - 2)
            {
                posicao++;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (posicao > 0)
            {
                posicao--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < posicao; i++)
            {
                botao.transform.GetChild(posicao).GetComponent<Button>().onClick.Invoke();
            }
        }
        if (!mouseOver)
        {
            botoes[posicao].GetComponent<Button>().Select();
        }
        else
        {
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
    }

    public void MouseOver(int posicaoBotao)
    {
        mouseOver = true;
        posicao = posicaoBotao;
    }

    public void MouseExit()
    {
        mouseOver = false;
    }
    void Volta ()
	{
		tempoVolta += Time.deltaTime * 10;

		if (tempoVolta < 3) {
			Time.timeScale = 0.1f;
			tempo.SetActive (true);
			tempo.GetComponent<Text>().text = "" + tempoVolta.ToString ("f0");
			;
		}

		if (tempoVolta >= 3) {
			Time.timeScale = 1;
			tempo.SetActive (false);
			despausou = false;
		}
	}

	public void Back ()
	{
		paused = false;
		despausou = true;
	}

	public void Restart ()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    public void OtherScene(string otherScene)
    {
        SceneManager.LoadScene(otherScene);
    }

    public void ExitGame ()
	{
		Application.Quit ();
	}
}
