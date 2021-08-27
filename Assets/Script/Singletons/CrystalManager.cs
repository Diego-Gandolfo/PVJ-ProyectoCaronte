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
            //DontDestroyOnLoad(gameObject); // Esto lo comente porque como esta puesto en un hijo, no funciona
        }

    }

    public void AddCrystal(int number)
    {
        crystalCounter += number;
        UpdateCrystalCounter();
        CheckCrystalsAmountForDemoQuest();
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

    // Temporal para la DemoQuest
    private void CheckCrystalsAmountForDemoQuest() {
        if (crystalCounter >= 30) Invoke("Victory", 0.5f);
    }

    private void Victory() {
        GameManager.instance?.Victory();
    }
}
