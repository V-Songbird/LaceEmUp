using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShoes", menuName = "ScriptableObjects/Inventory/Shoes", order = 1)]
public class InventoryShoe : ScriptableObject
{
    public string shoesName;
    public Sprite shoesImage;
}
