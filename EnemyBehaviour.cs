using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject localCanon;
    [SerializeField]
    private Animator anim;

    private float enemyVel = 1.5f;

    private Vector3 position;

	void Start () {
        InvokeRepeating("Shot", 1, 4);
        position = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(25, Screen.width-25), (Random.Range(Screen.height-25, Screen.height -125))));
        anim = GetComponent<Animator>();
    }
	

	void Update () {
        MoveBh();
        Animations();

        Debug.Log(string.Format("Nome {0} , PosicaoAtual: {1} , PosicaoFinal: {2} ", gameObject.name, transform.position.x, position.x));
    }

    private void MoveBh()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), position, enemyVel * Time.deltaTime);
        
    }

    private void Shot()
    {

        Instantiate(Bullet, localCanon.transform.position, Quaternion.Euler(0, 0, 90));
    }

    private void Animations()
    {
        if(Mathf.Ceil(transform.position.x) > Mathf.Ceil(position.x))
        {
            anim.SetBool("Left", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Right", false);
        }

        if (Mathf.Ceil(transform.position.x) == Mathf.Ceil(position.x))
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
        }

        if (Mathf.Ceil(transform.position.x) < Mathf.Ceil(position.x))
        {
            anim.SetBool("Right", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Left", false);
        }
    }
}
