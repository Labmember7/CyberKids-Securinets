using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfterSeconds : MonoBehaviour
{
    public float secondsBeforeSleep = 1;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(Hide());
    }

    public IEnumerator Hide()
    {
        yield return new WaitForSeconds(secondsBeforeSleep);
        gameObject.SetActive(false);
    }
}
