using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float delay = 2f;
    private string content;
    private string title;
    private float timer;
    private bool canShow;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (canShow)
            print(timer);
        if(canShow & timer <= 0)
        {
            TooltipSystem.instance.Tooltip.CheckPosition();
            TooltipSystem.instance.Show(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!canShow)
        {
            TooltipSystem.instance.Tooltip.SetText(content, title);
            canShow = true;
            timer = delay;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("no show");
        canShow = false;
        TooltipSystem.instance.Show(false);
    }

    public void SetContent(string content, string title = "")
    {
        this.content = content;
        this.title = title;
    }
}
