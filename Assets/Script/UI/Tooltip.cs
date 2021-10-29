using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private Text header;
    [SerializeField] private Text description;
    [SerializeField] private int characterWrapLimit;
    
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private RectTransform rectTransform;

    void Start()
    {
        layoutElement = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void CheckSize()
    {
        int headerLength = header.text.Length;
        int contentLength = description.text.Length;
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
        //Math.Max(headerfield.preferredWidth, contentField.preferredWidth) >= layoutElement.preferredWidth
    }

    public void CheckPosition()
    {
        Vector2 position = Input.mousePosition;
        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

    public void SetText(string content, string title = "" )
    {
        if (string.IsNullOrEmpty(title))
            header.gameObject.SetActive(false);
        else
        {
            header.gameObject.SetActive(true);
            header.text = title;
        }

        description.text = content;
        CheckSize();
    }
}
