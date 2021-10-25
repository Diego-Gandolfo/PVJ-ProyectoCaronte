using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShopItem : MonoBehaviour
{
    protected bool isActive;

    public abstract void Interact();
}
