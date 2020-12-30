using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StringCheck : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public List<GameObject> characters;
    [SerializeField]
    public string wordToGuess;
    public bool stringFull = false;
    public string inputWord="";

    public UnityEvent OnSuccess;
    public UnityEvent OnFail;
    // Update is called once per frame
  public void CheckWord()
    {
        inputWord = "";

        foreach (GameObject character in characters)
        {
            inputWord += character.GetComponent<InputField>().text;
        }
        
        if (inputWord.Length < characters.Count)
        {
            stringFull = false;
        }
        else
        {
            stringFull = true;
        }

    }

    public void Result()
    {
        Debug.Log(wordToGuess); Debug.Log(inputWord);
        if (stringFull == true && inputWord == wordToGuess)
        {
            OnSuccess.Invoke();
        }
        else
        {
            OnFail.Invoke();
        }
    }
}
