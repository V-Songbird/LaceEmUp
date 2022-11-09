using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamOff.Scripts.Managers;

public class InteractObject : MonoBehaviour
{
    [SerializeField] Transform targetPosition;
    public ConstantsManager.ShoesTypes interactType;
    bool CanInteract = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckForInteract();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanInteract = false;
            GamePlayManager.Instance.Player_OtherActions.interactObject = null;
        }
    }

    void CheckForInteract()
    {
        if (interactType == GamePlayManager.Instance.Player_Inventory.actualShoes)
        {
            GamePlayManager.Instance.Player_OtherActions.interactObject = this;
            CanInteract = true;
        }

    }

    public void MakeInteract()
    {
        switch (interactType)
        {
            case ConstantsManager.ShoesTypes.MarioBross:

                GamePlayManager.Instance.Player_CutActions.DisablePhysicsAndMovements();
                LeanTween.move(GamePlayManager.Instance.Player, targetPosition.position, 1f).setEaseInQuart().setOnComplete(() =>
                {
                    GamePlayManager.Instance.Player_CutActions.GetBackPhysicsAndMovement();
                });

                break;
        }
    }
}
