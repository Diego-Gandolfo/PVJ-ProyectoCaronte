using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void Awake()
    {
        //GameManager.instance.SetPlayer(player);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
            if (!HUDManager.instance.IsQuestVisible)
            {
                HUDManager.instance.VisibleQuest(true);
                HUDManager.instance.UpdateQuest("En una galaxia muy muy lejana, buscar cristales");
                print("prendido");
            }
            else
            {
                HUDManager.instance.VisibleQuest(false);
                print("apagado");
            }   
    }

}
