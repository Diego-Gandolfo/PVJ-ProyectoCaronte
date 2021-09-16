using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questBox;
    [SerializeField] private Text titleQuest;
    [SerializeField] private Text messageQuest;

    public void QuestVisible(bool value)
    {
        questBox.SetActive(value);
    }

    public void UpdateTitle(string title)
    {
        titleQuest.text = title;
    }

    public void UpdateMessage(string message)
    {
        messageQuest.text = message;
    }

}
