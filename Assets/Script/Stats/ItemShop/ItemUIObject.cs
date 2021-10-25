using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "ScriptableObject/ItemShop", order = 1)]
public class ItemUIObject : ScriptableObject
{
    public string title;
    public Sprite image;
    public int price;
    public string description;
    public BaseShopItem interaction;
}
