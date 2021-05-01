using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using Lean.Localization;
public class QuizzManager : MonoBehaviour
{
    private List <GameObject> questions = new List<GameObject>();
    public Color correctColor;
    public Color wrongColor;
    public TextAsset questionsPool;
    private ColorBlock theColor;
    public Text scoreText;
    public Text resultText;
    public Text detatilText;
    public UnityEvent Onfinish;
    private int correctAnswers = 0;
    private int numberOfTries;
    public AudioClip success;
    public AudioClip fail;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Button b in FindObjectsOfType<Button>())
        { 
            if (b.transform.root.tag == "Respawn") continue;
            b.gameObject.GetComponent<ButtonScript>().onInteract.AddListener(() => {
                GoNextQuestion(b.gameObject);
                });
        }
    }
    public void Init()
    {
        questions = GameObject.FindGameObjectsWithTag("Quizz").ToList<GameObject>();
        numberOfTries = Resources.FindObjectsOfTypeAll<UIManager>()[0].GetActualQuizTries();
        // Debug.Log("Quiz 2 numberOfTries = " + numberOfTries);
        ReadQuestionsFromFile();
       // Debug.Log(correctAnswers);
    }

    public void UpdateScore()
    {
        numberOfTries--;
        if (numberOfTries > 0)
        {
            detatilText.text = LeanLocalization.GetTranslationText("Tries") + numberOfTries + " / 3";
        }
        else
        {
            detatilText.text = LeanLocalization.GetTranslationText("Tries") +" 0 / 3";
        }
        detatilText.text += "\n" + LeanLocalization.GetTranslationText("Correct") + correctAnswers + " / " + questions.Count;
        if (numberOfTries <= 0)
        {
            correctAnswers = 0;
            numberOfTries=0;
        }
        int score = PlayerPrefs.GetInt("score"); 
        scoreText.text = "Points : +"+ correctAnswers*100;
        FindObjectOfType<PlayfabManager>().SendLeaderboard(score + correctAnswers * 100);
        FindObjectOfType<PlayfabManager>().GetPlayerInfo();
        PlayerPrefs.SetInt("score", score + correctAnswers * 100);

        if (correctAnswers == 0)
        {
            resultText.text = LeanLocalization.GetTranslationText("Lose");
        }
        else
        {
            resultText.text = LeanLocalization.GetTranslationText("Win");
        }

        correctAnswers = 0;

        Resources.FindObjectsOfTypeAll<UIManager>()[0].SetActualQuizTries();

    }
    string[] lines;
    int index = 0;
    Dictionary<string, string> questionAnswer = new Dictionary<string, string>();
    Dictionary<string, string> quizzQuestionsServer = new Dictionary<string, string>();
    void ReadQuestionsFromFile()
    {
        lines=null;
        index = 0;
        questionAnswer = null;
        quizzQuestionsServer = null;

        quizzQuestionsServer = FindObjectOfType<PlayfabManager>().quizzQuestionsServer;
        if (quizzQuestionsServer.Count == 4)
        {
            foreach (KeyValuePair<string, string> kvp in quizzQuestionsServer)
            {
                if (questionsPool.name == kvp.Key)
                {
                    lines = kvp.Value.Split(';');
                }
            }
        }
        else
        {
            //reading from the file
            lines = System.Text.Encoding.UTF8.GetString(questionsPool.bytes).Split('\n');
        }


        foreach (string line in lines)
        {
            questionAnswer.Add(line.Split('|')[0], line.Substring(line.IndexOf('|') + 1));
        }

        //Creating the dictionary containing questions as keys and responses as values
        KeyValuePair<string, string> keyValuePair;
        foreach (Text textArea in gameObject.GetComponentsInChildren<Text>())
        {
            if (textArea.gameObject.name == "Subtitle")
            {
                keyValuePair = questionAnswer.ElementAt(index);
                // Debug.Log("Subtitle " + pairOfIndex1.Key+ index);
                textArea.text = keyValuePair.Key;
                index++;
            }

        }
        index = 0;
        int i = 0;
        foreach (Button response in gameObject.GetComponentsInChildren<Button>())
        {
            theColor = response.colors;
            theColor.pressedColor = wrongColor;
            theColor.selectedColor = wrongColor;
            response.colors = theColor;

            response.onClick.RemoveAllListeners();
            keyValuePair = questionAnswer.ElementAt(index);
            string[] responses = keyValuePair.Value.Split('|');
            if (responses[i].Contains("true"))
            {
                theColor = response.colors;
                theColor.pressedColor = correctColor;
                theColor.selectedColor = correctColor;
                response.colors = theColor;
                response.onClick.AddListener(() => {
                    correctAnswers++;
                    FindObjectOfType<AudioSource>().PlayOneShot(success);
                });
            }
            else
            {
                response.onClick.AddListener(() => {
                    FindObjectOfType<AudioSource>().PlayOneShot(fail);
                });
            }
            if (index == questions.Count - 1)
            {
                response.onClick.AddListener(() => Onfinish.Invoke());

            }
            response.GetComponentInChildren<Text>().text = responses[i].Substring(0, responses[i].IndexOf('(') - 1);
            i++;
            if (i % responses.Length == 0)
            {
                index++;
                i = 0;
            }

        }
    }
    void GoNextQuestion(GameObject gameObject)
    {
        if (questions.Contains(gameObject.transform.parent.gameObject))
        {

               // Debug.Log(questions.IndexOf(gameObject.transform.parent.gameObject)+ "  " + questions.Count);
                questions[(questions.IndexOf(gameObject.transform.parent.gameObject)) % questions.Count].GetComponent<PanelAnimator>().StartAnimOut();
                questions[(questions.IndexOf(gameObject.transform.parent.gameObject) + 1) % questions.Count].GetComponent<PanelAnimator>().StartAnimIn();


        }
    }
   

}
