using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private PromptTrigger promptTrigger;
    [SerializeField] private LifeBarController lifeBar;
    public static HUDManager instance; 

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // Esto lo comente porque como esta puesto en un hijo, no funciona
        }

    }

    void Update()
    {
        
    }

    public void ShowPrompt(bool value)
    {
        promptTrigger.ShowPrompt(value);
    }

    public void ChangeCrystalAmount(int value)
    {
        //Do something
    }

    public LifeBarController GetLifeBar()
    {
        return lifeBar;
    }

}
