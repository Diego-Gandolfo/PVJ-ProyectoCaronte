using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questBox;
    [SerializeField] private Text titleQuest;
    [SerializeField] private Text messageQuest;
    [SerializeField] private float timerQuest;

    private UIQuestBoxAnimation questBoxAnimation;
    private bool canCount;
    private float timer;
    private string title;
    private string message;

    public bool IsMissionActive { get; private set; }

    #region Private
    private void Start()
    {
        questBoxAnimation = GetComponentInChildren<UIQuestBoxAnimation>();
        QuestVisible(false);
        OxygenQuest(); 
    }

    private void Update()
    {
        if (canCount)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                canCount = false;
                QuestVisible(true);
                questBoxAnimation.TriggerShow();
            }
        }
    }

    private void SetQuest()
    {
        QuestVisible(false); //Lo ocultamos
        questBoxAnimation.QuestBoxShow(false); //sacamos la animacion, recien ahi cambiamos los textos e iniciamos el timer
        titleQuest.text = title; 
        messageQuest.text = message;
        timer = timerQuest;
        canCount = true;
    }

    #endregion

    #region Public
    public void QuestVisible(bool value)
    {
        questBox.SetActive(value);
        IsMissionActive = true;
    }

    public void OxygenQuest()
    {
        title = "Take a breath";
        message = "Activate the \n <b>Oxigen Generator</b> \n <size=20><i>(is movin' near you)</i></size>";
        SetQuest();
    }

    public void CrystalQuest()
    {
        title = "Search & Collect";
        message = "Find  \n <b>" + LevelManager.instance.CrystalsNeeded + " CRYSTALS</b> \n <size=20><i>(shoot'em to break)</i></size>";
        SetQuest();
    }

    public void ShowBox(bool value)
    {
        if (value)
            questBoxAnimation.QuestBoxVisible();
        else
            questBoxAnimation.QuestBoxInvisible();
    }

    #endregion
}
