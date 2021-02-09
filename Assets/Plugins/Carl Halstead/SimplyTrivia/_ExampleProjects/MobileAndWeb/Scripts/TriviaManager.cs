using SimplyTrivia;
using SimplyTrivia.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0414
public class TriviaManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AnswerManager answerManager;
	[SerializeField] private TimerManager timerManager;
    [SerializeField] private Text questionText;

	[Space(5f)]

	[SerializeField] private GameObject mainGameCanvas;
	[SerializeField] private GameObject gameOverCanvas;

	[Header("References - Stats")]
	[SerializeField] private Text incorrectAnswers;
	[SerializeField] private byte incorrectAnswerCount;

	[Space(5f)]

	[SerializeField] private Text correctAnswers;
	[SerializeField] private byte correctAnswerCount;

	[Space(5f)]

	[SerializeField] private Text totalScore;
	[SerializeField] private int totalScoreCount;

    [Header("Trivia")]
    [SerializeField] private QuizParams quizParams = new QuizParams();

    private Queue<TriviaResponse.TriviaQuestion> pendingQuestions = new Queue<TriviaResponse.TriviaQuestion>();
    private TriviaResponse.TriviaQuestion currentQuestion = null;

    public static TriviaManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    private void Start()
    {
        timerManager.OnTimerFinished += (sender, e) => GameOver();

        TriviaClient.Instance.OnTriviaQuizReceived += TriviaClient_OnOpenTriviaQuizReceived;
        TriviaClient.Instance.OnTriviaErrorReceived += TriviaClient_OnOpenTriviaErrorReceived;
        TriviaClient.Instance.CreateQuiz(quizParams);
    }

    private void TriviaClient_OnOpenTriviaErrorReceived(object sender, OpenTriviaErrorReceived e)
    {
        Debug.Log("Error Received: " + e.Response.response_code_enum.ToString());
    }

    private void TriviaClient_OnOpenTriviaQuizReceived(object sender, OpenTriviaQuizReceived e)
    {
        for (int i = 0; i < e.Response.results.Count; i++)
        {
            pendingQuestions.Enqueue(e.Response.results[i]);
        }

        SetupNextQuestion();
    }

    private TriviaResponse.TriviaQuestion DequeueQuestion()
    {
        if (pendingQuestions.Count == 0)
        {
			GameOver();
			return new TriviaResponse.TriviaQuestion();
        }

        TriviaResponse.TriviaQuestion question = pendingQuestions.Dequeue();
        return question;
    }

    private void SetUI()
    {
		timerManager.StartTimer(12f);

        questionText.text = currentQuestion.question;
        answerManager.SetAnswers(currentQuestion.allAnswersShuffled);
    }

    public bool CheckAnswer(string guess)
    {
        if (currentQuestion.IsCorrectAnswer(guess, true))
        {
			CorrectAnswer();
            SetupNextQuestion();
			return true;
        }
		else
		{
			IncorrectAnswer();
			return false;
		}
    }

	private void GameOver()
	{
		gameOverCanvas.SetActive(true);
		mainGameCanvas.SetActive(false);

		GameOver go = gameOverCanvas.GetComponent<GameOver>();
		go.SetGameOverText(totalScoreCount, correctAnswerCount, incorrectAnswerCount);
	}

	private void CorrectAnswer()
	{
		correctAnswerCount += 1;
		correctAnswers.text = "Correct Answers: " + correctAnswerCount;

		totalScoreCount += Mathf.RoundToInt(timerManager.TimeRemaining * 10f);
		totalScore.text = "Total Score: " + totalScoreCount;
	}

	private void IncorrectAnswer()
	{
		incorrectAnswerCount += 1;
		incorrectAnswers.text = "Incorrect Answers: " + incorrectAnswerCount;

		timerManager.SubtractTime();
	}

    private void SetupNextQuestion()
    {
        currentQuestion = DequeueQuestion();
        SetUI();
    }
}
