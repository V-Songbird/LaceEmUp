using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamOff.Scripts.Managers;

[CreateAssetMenu(fileName = "NewShoes", menuName = "ScriptableObjects/Inventory/Shoes", order = 1)]
public class InventoryShoe : ScriptableObject
{
    public string shoesName;
    public Sprite shoesImage;

    public ConstantsManager.ShoesTypes myShoeType;
}
