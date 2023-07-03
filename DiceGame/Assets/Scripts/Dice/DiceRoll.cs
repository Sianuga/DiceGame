using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    Rigidbody rb;
    [Range(1f, 50f)] public float maxRollUpwardForce = 10f;
    [Range(1f, 50f)] public float maxRollForwardForce = 1f;
    public Vector3 startingPosition;
    public bool diceRolled { get; set; } = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position + Vector3.up;
        transform.position = startingPosition;
    }


    
 
    public void RollDice()
    {

        if (!diceRolled)
        {
            diceRolled = true;
            ResetDice();
            ThrowDice();
        }
    }

    private void ThrowDice()
    {
        Vector3 rollDirection = Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward;


        Vector3 rollForce = rollDirection * Random.Range(0, maxRollForwardForce) + Vector3.up * Random.Range(4, maxRollUpwardForce);
        rb.AddForce(rollForce, ForceMode.Impulse);




        rb.AddTorque(Random.Range(200, 1000), Random.Range(200, 1000), Random.Range(200, 1000));
    }

    private void ResetDice()
    {
        transform.position = startingPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
