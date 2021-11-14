using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueSystem : MonoBehaviour
{
    #region Serialized Fields

    [Header("Properties")]
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float readSpeed;
    [SerializeField] private float timeOfDialogueToDisappear;
    [SerializeField] private List<DialogueSO> dialogueQueue = new List<DialogueSO>();
    #endregion

    #region Private Fields

    private List<String> dialogueLines = new List<string>();
    private bool isReproducingDialogue = false;
    private Animator animator;
    private int currentLine;
    private bool wantToSkip;
    #endregion

    #region Unity Methods

    void Awake()
    {
        DialogueManager.Instance.SetDialogueSystem(this);
        animator = GetComponent<Animator>();
        textComponent.text = string.Empty;
    }
    private void Update()
    {
        CheckForDialogue();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            wantToSkip = true;
        }
        else
        {
            wantToSkip = false;
        }

    }
    void NextLine()
    {
        if(currentLine < dialogueLines.Count)
        {
            textComponent.text = string.Empty;
            currentLine++;
        }
        if (currentLine >= dialogueLines.Count)
        {
            ReproduceNextDialogue();
        }
    }
    void SkipAllText()
    {
        textComponent.text = dialogueLines[currentLine];
    }
    void ReproduceNextDialogue()
    {

        dialogueQueue.RemoveAt(0);
        isReproducingDialogue = false;
        animator.SetBool("Enabled", false);
        
    }
    #endregion

    #region Public Methods

    public void AddToDialogueQueue(DialogueSO dialog)
    {
        dialogueQueue.Add(dialog);
    }
    public void StartDialogue()
    {
        animator.SetBool("Enabled", true);
        StartCoroutine(TypeLine());

    }
    private void CheckForDialogue()
    {
        if (!isReproducingDialogue && dialogueQueue.Count > 0)
        {
            StartDialogue();
        }
    }
    #endregion
    
    #region Coroutines

    IEnumerator TypeLine()
    {
        currentLine = 0;
        isReproducingDialogue = true;
        dialogueLines = dialogueQueue[0].dialogueStrings;
        for (int i = 0; i < dialogueLines.Count; i++)
        {
            foreach (char character in dialogueLines[currentLine])
            {
                if (!wantToSkip)
                { 
                textComponent.text += character;
                yield return new WaitForSeconds(readSpeed);
                }
                else
                {
                    SkipAllText();
                }
            }
            yield return new WaitForSeconds(timeOfDialogueToDisappear);
            NextLine();
        }
    }

  #endregion
}
