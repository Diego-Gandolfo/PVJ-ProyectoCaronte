using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipItem", menuName = "ScriptableObject/ShipItem", order = 1)]
public class ShipItemSO : ScriptableObject
{
    public string title;
    public Sprite image;
    public string description;
    public bool isDisabled;
}
