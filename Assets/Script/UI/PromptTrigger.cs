using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptTrigger : MonoBehaviour
{
    [SerializeField] private GameObject promptBox;
    [SerializeField] private Text text;

    void Start()
    {
        ShowPrompt(false);
    }

    public void ShowPrompt(bool value)
    {
        promptBox.SetActive(value);
    }

    public void ChangeLetter(string value) //Ni idea si esto es necesario, pero si necesitamos cambiar la letra desde andentro.. con esto se puede
    {
        text.text = value.ToString();
    }


}
