using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerManager : MonoBehaviour 
{
    [Header("Answer")]
    [SerializeField] GameObject answerPrefab;
    [SerializeField] Transform answerParent;

    [Header("Instantiated Answers")]
    [SerializeField] GameObject[] answerButtons;

    private void Awake()
    {
        InstantiateAnswersPrefabs();
    }

    private void InstantiateAnswersPrefabs()
    {
        answerButtons = new GameObject[4];

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i] = Instantiate(answerPrefab, answerParent);
        }

        SetActiveAllAnswers(false);
    }

    public void SetAnswers(List<string> answers)
    {
        SetActiveAllAnswers(false);

        for (int i = 0; i < answers.Count; i++)
        {
			answerButtons[i].GetComponent<Button>().interactable = true;
            answerButtons[i].GetComponent<Answer>().SetText(answers[i]);
            answerButtons[i].SetActive(true);
        }
    }

    private void SetActiveAllAnswers(bool active)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].SetActive(active);
        }
    }
}
