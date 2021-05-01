using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    public GameObject VideoPanel;
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    public GameObject[] quizs;
    public TextAsset[] quizQuestions;
    private int[] numberOfTries = new int[4];
    public int selectedChapter = 0;
    
    public void HideShow()
    {
          VideoPanel.SetActive(!VideoPanel.activeInHierarchy);
    }
    public void SetNumberOfTries()
    {
        for(int i = 0; i < numberOfTries.Length; i++)
        {
            if (!PlayerPrefs.HasKey("numberOfTries"+i))
            {
                numberOfTries[i] = 3;
                PlayerPrefs.SetInt("numberOfTries" + i,numberOfTries[i]) ;
                //Debug.Log("Numberoftries is not found");

            }
            else
            {
                numberOfTries[i]= PlayerPrefs.GetInt("numberOfTries" + i);

            }
            //Debug.Log("numberOfTries "+i+" = "+numberOfTries[i]);
        }
    }
    public int GetActualQuizTries()
    {
        return PlayerPrefs.GetInt("numberOfTries" + (selectedChapter - 1), 3);

    }  
    public void SetActualQuizTries()
    {
        PlayerPrefs.SetInt("numberOfTries" + (selectedChapter - 1), numberOfTries[(selectedChapter - 1)]-1);
    }
    public void QuizHideShow()
    {
        quizs[selectedChapter].SetActive(true);
        if (selectedChapter > 0)
        {
            var questionsFile = Resources.FindObjectsOfTypeAll<QuizzManager>()[0];
            //Debug.Log(questionsFile.gameObject.name);
            questionsFile.questionsPool = quizQuestions[selectedChapter - 1];
            SetNumberOfTries();
            questionsFile.Init();
        }
        

        if (selectedChapter > 0)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        /*GameObject quizz = Instantiate(quizs[i]);
         quizz.SetActive(true);
         Screen.orientation = ScreenOrientation.Portrait;*/
        //quizs[i].SetActive(!quizs[i].activeInHierarchy);
    }

}

