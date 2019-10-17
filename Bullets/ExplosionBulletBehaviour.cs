using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBulletBehaviour : MonoBehaviour {

    [SerializeField]
    private int numObjects = 20;
    public GameObject prefab;

    void Start()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            int a = 360 / numObjects * i;
            Vector3 pos = RandomCircle(center, 1.0f, a);
            GameObject ExplosionBullets = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
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
