using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickChange : MonoBehaviour
{
    public GameObject textObject;
    public string chaine;
    public void ChangeTextOnClick()
    {
        textObject.GetComponent<TextMesh>().text = chaine;
    }
}
