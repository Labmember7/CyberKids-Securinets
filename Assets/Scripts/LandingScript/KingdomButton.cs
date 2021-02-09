using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class KingdomButton : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler{

    public Text text;
    public Text textNum;
    public Image rect;
    public Image circle;

    public Color textColorWhenSelected;
    public Color rectColorMouseOver;

    private void Start()
    {
        rect.color = Color.clear;
        text.color = Color.white;
        textNum.color = Color.white;
        circle.color = Color.white;
    }
    public void ChoseChapterVideo()
    {
        // Debug.Log(int.Parse(textNum.text) - 1);
        FindObjectOfType<UIManager>().videoPlayer.clip = FindObjectOfType<UIManager>().videoClips[int.Parse(textNum.text) - 1];
        
    }
    public void FixLangAppearance(string lang)
    {
        foreach( var button in Resources.FindObjectsOfTypeAll<KingdomButton>())
        {
            if (lang == "fr")
            {

                button.textNum.alignment = TextAnchor.MiddleRight;
                //Debug.Log("ALigned right");

            }
            else
            {
                button.textNum.alignment = TextAnchor.MiddleLeft;
                //Debug.Log("ALigned left");

            }
        }
        
        
    }
    public void OnDeselect(BaseEventData eventData)
    {
        rect.DOColor(Color.clear, .1f);
        text.DOColor(Color.white, .1f);
        textNum.DOColor(Color.white, .1f);
        circle.DOColor(Color.white, .1f);
    }

    public void OnSelect(BaseEventData eventData)
    {
        rect.DOColor(Color.white, .1f);
        text.DOColor(textColorWhenSelected, .1f);
        textNum.DOColor(textColorWhenSelected, .1f);
        circle.DOColor(Color.red, .1f);
        rect.transform.DOComplete();
        rect.transform.DOPunchScale(Vector3.one / 3, .2f, 20, 1);
        //Debug.Log(eventData.selectedObject.GetComponentInChildren<Text>().text);

    }

    public void OnSubmit(BaseEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            rect.DOColor(rectColorMouseOver, .2f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            rect.DOColor(Color.clear, .2f);
        }
    }
}
