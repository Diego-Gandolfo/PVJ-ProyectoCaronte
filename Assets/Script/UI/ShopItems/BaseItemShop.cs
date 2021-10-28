using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemShop : MonoBehaviour
{
    [SerializeField] private ItemUISO itemScriptableObject;

    public ItemUISO SO => itemScriptableObject;

    public abstract void Interact();
}
