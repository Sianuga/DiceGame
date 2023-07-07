using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{

    [SerializeField] float letterDistance = 0.4f;
    [SerializeField] float startingPoint = -2f;
    [SerializeField] int maxWordLength=15;
    [SerializeField] float fadeDuration = 1f;
    [SerializeField] float letterRotation = -43f;

    List<GameObject> letters = new List<GameObject>();
    

    [UDictionary.Split(30, 70)]
    public UDictionary1 lettersGO;
    [Serializable]
    public class UDictionary1 : UDictionary<string, GameObject> { }

    void Start()
    {

        for (int i = 0; i < maxWordLength; i++)
        {
            GameObject letter = Instantiate(lettersGO["I"], transform.position, transform.rotation, transform);
            letter.name = "Letter " + i;
            letter.transform.Rotate(letterRotation, 0,0);
            letters.Add(letter) ;
            letters[i].transform.Translate(letterDistance * i - startingPoint, 0, 0);
/*            Renderer renderer = letters[i].GetComponent<Renderer>();
            StartCoroutine(FadeLetter(renderer.material, fadeDuration,  0 , true));*/

        }
      

    }

    int calculateMiddlePoint(int numberOfSpots)
    {
        if (numberOfSpots % 2 == 0)
        {
            return numberOfSpots / 2;
        }
        else
        {
            return (numberOfSpots +1) / 2;
        }
    }

    public void spawnWord(string Word)
    {

        StartCoroutine(WaitForFade(Word));
       
           

        
    }


    IEnumerator FadeLetter(Material material, float duration, float delay, bool isFadingIn)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0;
        float startFade = material.GetFloat("_Fade");
        float targetFade = isFadingIn ? 1 : 0;


        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float lerpValue = Mathf.Lerp(startFade, targetFade, t);
            material.SetFloat("_Fade",lerpValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        material.SetFloat("_Fade", targetFade);
    }

    IEnumerator WaitForFade(string Word)
    {
        Word = Word.ToUpper();

        for (int i = 0; i < maxWordLength; i++)
        {
            StartCoroutine(FadeLetter(letters[i].GetComponent<Renderer>().material, fadeDuration, 0, false));
        }

        yield return new WaitForSeconds(fadeDuration*2);


            int startingPoint;
        
        if (Word.Length < maxWordLength)
        {
            if (Word.Length % 2 == 0)
            {
                startingPoint = calculateMiddlePoint(maxWordLength) - (Word.Length / 2);
            }
            else
            {
                startingPoint = calculateMiddlePoint(maxWordLength) - (Word.Length / 2) - 1;
            }
            for (int i = 0; i < Word.Length; i++)
            {
                if (lettersGO.ContainsKey(Word[Word.Length - 1 - i].ToString()))
                {
                    StartCoroutine(FadeLetter(letters[startingPoint + i].GetComponent<Renderer>().material, fadeDuration, 0, true));
                    letters[startingPoint + i].GetComponent<MeshFilter>().sharedMesh = lettersGO[Word[Word.Length - 1 - i].ToString()].GetComponent<MeshFilter>().sharedMesh;
                 
                }
                else
                {
                    StartCoroutine(FadeLetter(letters[startingPoint + i].GetComponent<Renderer>().material, fadeDuration, 0, true));
                    letters[startingPoint + i].GetComponent<MeshFilter>().sharedMesh = lettersGO["?"].GetComponent<MeshFilter>().sharedMesh;
               
                }

            }

        }
        else
        {
            for (int i = 0; i < maxWordLength; i++)
            {
                if (lettersGO.ContainsKey(Word[i].ToString()))
                {
                    StartCoroutine(FadeLetter(letters[i].GetComponent<Renderer>().material, fadeDuration, fadeDuration, true));
                    letters[i].GetComponent<MeshFilter>().sharedMesh = lettersGO[Word[i].ToString()].GetComponent<MeshFilter>().sharedMesh;
                  
                }
                else
                {
                    StartCoroutine(FadeLetter(letters[i].GetComponent<Renderer>().material, fadeDuration, fadeDuration, true));
                    letters[i].GetComponent<MeshFilter>().sharedMesh = lettersGO["?"].GetComponent<MeshFilter>().sharedMesh;
              
                }

            }
        }
    }

    void Update()
    {
    }
}
