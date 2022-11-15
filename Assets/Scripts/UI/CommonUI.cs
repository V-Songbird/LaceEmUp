using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUI : MonoBehaviour
{
    [HideInInspector] public InventoryUI InventoryUI;
    [HideInInspector] public TransitionUI TransitionUI;

    private void Awake()
    {
        InventoryUI = GetComponentInChildren<InventoryUI>();
        TransitionUI = GetComponentInChildren<TransitionUI>();
    }
}
