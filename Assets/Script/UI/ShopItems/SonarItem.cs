using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarItem : BaseItemShop
{
    public override void Interact()
    {
        CrystalManager.instance.ActivateSonar();
    }
}
