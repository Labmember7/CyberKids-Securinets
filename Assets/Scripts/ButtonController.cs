using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
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
}
