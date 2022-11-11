using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamOff.Scripts.Managers;

public class Player_OtherActions : MonoBehaviour
{
    [HideInInspector] public InteractObject interactObject;

    [SerializeField] GameObject BluePortal;
    [SerializeField] GameObject RedPortal;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            GamePlayManager.Instance.CommonUI.InventoryUI.Interact();
        }

        if (Input.GetKeyDown(KeyCode.T) && interactObject != null)
        {
            interactObject.MakeInteract();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LeftClick();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RightClick();
        }
    }


    void LeftClick()
    {
        switch (GamePlayManager.Instance.Player_Inventory.actualShoes)
        {

            case ConstantsManager.ShoesTypes.None:

                break;

            case ConstantsManager.ShoesTypes.MarioBross:

                break;

            case ConstantsManager.ShoesTypes.Portal:
                GamePlayManager.Instance.PortalsManager.MakePortal(true);
                break;
        }
    }

    void RightClick()
    {
        switch (GamePlayManager.Instance.Player_Inventory.actualShoes)
        {

            case ConstantsManager.ShoesTypes.None:

                break;

            case ConstantsManager.ShoesTypes.MarioBross:

                break;

            case ConstantsManager.ShoesTypes.Portal:
                GamePlayManager.Instance.PortalsManager.MakePortal(false);
                break;
        }
    }

}
