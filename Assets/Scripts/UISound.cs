using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSfx;

    public void PlaySound()
    {
        audioSource.PlayOneShot(clickSfx);
        Debug.Log("Button pressed");
    }
}
