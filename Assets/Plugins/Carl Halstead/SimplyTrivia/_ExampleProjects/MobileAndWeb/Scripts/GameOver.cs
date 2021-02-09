using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	[SerializeField] private Text finalScore;
	[SerializeField] private Text correctAnswers;
	[SerializeField] private Text incorrectAnswers;

	private void Update ()
	{
		if (Input.GetMouseButtonDown(0))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void SetGameOverText(int score, int correctAnswerCount, int incorrectAnswerCount)
	{
		finalScore.text = "Final Score: " + score;
		correctAnswers.text = "Correct Answers: " + correctAnswerCount;
		incorrectAnswers.text = "Incorrect Answers: " + incorrectAnswerCount;
	}
}
