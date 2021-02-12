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
    public int selectedChapter = 0;
    public void HideShow()
    {
          VideoPanel.SetActive(!VideoPanel.activeInHierarchy);
    }
    public void QuizHideShow()
    {
        quizs[selectedChapter].SetActive(true);
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

