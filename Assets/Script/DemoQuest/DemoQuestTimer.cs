using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoQuestTimer : MonoBehaviour
{
    [SerializeField] private float gameDuration;
    [SerializeField] private Text timerText;

    private float remainingTime;

    private void Start()
    {
        remainingTime = gameDuration;
    }

    private void Update()
    {
        remainingTime -= Time.deltaTime;

        float minutes = Mathf.FloorToInt(remainingTime / 60);
        float seconds = Mathf.FloorToInt(remainingTime % 60);

        if (remainingTime <= 0) GameManager.instance?.GameOver();
        else timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
