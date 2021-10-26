using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string content;
    private string title;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.instance.Show(true);
        TooltipSystem.instance.Tooltip.SetText(content, title);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.instance.Show(false);
    }

    public void SetContent(string content, string title = "")
    {
        this.content = content;
        this.title = title;
    }
}
