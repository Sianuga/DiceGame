using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSystem : MonoBehaviour
{
    [SerializeField] public GameObject dice;
    [SerializeField] public LetterSpawner wordSpawner;
    [SerializeField] public Button rollButton;
    [SerializeField] public ParticleSystem particleSystem;


    List<string> easyWords = new List<string>()
    {
        "Cat",
        "Dog",
        "Bat",
        "Rat",
        "Hat",
        "Cap",
        "Map",
        "Nap",
    };
    List<string> mediumWords = new List<string>()
    {
        "Bottle",
        "Catastrophe",
        "Durable",
        "Seldom",
        "Shelter",
        

    };
    List<string> hardWords = new List<string>()
    {
        "Ornate",
        "Loathe",
        "Lobby",
        "Dreamer",
        "Rancor",
        "Zealous",
       
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

        if (diceRoll.diceRolled && wallCheck.diceLanded)
        {
            finishThrow();
        }


    }

    private void finishThrow()
    {
        particleSystem.transform.rotation = Quaternion.Euler(-90, 0, 0);
        particleSystem.Play();
        wordSpawner.spawnWord(wallCheck.getTopWord());
        rollButton.interactable = true;
        diceRoll.diceRolled = false;
        wallCheck.diceLanded = false;
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

    public string[] drawWordsForDice(int numberOfSides)
    {
        levelOfWord[] levels = { levelOfWord.easy, levelOfWord.medium, levelOfWord.hard };
        string[] words = new string[numberOfSides];

        int levelIndex = 0;

        for (int i = 0; i < numberOfSides; i++)
        {
            string randomWord = GetRandomWord(levels[levelIndex]);
            if (i == 0)
            {
                words[i] = randomWord;
            }
            else
            {
                while (words[i - 1] == randomWord)
                {
                    randomWord = GetRandomWord(levels[levelIndex]);
                }
                words[i] = randomWord;
            }


            if ((i + 1) % 2 == 0)
            {
                levelIndex = (levelIndex + 1) % 3;
            }
        }
        return words;
    }
}
