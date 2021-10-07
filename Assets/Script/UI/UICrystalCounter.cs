using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICrystalCounter : MonoBehaviour
{
    [SerializeField] private Text text;

    public static UICrystalCounter instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

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
