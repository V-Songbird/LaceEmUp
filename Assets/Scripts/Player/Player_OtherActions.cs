using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamOff.Scripts.Managers;

public class Player_OtherActions : MonoBehaviour
{
    [HideInInspector] public InteractObject interactObject;
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
    }
}
