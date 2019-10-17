using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{

    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject localCanon;
    [SerializeField]
    private Animator anim;

    private float enemyVel = 0.25f;

    private Vector3 posInicial, pos1, pos2;

    [SerializeField]
    private bool chegou = false, indo;

    void Start()
    {
        InvokeRepeating("Shot", 1, 4);
        posInicial = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height - 75, Camera.main.farClipPlane / 2));
        pos1 = Camera.main.ScreenToWorldPoint(new Vector3(75, Screen.height - 75, Camera.main.farClipPlane / 2));
        pos2 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 75, Screen.height - 75, Camera.main.farClipPlane / 2));
        anim = GetComponent<Animator>();

        indo = (Random.value > 0.5f);
    }


    void Update()
    {
        MoveBh();          
    }

    private void MoveBh()
    {
        if (!chegou)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), posInicial, (enemyVel+1f) * Time.deltaTime);
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
            //transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(enemyVel * Time.time) + 1.0f) / 2.0f);

           // this.transform.position = new Vector3 (posInicial.x + Mathf.Sin (Time.time) * 2, posInicial.y, posInicial.z);
        }
    }

    private void Shot()
    {
        Instantiate(Bullet, localCanon.transform.position, Quaternion.Euler(0, 0, 90));
    }
}
