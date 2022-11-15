using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JamOff.Scripts.Managers;

public class Player_Inventory : MonoBehaviour
{
    public int Coins;

    public List<InventoryShoe> backpackInventory = new List<InventoryShoe>();

    public ConstantsManager.ShoesTypes actualShoes;

    public Scrollbar ChangeShoes;


}
