using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUI : MonoBehaviour
{
    [HideInInspector] public InventoryUI InventoryUI;

    private void Awake()
    {
        InventoryUI = GetComponentInChildren<InventoryUI>();
    }
}
