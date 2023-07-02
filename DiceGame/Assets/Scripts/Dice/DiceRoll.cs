using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    Rigidbody rb;
    [Range(1f, 50f)] public float rollUpwardForce = 10f;
    [Range(1f, 50f)] public float rollForwardForce = 1f;
    public Vector3 startingPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
    }


    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            transform.position = startingPosition;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

      
            Vector3 rollDirection = Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward;


            Vector3 rollForce = rollDirection * rollForwardForce + Vector3.up * rollUpwardForce;
            rb.AddForce(rollForce, ForceMode.Impulse);




            rb.AddTorque(Random.Range(200, 1000), Random.Range(200, 1000), Random.Range(200,1000));
         
        }
    }
}
