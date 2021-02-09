using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuizzManager : MonoBehaviour
{
    public Button[] correctAnswers;
    public List <GameObject> questions = new List<GameObject>();
    public Color color;
    private ColorBlock theColor;
    public UnityEvent Onfinish;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Button b in FindObjectsOfType<Button>())
        {
            b.gameObject.GetComponent<ButtonScript>().onInteract.AddListener(()=>GoNextQuestion(b.gameObject));
        }

        foreach (Button b in correctAnswers)
        {
            theColor = b.colors;
            theColor.pressedColor = color;
            theColor.selectedColor = color;
            b.colors = theColor;
        }
    }
    void GoNextQuestion(GameObject gameObject)
    {
        if (questions.Contains(gameObject.transform.parent.gameObject))
        {
            if((questions.IndexOf(gameObject.transform.parent.gameObject)) == questions.Count - 1)
            {
                Onfinish.Invoke();
            }
            else
            {
                Debug.Log(questions.IndexOf(gameObject.transform.parent.gameObject));
                questions[(questions.IndexOf(gameObject.transform.parent.gameObject)) % questions.Count].GetComponent<PanelAnimator>().StartAnimOut();
                questions[(questions.IndexOf(gameObject.transform.parent.gameObject) + 1) % questions.Count].GetComponent<PanelAnimator>().StartAnimIn();
            }

        }
    }
    public void UpdateAllButtons()
    {
           foreach(Button b in FindObjectsOfType<Button>())
        {
            b.interactable = false;
        }
    }
    public void ActivateAllButtons()
    {
        foreach (Button b in FindObjectsOfType<Button>())
        {
            b.interactable = true;
        }
    }

}
