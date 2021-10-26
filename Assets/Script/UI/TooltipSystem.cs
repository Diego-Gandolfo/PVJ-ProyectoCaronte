using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    [SerializeField] private Tooltip tooltip;

    public static TooltipSystem instance;
    public Tooltip Tooltip => tooltip;

    public void Awake()
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

    public void Show(bool value)
    {
        print("mostramos");
        tooltip.gameObject.SetActive(value);
    }

}
