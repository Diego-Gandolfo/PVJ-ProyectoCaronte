using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolingSystemItem : BaseItemShop
{
    public override void Interact()
    {
        LevelManager.instance.Player.UpgradeCoolingSystem();
    }
}
