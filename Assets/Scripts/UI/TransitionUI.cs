using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using JamOff.Scripts.Managers;


public class TransitionUI : GeneralUI
{
    public UnityEvent onTransitionFinish;
    [SerializeField] RectTransform myTransform;

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
        LeanTween.scale(myTransform, new Vector2(0, 0), 2f).setOnComplete(HideUI);
    }

    public void TransitionFromPortal(Vector3 newPos)
    {
        if (GamePlayManager.Instance.PortalsManager.redPortal.portalActive && GamePlayManager.Instance.PortalsManager.bluePortal.portalActive)
        {
            //ShowUI();
            LeanTween.scale(myTransform, new Vector2(1, 1), 1f).setOnComplete(() =>
            {
                GamePlayManager.Instance.Player.transform.position = newPos;
                FinishPortalTransition();
            });
        }
    }

    void FinishPortalTransition()
    {
        LeanTween.scale(myTransform, new Vector2(0, 0), 1f).setOnComplete(() =>
        {
            GamePlayManager.Instance.Player_CutActions.GetBackPhysicsAndMovement();
            //HideUI();
        }).setDelay(0.5f);
    }
}
