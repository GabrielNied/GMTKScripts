using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoGravManager : MonoBehaviour
{
    public GameObject CampoGrav;

    private GameObject CampoGravJogo, CampoGravHeld, CanvasPai;
    private Vector3 mousepos;

    public int quantidade, limite = 1; 

    public enum ControlType { HoldMeSenpai, SetAndForget, WhyNotBoth };
    public ControlType currentType = ControlType.SetAndForget;

    private bool holding = false; 
    public bool possoColocar = true;
    public bool tutorialEnded = false;

    // Use this for initialization
    void Start()
    {
        CanvasPai = GameObject.Find("Canvas");
    }

    void Update()
    {
        if (!tutorialEnded)
            return;

        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, new Vector2(0, 0), 0.01f);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.tag == "AreaCampoGrav")
                {
                    possoColocar = false;
                }
            }
        }
        else
        {
            possoColocar = true;
        }

        if (Input.GetButtonDown("Fire1") && possoColocar)
        {
            if ((currentType == ControlType.HoldMeSenpai || currentType == ControlType.WhyNotBoth) && !holding)
            {
                TryHolding();
            }
            else if (currentType == ControlType.SetAndForget && quantidade < 1)
            {
                TryCreate();
            }
        }
        else if (Input.GetButtonUp("Fire1") && holding &&
          (currentType == ControlType.HoldMeSenpai || currentType == ControlType.WhyNotBoth))
        {
            TryReleasing();
        }

        if (Input.GetButtonDown("Fire2") && currentType == ControlType.WhyNotBoth && possoColocar)
        {
            TryCreate();
        }
    }

    void TryCreate()
    {
        if (quantidade < limite)
        {
            quantidade++;

            mousepos = Input.mousePosition;
            mousepos.z = 10;
            mousepos = Camera.main.ScreenToWorldPoint(mousepos);

            CampoGravJogo = Instantiate(CampoGrav, mousepos, Quaternion.identity) as GameObject;
            
            CampoGravJogo.transform.SetParent(CanvasPai.GetComponent<Transform>());
        }
    }

    void TryHolding()
    {
        holding = true;
        mousepos = Input.mousePosition;
        mousepos.z = 10;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);

        CampoGravHeld = Instantiate(CampoGrav, mousepos, Quaternion.identity) as GameObject;
        CampoGravHeld.GetComponent<CampoGrav>().held = true;
        
        CampoGravHeld.transform.SetParent(CanvasPai.GetComponent<Transform>());
    }

    void TryReleasing()
    {
        holding = false;
        Destroy(CampoGravHeld);
    }
}
