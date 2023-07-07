using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    Rigidbody rb;
    [Range(1f, 50f)] public float maxRollUpwardForce = 10f;
    [Range(0.5f, 50f)] public float maxRollForwardForce = 1f;
    public Vector3 startingPosition;
    public bool diceRolled { get; set; } = false;
    [SerializeField] public Text[] diceSidesText;
    [SerializeField] public WordSystem wordSystem;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position + Vector3.up;
        transform.position = startingPosition;

        foreach (Text t in diceSidesText)
        {
            t.text = "";
        }
    }


    
 
    public void RollDice()
    {

        if (!diceRolled)
        {

            StartCoroutine(DesintegrateAndThrow());
            
        
        }
    }

    private IEnumerator DesintegrateAndThrow()
    {
  
        
        /*yield return new WaitForSeconds(1f);*/
        fillTexts();
        diceRolled = true;
        ResetDice();
        ThrowDice();
        yield return new WaitForSeconds(0.01f);
    }



    private void fillTexts()
    {
  
        string[] words = wordSystem.drawWordsForDice(diceSidesText.Length);
       
        for (int i = 0; i < diceSidesText.Length; i++)
        {

            diceSidesText[i].text = words[i];
        }
    }

    private void ThrowDice()
    {
        Vector3 rollDirection = Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward;


        Vector3 rollForce = rollDirection * Random.Range(0, maxRollForwardForce) * (Random.Range(0, 1) <= 0.5 ? -1:1) + Vector3.up * Random.Range(4, maxRollUpwardForce);
        rb.AddForce(rollForce, ForceMode.Impulse);




        rb.AddTorque(Random.Range(200, 500) * (Random.Range(0, 1) <= 0.5 ? -1 : 1) , Random.Range(200, 500) * (Random.Range(0, 1) <= 0.5 ? -1 : 1), Random.Range(200, 500) * (Random.Range(0, 1) <= 0.5 ? -1 : 1));
    }

    private void ResetDice()
    {
        transform.position = startingPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public string getTopWord(int diceFaceNumber)
    {
        return diceSidesText[diceFaceNumber].text;
    }
}
