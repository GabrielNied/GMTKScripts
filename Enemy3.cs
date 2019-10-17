using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    [SerializeField]
    private int numObjects = 20;
    public GameObject tiroExplode;

    private float enemyVel = 0.25f;

    private Vector3 posInicial;

    [SerializeField]
    private bool chegou = false;

    void Start()
    {
        InvokeRepeating("Shot", 1, 2);
    
        posInicial = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.farClipPlane / 2));
    }

    void Update()
    {
        MoveBh();
    }

    private void MoveBh()
    {
        if (!chegou)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), posInicial, (enemyVel + 1f) * Time.deltaTime);

            float distance = Vector2.Distance(transform.position, posInicial);
            if (distance <= 0)
            {
                chegou = true;
            }
        }
    }

    private void Shot()
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
}