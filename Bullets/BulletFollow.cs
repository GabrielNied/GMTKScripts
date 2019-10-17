using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFollow : MonoBehaviour {

    private float  auxSpeed = 3f;

    [SerializeField]
    private GameObject gameManager, playerRef;

    private Rigidbody2D bulletRb;

    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        bulletRb = this.transform.GetComponent<Rigidbody2D>();
        //InvokeRepeating("Move", 0.1f, 3f);

        //Move();
    }

    void Update()
    {
        if(playerRef == null)
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
        }

        OlhaProPlayerSeuMerda();
        Move2();
    }

    void OlhaProPlayerSeuMerda()
    {
        Vector3 difference = playerRef.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void Move()
    {
        Vector2 distance = playerRef.transform.position - this.transform.position;
        Vector2 direction = distance;
        
        float bulletMass = 1f;

        float magnitude = distance.sqrMagnitude;
        direction.Normalize();

        bulletRb.AddForce(direction * bulletMass * auxSpeed / magnitude);
    }

    private void Move2()
    {
        float step = auxSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, playerRef.transform.position, step);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().lifePrincipal -= 1;
            Destroy(col.gameObject, 0.01f);
            Destroy(gameObject, 0.01f);
        }

        if (this.tag == "TiroAfetado")
        {
            if (col.transform.CompareTag("Enemy"))
            {
                if (col.gameObject.name == "Nave Inimigo 2(Clone)")
                {
                    col.transform.DetachChildren();
                }
                Destroy(col.gameObject, 0.01f);
                Destroy(this.gameObject, 0.01f);

                GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>().EnemyDestroyed();
            }

            if (col.transform.CompareTag("Boss"))
            {
                col.GetComponent<Boss1>().life -= 0.1f;
                Destroy(this.gameObject, 0.01f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.CompareTag("Sucker"))
        {
            Move();
        }
    }
}
