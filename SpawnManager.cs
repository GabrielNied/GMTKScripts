using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour {

    public GameObject bossBarraHP;

    [Header("Wave Settings")]
    public float windUpDelay;
    public UnityEngine.UI.Text bottomTextLeft, bottomTextRight;
    public SpawnWave[] gameWaves;
    
    private int currentWave = -1;
    private int enemiesSpawned = 0, enemiesKilled = 0;

    private float windUpTimer = 0f, intervalTimer = 0f;
    private bool spawning = false, endless = false;

    [Space(5)]
    [Header("Survival Mode Settings")]
    public GameObject enemy1Prefab; // Deixa assim (:
    public GameObject enemy2Prefab, enemy3Prefab, CanvasPai;
    public float timeEnemy1Spawn = 2, timeEnemy2Spawn = 4, timeEnemy3Spawn = 6;

    [Space(5)]
    [Header("Tutorial Stuff")]
    public bool playTutorial = true;
    public TutorialPhase[] tutorialSequence;
    public GameObject godObject, textObject;

    private int currentTutorialScreen = -1;
    private bool tutorialFinished = false;

    void Start () {
        CanvasPai = GameObject.Find("Canvas");
    }

	void Update () {
        if (playTutorial && !tutorialFinished)
        {
            if (currentTutorialScreen == -1)
            {
                currentTutorialScreen += 1;
                godObject.GetComponent<UnityEngine.UI.Image>().sprite = tutorialSequence[currentTutorialScreen].godImage;
                textObject.GetComponent<UnityEngine.UI.Text>().text = tutorialSequence[currentTutorialScreen].whatToSay;
            } else if (currentTutorialScreen < tutorialSequence.Length)
            {
                foreach (KeyCode key in tutorialSequence[currentTutorialScreen].keysToPress)
                {
                    if (Input.GetKeyDown(key))
                    {
                        currentTutorialScreen += 1;
                        if (currentTutorialScreen < tutorialSequence.Length) { 
                            godObject.GetComponent<UnityEngine.UI.Image>().sprite = tutorialSequence[currentTutorialScreen].godImage;
                            textObject.GetComponent<UnityEngine.UI.Text>().text = tutorialSequence[currentTutorialScreen].whatToSay;
                        }

                        if (currentTutorialScreen == 2)
                            GameObject.FindGameObjectWithTag("CampoGravManager").GetComponent<CampoGravManager>().tutorialEnded = true;

                        break;
                    }
                }
            } else
            {
                tutorialFinished = true;
                godObject.SetActive(false);
                textObject.transform.parent.gameObject.SetActive(false);
            }

        } else
        {
            if (!spawning && !endless)
            {
                windUpTimer += Time.deltaTime;
                if (windUpTimer >= windUpDelay)
                {
                    currentWave += 1;

                    if (currentWave == gameWaves.Length)
                    {
                        endless = true;
                        InvokeRepeating("SpawnEnemy1", timeEnemy1Spawn, timeEnemy1Spawn);
                        InvokeRepeating("SpawnEnemy2", timeEnemy2Spawn, timeEnemy2Spawn);
                        InvokeRepeating("SpawnEnemy3", timeEnemy3Spawn, timeEnemy3Spawn);
                        bottomTextLeft.text = "Survived:";
                        enemiesKilled = 0;

                    }
                    else
                    {
                        spawning = true;
                        windUpTimer = 0;
                        enemiesSpawned = 0;
                        enemiesKilled = 0;
                        bottomTextRight.text = gameWaves[currentWave].totalEnemies.ToString();
                    }
                }
            }
            else if (!endless)
            {
                if (intervalTimer >= gameWaves[currentWave].enemyInterval &&
                    enemiesSpawned < gameWaves[currentWave].totalEnemies)
                {
                    intervalTimer = 0;
                    float roll = Random.Range(0f, 1f);
                    foreach (EnemyType enemy in gameWaves[currentWave].enemies)
                    {
                        if (enemiesSpawned == gameWaves[currentWave].totalEnemies)
                            break;

                        if (roll <= enemy.chanceOfEnemy)
                        {
                            SpawnEnemyAtLocation(enemy.enemyPrefab, enemy.whereToSpawn);
                            enemiesSpawned += 1;
                        }
                    }
                    if (gameWaves[currentWave].bossFight == true)
                    {
                        bossBarraHP.SetActive(true);
                    }

                }
                else
                {
                    intervalTimer += Time.deltaTime;
                }

                if (gameWaves[currentWave].totalEnemies - enemiesKilled == 0)
                {
                    spawning = false;
                    bottomTextRight.text = "-";
                }
            }
        }
	}

    void SpawnEnemy1() {
        
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height+50, Camera.main.farClipPlane / 2));
        GameObject Enemy1 = Instantiate(enemy1Prefab) as GameObject;
        Enemy1.transform.position = screenPosition;
        Enemy1.transform.SetParent(CanvasPai.GetComponent<Transform>());
    }
    void SpawnEnemy2()
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height + 50, Camera.main.farClipPlane / 2));
        GameObject Enemy2 = Instantiate(enemy2Prefab) as GameObject;
        Enemy2.transform.position = screenPosition;
        Enemy2.transform.SetParent(CanvasPai.GetComponent<Transform>());
    }
    void SpawnEnemy3()
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height + 50, Camera.main.farClipPlane / 2));
        GameObject Enemy3 = Instantiate(enemy3Prefab) as GameObject;
        Enemy3.transform.position = screenPosition;
        Enemy3.transform.SetParent(CanvasPai.GetComponent<Transform>());
    }

    void SpawnEnemyAtLocation (GameObject prefabToSpawn, Location whereToSpawn)
    {
        Vector3 screenPosition = new Vector3();
        switch (whereToSpawn)
        {
            case Location.Random:
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height + 50, Camera.main.farClipPlane / 2));
                break;

            case Location.CenterOfScreen:
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height + 50, Camera.main.farClipPlane / 2));
                break;

            default:
                break;
        }
        
        GameObject Enemy = Instantiate(prefabToSpawn) as GameObject;
        Enemy.transform.position = screenPosition;
        Enemy.transform.SetParent(CanvasPai.GetComponent<Transform>());
    }

    public void EnemyDestroyed()
    {
        enemiesKilled += 1;

        if (endless)
        {
            bottomTextRight.text = enemiesKilled.ToString();
        } else
        {
            bottomTextRight.text = (gameWaves[currentWave].totalEnemies - enemiesKilled).ToString();
        }
    }
}

[System.Serializable]
public class SpawnWave {
    [Range(0f,2f)]
    public float enemyInterval;
    public int totalEnemies;

    public EnemyType[] enemies;
    public bool bossFight = false;
}

[System.Serializable]
public class EnemyType {
    [Range(0f, 1f)]
    public float chanceOfEnemy;
    public Location whereToSpawn;
    public GameObject enemyPrefab;
}

[System.Serializable]
public class TutorialPhase
{
    public Sprite godImage;
    public string whatToSay;
    public KeyCode[] keysToPress;
}

public enum Location { Random, CenterOfScreen }