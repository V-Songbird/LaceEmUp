using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class TransitionUI : GeneralUI
{
    public UnityEvent onTransitionFinish;
    RectTransform myTransform;

    private void Start()
    {
        myTransform = GetComponentInChildren<RectTransform>();
    }

    void MakeTransition()
    {
        ShowUI();
        LeanTween.scale(myTransform, new Vector2(1, 1), 2f).setOnComplete(() =>
        {
            FinishTransition();
        });
    }

    void FinishTransition()
    {
        onTransitionFinish.Invoke();
        LeanTween.scale(myTransform, new Vector2(1, 1), 2f).setOnComplete(HideUI);
    }
}
