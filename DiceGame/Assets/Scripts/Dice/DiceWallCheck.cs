using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceWallCheck : MonoBehaviour
{
    Vector3[] diceSides;
    BoxCollider boxCollider;
    public bool diceLanded { get;  set; } = false;
    private Rigidbody rb;
    private float rayCastLength=1f;
    private DiceRoll diceRoll;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        diceSides = new Vector3[6];
        diceRoll = GetComponent<DiceRoll>(); 
    }

    private void CalculateDiceFacesCenter()
    {
        diceSides[0] = transform.TransformPoint(boxCollider.center + new Vector3(0, 0, boxCollider.size.z / 2));
        diceSides[1] = transform.TransformPoint(boxCollider.center + new Vector3(0, boxCollider.size.y / 2, 0));
        diceSides[2] = transform.TransformPoint(boxCollider.center - new Vector3(boxCollider.size.x / 2, 0, 0));
        diceSides[3] = transform.TransformPoint(boxCollider.center + new Vector3(boxCollider.size.x / 2, 0, 0));
        diceSides[4] = transform.TransformPoint(boxCollider.center - new Vector3(0, boxCollider.size.y / 2, 0));
        diceSides[5] = transform.TransformPoint(boxCollider.center - new Vector3(0, 0, boxCollider.size.z / 2));

    }

    void Update()
    {
        CalculateDiceFacesCenter();
        CastRays();

        CheckDiceLanding();
    }

    private void CheckDiceLanding()
    {
        if(diceRoll.diceRolled)
        {
            if (rb.velocity.magnitude <= 0.01f && rb.angularVelocity.magnitude <= 0.01f && GetTopSideNumber() != -1)
            {

                diceLanded = true;
            }
     
        }

    }

    private void CastRays()
    {
        foreach(Vector3 v in diceSides)
        {
            RaycastHit hit;
            Vector3 direction = (v - transform.position).normalized;
            Physics.Raycast(transform.position, direction, out hit, rayCastLength);
            Debug.DrawRay(transform.position, direction , Color.red);

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (diceSides != null)
        {
            int i = 0;
            foreach (Vector3 v in diceSides)
            {
                if (i == 0)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.blue;
                }
                Gizmos.DrawSphere(v, 0.04f);
                i++;
            }
        }
    }

    public int GetTopSideNumber()
    {
        int i = 0;
        foreach (Vector3 v in diceSides)
        {
            RaycastHit hit;
            Vector3 direction = (v - transform.position).normalized;
            Physics.Raycast(transform.position, direction, out hit, rayCastLength);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Table"))
                {
                    int topNumber = diceSides.Length - i;
           
                    return topNumber ;
                }
            }
          
            i++;
        }
        return -1;
    }

    public string getTopWord()
    {
        return diceRoll.diceSidesText[GetTopSideNumber() - 1].text;
    }
}