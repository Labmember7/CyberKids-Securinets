using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoomIn : MonoBehaviour
{
    float t =0;
    float x = 0f;
    public float timeBeforeZoom = 0.5f;
    bool clicked = false;
    static Vector2 dest = new Vector2 (-5.14f,-2.02f);
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        if (cam)
        {
            x = cam.fieldOfView;
        }
    }
    IEnumerator startZoom()
    {
        if (clicked)
        {
            if (x > 1f)
            {
                yield return new WaitForSeconds(timeBeforeZoom);

                while (x > 0.9f)
                {
                    cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, 0, t);
                    cam.GetComponent<Camera>().lensShift = Vector2.Lerp(cam.GetComponent<Camera>().lensShift, dest, t/10);
                    x = cam.GetComponent<Camera>().fieldOfView;
                    t += 0.05f * Time.deltaTime;
                    Debug.Log(x);
                    yield return new WaitForEndOfFrame();
                }
                NextScene(1);
                Debug.LogWarning("Zomm end");
            }
          
        }
        yield return null;

        
    }

    public void NextScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    public void Zoom()
    {
        StartCoroutine(startZoom());
        clicked = true;
    }


}
