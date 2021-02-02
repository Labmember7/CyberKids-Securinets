using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    void OnEnable()
    {
        gameObject.SetActive(false);
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        gameObject.SetActive(true);
    }
}

