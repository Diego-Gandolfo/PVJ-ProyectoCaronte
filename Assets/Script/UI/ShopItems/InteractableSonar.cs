using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSonar : BaseShopItem
{
    public override void Interact()
    {
        if (!isActive)
        {
            isActive = true;
            CrystalManager.instance.ActivateSonar();
        }
    }
}
