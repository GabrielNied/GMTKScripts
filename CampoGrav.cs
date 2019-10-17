using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoGrav : MonoBehaviour {

    public float tempo;
    public bool held = false;

    private Vector3 originalScale;

    private GameObject campoGravManager;

	// Use this for initialization
	void Start () {
        campoGravManager = GameObject.Find("CampoGravManager");
        originalScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        tempo += Time.deltaTime;

        if (!held)
        {
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, tempo / 5);

            if (tempo >= 5)
            {
                campoGravManager.GetComponent<CampoGravManager>().quantidade--;
                Destroy(this.gameObject);
            }
        }
	}

    void OnTriggerStay2D(Collider2D coll)
    {
        coll.tag = "TiroAfetado";
        Vector2 distance = transform.position - coll.transform.position;
        Vector2 direction = distance;

        Rigidbody2D bulletRb = coll.transform.GetComponent<Rigidbody2D>();
        float bulletMass = 0.5f; // Arbitrario isso, é o "peso" da bala
        
        float magnitude = Mathf.Max(distance.sqrMagnitude, 0.5f);
        direction.Normalize();

        bulletRb.AddForce(direction * bulletMass / magnitude);
    }

}
