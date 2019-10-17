using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enforcer : MonoBehaviour {

    static Enforcer instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}
