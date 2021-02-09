using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour 
{
    [SerializeField] Text answerTxt;

    public void SetText(string answer)
    {
        answerTxt.text = answer;
    }

    public void MakeGuess()
    {
        bool isCorrect = TriviaManager.Instance.CheckAnswer(answerTxt.text);

		if (isCorrect == false)
			GetComponent<Button>().interactable = false;
    }
}
