using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float timeRemaining;
    public string label = "Temps Restant : ";
    public bool pause = false;
    public UnityEvent OnElapsed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTime());
    }

    public void SwitchPause()
    {
        pause = !pause;
    }

    // Update is called once per frame
    IEnumerator UpdateTime()
    {
        while (timeRemaining > 0)
        {
            if (!pause)
            {
                yield return new WaitForSecondsRealtime(1);
                timeRemaining = timeRemaining - 1;
                gameObject.GetComponent<Text>().text = label + TimeElapsed();
            }
            else
            {
                yield return new WaitUntil(()=>pause==true);

            }

        }
        if (OnElapsed.GetPersistentEventCount() > 0)
        {
            OnElapsed.Invoke();
        }
        Debug.Log(OnElapsed.GetPersistentEventCount());
    }
    private string TimeElapsed()
    {
        TimeSpan t = TimeSpan.FromSeconds(Mathf.RoundToInt(timeRemaining));
        return String.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

}
