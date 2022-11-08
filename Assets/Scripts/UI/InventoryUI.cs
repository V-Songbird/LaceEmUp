using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamOff.Scripts.Managers;

public class InventoryUI : GeneralUI
{

    public GameObject Slot01;
    public GameObject Slot02;
    public GameObject Slot03;


    public override void ShowUI()
    {
        GetInventoryInfo();
        base.ShowUI();
    }

    void GetInventoryInfo()
    {
        ShowSlots();
    }

    void ShowSlots()
    {
        if (GamePlayManager.Instance.Player_Inventory.backpackInventory.Count == 0)
        {
            Slot01.gameObject.SetActive(false);
            Slot02.gameObject.SetActive(false);
            Slot03.gameObject.SetActive(false);
        }
        else if (GamePlayManager.Instance.Player_Inventory.backpackInventory.Count == 1)
        {
            Slot01.gameObject.SetActive(true);
        }
        else if (GamePlayManager.Instance.Player_Inventory.backpackInventory.Count == 2)
        {
            Slot01.gameObject.SetActive(true);
            Slot02.gameObject.SetActive(true);
        }
        else
        {
            Slot01.gameObject.SetActive(true);
            Slot02.gameObject.SetActive(true);
            Slot03.gameObject.SetActive(true);
        }
    }
}
