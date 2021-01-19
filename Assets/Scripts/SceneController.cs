using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    public ScreenOrientation screenOrientation;
    void Start()
    {
        Screen.orientation = screenOrientation;
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
    }
    public void NextScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
    public void StartScene()
    {
        SceneManager.LoadScene(0);

    }
}
