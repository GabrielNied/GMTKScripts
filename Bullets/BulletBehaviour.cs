using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    private Rigidbody2D rb;
    [SerializeField]
    private float _speed;

    private GameObject gameManager;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        BulletMovement();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
	
	
	void Update () {
		
	}

    private void BulletMovement()
    {
        rb.AddForce(Vector2.down * _speed);
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

                // Warn Manager that its been killed.
                GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>().EnemyDestroyed();
            }

            if (col.transform.CompareTag("Boss"))
            {
                col.GetComponent<Boss1>().life -= 0.1f;
                Destroy(this.gameObject, 0.01f);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
