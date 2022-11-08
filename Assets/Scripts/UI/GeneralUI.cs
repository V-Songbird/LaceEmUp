using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneralUI : MonoBehaviour
{
    CanvasGroup myCanvasGroup;
    Canvas myCanvas;
    bool canHide = false;
    bool onAnimation = false;
    bool isOpen = false;
    private void Awake()
    {
        myCanvasGroup = GetComponent<CanvasGroup>();
        myCanvas = GetComponent<Canvas>();
    }

    public void ShowUI()
    {
        if (canHide == false && onAnimation == false)
        {
            onAnimation = true;
            myCanvas.enabled = true;

            LeanTween.value(gameObject, 0, 1, 0.5f).setOnUpdate((float value) =>
            {
                myCanvasGroup.alpha = value;
            }).setOnComplete(() =>
            {
                canHide = true;
                onAnimation = false;
            });
        }
    }

    public void HideUI()
    {
        if (canHide == true && onAnimation == false)
        {
            onAnimation = true;

            LeanTween.value(gameObject, 1, 0, 0.5f).setOnUpdate((float value) =>
            {
                myCanvasGroup.alpha = value;
            }).setOnComplete(() =>
            {
                canHide = false;
                onAnimation = false;
                myCanvas.enabled = false;
            });
        }
    }

    public void Interact()
    {
        if (isOpen == false)
        {
            isOpen = true;
            ShowUI();
        }
        else
        {
            isOpen = false;
            HideUI();
        }
    }
}
