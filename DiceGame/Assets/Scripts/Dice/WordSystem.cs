using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSystem : MonoBehaviour
{
    [SerializeField] public GameObject dice;
    [SerializeField] public Text wordText;
    [SerializeField] public Button rollButton;

    List<string> easyWords = new List<string>()
    {
        "cat",
        "dog",
        "bat",
        "rat",
        "hat",
        "cap",
        "map",
        "nap",
    };
    List<string> mediumWords = new List<string>()
    {
        "Enigmatic",
        "Alleviate",
        "Resilient",
        "Seldom",
        "Shelter",
        

    };
    List<string> hardWords = new List<string>()
    {
        "Ubiquitous",
        "Egregious",
        "Magnanimous",
        "Serendipity",
        "Esoteric",
        "Vicarious",
       
    };

    private DiceWallCheck wallCheck;
    private DiceRoll diceRoll;

    
    
    void Start()
    {
        wallCheck = dice.GetComponent<DiceWallCheck>();
        diceRoll = dice.GetComponent<DiceRoll>();
    }

    void Update()
    {
        /*Debug.Log("Dice Landed: " + wallCheck.diceLanded + "Dice Rolled: " + diceRoll.diceRolled);*/

        if (diceRoll.diceRolled && wallCheck.diceLanded)
        {
            int topSideNumber = wallCheck.GetTopSideNumber();
            if (topSideNumber == 1 || topSideNumber == 2)
            {
                wordText.text = GetRandomWord(levelOfWord.easy);
            }
            else if (topSideNumber == 3 || topSideNumber == 4)
            {
                wordText.text = GetRandomWord(levelOfWord.medium);
            }
            else if (topSideNumber == 5 || topSideNumber == 6)
            {
                wordText.text = GetRandomWord(levelOfWord.hard);
            }
            rollButton.interactable = true;
            diceRoll.diceRolled = false;
            wallCheck.diceLanded = false;
        }
        else if (diceRoll.diceRolled && !wallCheck.diceLanded)
        {
            wordText.text = "Rolling...";
        }

    }

    enum levelOfWord
    {
        easy,
        medium,
        hard,
    }
    private string GetRandomWord(levelOfWord level)
    {
        if (level == levelOfWord.easy)
        {
            return easyWords[UnityEngine.Random.Range(0, easyWords.Count)];
        }
        else if (level == levelOfWord.medium)
        {
            return mediumWords[UnityEngine.Random.Range(0, mediumWords.Count)];
        }
        else if (level == levelOfWord.hard)
        {
            return hardWords[UnityEngine.Random.Range(0, hardWords.Count)];
        }
        else
        {
            return "Cheating is not allowed";
        }
     
    }
}
