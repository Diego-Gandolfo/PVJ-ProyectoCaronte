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
    private void Start()
    {
        InputController.instance.OnSkipDialogue += SkipDialogueListener;
    }
    private void Update()
    {
        CheckForDialogue();
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
        wantToSkip = false;
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
    private void SkipDialogueListener()
    {
        wantToSkip = true;
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
            for (int i1 = 0; i1 < dialogueLines[currentLine].Length; i1++)
            {
                char character = dialogueLines[currentLine][i1];
                if (!wantToSkip)
                { 
                textComponent.text += character;
                yield return new WaitForSeconds(readSpeed);
                }
                else
                {
                    SkipAllText();
                    i1 = dialogueLines[currentLine].Length;
                }
            }
            yield return new WaitForSeconds(timeOfDialogueToDisappear);
            NextLine();
        }
    }

  #endregion
}
