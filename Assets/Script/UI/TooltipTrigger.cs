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

    private void Start()
    {
        TooltipSystem.instance.OnReset += OnReset;
    }

    private void Update()
    {
        timer -= Time.unscaledTime;
        if(canShow & timer <= 0)
        {
            TooltipSystem.instance.Tooltip.CheckPosition();
            TooltipSystem.instance.Show(true);
        }
    }

    private void OnReset()
    {
        canShow = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!canShow && (content != "" || title != ""))
        {
            TooltipSystem.instance.Tooltip.SetText(content, title);
            TooltipSystem.instance.Tooltip.CheckSize();
            canShow = true;
            timer = delay;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canShow = false;
        TooltipSystem.instance.Show(false);
    }

    public void SetContent(string content, string title = "")
    {
        this.content = content;
        this.title = title;
    }
}
