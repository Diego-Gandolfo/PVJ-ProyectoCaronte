using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalManager : MonoBehaviour
{
    public static CrystalManager instance;
    [SerializeField] private Text text;

    private int crystalCounter;

    public int CrystalNumber => crystalCounter;
    

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void AddCrystal(int number)
    {
        crystalCounter += number;
        UpdateCrystalCounter();
    }

    public void RemoveCrystal(int number)
    {
        crystalCounter -= number;
        UpdateCrystalCounter();
    }


    public void UpdateCrystalCounter()
    {
        text.text = crystalCounter.ToString();
    }
}
