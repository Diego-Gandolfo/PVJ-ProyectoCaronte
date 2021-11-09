using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipItemsManager : MonoBehaviour
{
    private List<ShipItemSO> playerList = new List<ShipItemSO>();
    private List<ShipItemSO> shipList = new List<ShipItemSO>();
    public Action OnCompleted;

    public bool CheckIfPlayerHasItem(ShipItemSO item)
    {
        if (playerList.Contains(item))
        {
            return true;
        }
        return false;
    }

    public void AddItemToPlayer(ShipItemSO item)
    {
        if (!playerList.Contains(item))
        {
            playerList.Add(item);
        }
    }

    public void RemoveFromPlayer(ShipItemSO item)
    {
        playerList.Remove(item);
    }

    public void AddItemToShip(ShipItemSO item)
    {
        if (!shipList.Contains(item))
        {
            shipList.Add(item);
            CheckIfCompleted();
        }
    }

    public void CheckIfCompleted()
    {
        if(shipList.Count > HUDManager.instance.ShipManagerUI.itemsNeeded)
        {
            OnCompleted?.Invoke();
        }
    }
}
