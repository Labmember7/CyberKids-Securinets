using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
public class ScoreManager : MonoBehaviour
{
    public static int score;
    [SerializeField]
    public int[] scoreMultiplier;
    // Start is called before the first frame update
    public Text scoreTextField;
    public KingdomButton kingdomButton;
    void Start()
    {
        if (LeanLocalization.currentLanguage.ToLower().Contains("ar"))
        {
            kingdomButton.FixLangAppearance("ar");
        }
        else
        {
            kingdomButton.FixLangAppearance("fr");
        }
    }
    void GetScore()
    {
        score = PlayerPrefs.GetInt("score");
    }
    int earnedPoints;
    Timer time;
    // Update is called once per frame
    public void UpdateScore(int numQuizz)
    {
        //Calculate earned Score
        time = GameObject.FindObjectOfType<Timer>();
        earnedPoints = (int)(time.timeRemaining) * scoreMultiplier[numQuizz];
        //Reduce
        if(scoreMultiplier[numQuizz] > 1)
        {
            scoreMultiplier[numQuizz] /= 10;
        }
        else
        {
            scoreMultiplier[numQuizz] = 0;
        }
        GetScore();
        //Update Server
        Debug.Log("Score Updated");
        gameObject.GetComponent<PlayfabManager>().SendLeaderboard(score+ earnedPoints);
        gameObject.GetComponent<PlayfabManager>().GetPlayerInfo();
        //Update Locally
        PlayerPrefs.SetInt("score", score + earnedPoints);
        GetScore();
        ShowScore();
    }
    public void ShowScore()
    {
        //Show on Finish Screen
        scoreTextField.text = "+ " + earnedPoints+"\n"+ Lean.Localization.LeanLocalization.GetTranslationText("Score") +" : "  + score;
    }
}
