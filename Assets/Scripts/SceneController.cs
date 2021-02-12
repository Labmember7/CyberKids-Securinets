using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private ScreenOrientation screenOrientation ;
   // float t = 0;
   // float x = 0f;
   // public float timeBeforeZoom = 0.5f;
   // static Vector2 dest = new Vector2(-5.14f, -2.02f);
    //Camera cam;
    // public Font font;

    void OnEnable()
    {
        if (gameObject.tag == "UI")
            GameObject.FindObjectOfType<PlayfabManager>().GetLeaderboard();

    }
    void Start()
    {
        Screen.orientation = screenOrientation;
    }
    public void UpdateScreenOrientation()
    {
        if (Screen.orientation.Equals(ScreenOrientation.Portrait))
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    public void EmitParticles(GameObject particles)
    {

        StartCoroutine(EmitForSeconds(particles));
    }

    IEnumerator EmitForSeconds(GameObject particles)
    {

            UIParticleSystem particleSystem = particles.GetComponent<UIParticleSystem>();
            particleSystem.enabled = true;
            yield return null;
            particleSystem.enabled = false;


    }

    public void NextScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex)+1);
        //UpdateScreenOrientation(screenOrientation);
    }
    public void NextScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
       // UpdateScreenOrientation(screenOrientation);
    }
    public void StartScene()
    {
        SceneManager.LoadScene(0);
        //UpdateScreenOrientation(screenOrientation);
    }
    public void Exit()
    {
        Application.Quit();
    }
   
   /* public void Zoom()
    {
        StartCoroutine(startZoom());
    }
    IEnumerator startZoom()
    {
        if (x > 1f)
        {
            yield return new WaitForSeconds(timeBeforeZoom);

            while (x > 0.9f)
            {
                cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, 0, t);
                cam.GetComponent<Camera>().lensShift = Vector2.Lerp(cam.GetComponent<Camera>().lensShift, dest, t / 10);
                x = cam.GetComponent<Camera>().fieldOfView;
                t += 0.05f * Time.deltaTime;
                Debug.Log(x);
                yield return new WaitForEndOfFrame();
            }
            NextScene();
            Debug.LogWarning("Zomm end");
        }
        yield return null;


    }*/


}
/* foreach(var fooGroup in Resources.FindObjectsOfTypeAll<Text>())
  {
      fooGroup.font = font;
  }*/