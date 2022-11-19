using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JamOff.Scripts.Managers;

public class IndividualHeart : MonoBehaviour
{
    public Image MyHeart;
    public IndividualHeart BackHeart;
    public IndividualHeart NextHeart;

    public bool HealtEnable = true;
    float healtValue;

    void Start()
    {
        healtValue = GamePlayManager.Instance.CommonUI.HealthSystem.healtPerHeart;
    }
    public void RecibeDamage(float damage)
    {
        float actualHealtNormalVal = MyHeart.fillAmount * healtValue;
        float actualfillVal = actualHealtNormalVal / healtValue;

        GamePlayManager.Instance.CommonUI.HealthSystem.ActualHealt = this;

        if (damage < actualHealtNormalVal)
        {
            float newHealt = actualHealtNormalVal - damage;
            float nextFillVal = newHealt / healtValue;

            LeanTween.value(gameObject, actualfillVal, nextFillVal, 0.25f).setOnUpdate((value) =>
            {
                MyHeart.fillAmount = value;
            });
        }
        else
        {

            if (BackHeart != null)
            {
                LeanTween.value(gameObject, actualfillVal, 0, 0.25f).setOnUpdate((value) =>
                {
                    MyHeart.fillAmount = value;

                }).setOnComplete(() =>
                {
                    BackHeart.RecibeDamage(damage - actualHealtNormalVal);
                });
            }
            else
            {
                LeanTween.value(gameObject, actualfillVal, 0, 0.25f).setOnUpdate((value) =>
                 {
                     MyHeart.fillAmount = value;

                 }).setOnComplete(() =>
                 {
                     //GameOver
                 });
            }
        }
    }
    public void Heal(float healAmount)
    {
        float actualHealtNormalVal = MyHeart.fillAmount * healtValue;
        float actualfillVal = actualHealtNormalVal / healtValue;

        GamePlayManager.Instance.CommonUI.HealthSystem.ActualHealt = this;

        if (healAmount < healtValue - actualHealtNormalVal)
        {
            float newHealt = actualHealtNormalVal + healAmount;
            float nextFillVal = newHealt / healtValue;

            LeanTween.value(gameObject, actualfillVal, nextFillVal, 0.25f).setOnUpdate((value) =>
            {
                MyHeart.fillAmount = value;
            });
        }
        else
        {

            if (NextHeart != null && NextHeart.HealtEnable)
            {
                LeanTween.value(gameObject, actualfillVal, 1, 0.25f).setOnUpdate((value) =>
                {
                    MyHeart.fillAmount = value;

                }).setOnComplete(() =>
                {
                    NextHeart.Heal(healAmount - actualHealtNormalVal);
                });
            }
            else
            {
                LeanTween.value(gameObject, actualfillVal, 1, 0.25f).setOnUpdate((value) =>
                 {
                     MyHeart.fillAmount = value;

                 });
            }
        }
    }

}
