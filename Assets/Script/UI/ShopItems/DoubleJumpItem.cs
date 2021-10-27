using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpItem : BaseItemShop
{
    public override void Interact()
    {
        LevelManager.instance.Player.SetCanDoDoubleJump();
    }
}
