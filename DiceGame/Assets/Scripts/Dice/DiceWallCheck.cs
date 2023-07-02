using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceWallCheck : MonoBehaviour
{
    BoxCollider boxCollider;
    Vector3[] diceSides;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        diceSides = new Vector3[6];
        diceSides[0] = boxCollider.center + new Vector3(0, 0, 0.5f);
        diceSides[1] = boxCollider.center + new Vector3(0, 0, -0.5f);
        diceSides[2] = boxCollider.center + new Vector3(-0.5f, 0, 0);
        diceSides[3] = boxCollider.center + new Vector3(0.5f, 0, 0);
        diceSides[4] = boxCollider.center + new Vector3(0, 0.5f, 0);
        diceSides[5] = boxCollider.center + new Vector3(0, -0.5f, 0);
        
    }

    

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 0.4f);
    }
}
