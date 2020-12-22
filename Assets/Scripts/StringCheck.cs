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
    public List<string> Passwords;
    public bool stringFull = false;
    public string password="";

    public UnityEvent OnSuccess;
    public UnityEvent OnFail;
    // Update is called once per frame
  public void CheckPassword()
    {
        password = "";

        foreach (GameObject character in characters)
        {
            password+= character.GetComponent<InputField>().text;
        }
        
        if (password.Length < characters.Count)
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
        Debug.Log(Passwords[0]); Debug.Log(password);
        if (stringFull == true && password == Passwords[0])
        {
            OnSuccess.Invoke();
        }
        else
        {
            OnFail.Invoke();
        }
    }
}
