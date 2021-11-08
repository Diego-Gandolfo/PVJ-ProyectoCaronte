using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenUpgradeItem : BaseItemShop
{
    [SerializeField] private float newMaxOxygen;

    public override void Interact()
    {
        LevelManager.instance.Player.UpgradeMaxOygen(newMaxOxygen);
    }
}
