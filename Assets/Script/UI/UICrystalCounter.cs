using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICrystalCounter : MonoBehaviour
{
    [SerializeField] private Text text;

    private void Start()
    {
        LevelManager.instance.OnCrystalUpdate += UpdateCrystalCounter;
    }

    public void UpdateCrystalCounter(int number)
    {
        text.text = number.ToString();
    }

    public void OnDestroy()
    {
        LevelManager.instance.OnCrystalUpdate -= UpdateCrystalCounter;
    }
}
