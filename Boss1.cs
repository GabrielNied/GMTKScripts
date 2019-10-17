using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour
{

    [SerializeField]
    public GameObject Bullet, BulletFollow, localCanon1, localCanon2, localCanon3;

    private GameObject lifeBar, gameManager;
    private float enemyVel = 0.25f;

    private Vector3 posInicial, pos1, pos2;

    [SerializeField]
    private bool chegou = false, indo;

    public float life = 1f;

    [SerializeField]
    private int numObjects = 20;
    public GameObject tiroExplode;

    [SerializeField]
    private Animator anim;



    void Start()
    {
        lifeBar = GameObject.Find("Canvas/BossHP");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        InvokeRepeating("Shot", 2.38f, 2.38f);
        InvokeRepeating("ShotFollow", 6.13f, 4.13f);
        InvokeRepeating("ShotExplode", 10.55f, 9.75f);

        posInicial = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height - 75, Camera.main.farClipPlane / 2));
        pos1 = Camera.main.ScreenToWorldPoint(new Vector3(75, Screen.height - 75, Camera.main.farClipPlane / 2));
        pos2 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 75, Screen.height - 75, Camera.main.farClipPlane / 2));

        indo = (Random.value > 0.5f);

        anim = GetComponent<Animator>();
    }


    void Update()
    {
        MoveBh();
        
        lifeBar.GetComponent<Image>().fillAmount = life;

        Dead();

    }

    private void MoveBh()
    {
        if (!chegou)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), posInicial, (enemyVel + 1f) * Time.deltaTime);
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
            anim.SetBool("Idle", true);
            float distance = Vector2.Distance(transform.position, posInicial);
            if (distance <= 0)
            {
                chegou = true;
            }
        }
        if (chegou)
        {
            if (indo)
            {
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), pos1, (enemyVel + 1f) * Time.deltaTime);
                anim.SetBool("Left", true);
                anim.SetBool("Right", false);
                anim.SetBool("Idle", false);
                float distance1 = Vector2.Distance(transform.position, pos1);
                if (distance1 <= 0)
                {
                    indo = false;
                }
            }
            if (!indo)
            {
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), pos2, (enemyVel + 1f) * Time.deltaTime);
                anim.SetBool("Left", false);
                anim.SetBool("Right", true);
                anim.SetBool("Idle", false);
                float distance2 = Vector2.Distance(transform.position, pos2);
                if (distance2 <= 0)
                {
                    indo = true;
                }
            }
        }
    }

    private void Shot()
    {
        Instantiate(Bullet, localCanon1.transform.position, Quaternion.Euler(0, 0, 90));
        Instantiate(Bullet, localCanon2.transform.position, Quaternion.Euler(0, 0, 90));
    }

    private void ShotFollow()
    {
        Instantiate(BulletFollow, localCanon3.transform.position, Quaternion.identity);
    }

    private void ShotExplode()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            int a = 360 / numObjects * i;
            Vector3 pos = RandomCircle(center, 1.0f, a);
            GameObject ExplosionBullets = Instantiate(tiroExplode, pos, Quaternion.identity) as GameObject;
            ExplosionBullets.transform.SetParent(this.gameObject.GetComponent<Transform>());

        }

    }

    Vector3 RandomCircle(Vector3 center, float radius, int a)
    {
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    private void Dead()
    {
        if(life <= 0f)
        {
            gameManager.GetComponent<GameManager>().bossMorto = true;
            Destroy(this.gameObject);
        }
    }
}
