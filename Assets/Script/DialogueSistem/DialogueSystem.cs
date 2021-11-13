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
    [SerializeField] private bool canSkipAllText;
    #endregion

    #region Private Fields

    private bool isReproducingDialogue = false;
    private Animator animator;
    #endregion

    #region Unity Methods

    void Awake()
    {
        DialogueManager.Instance.SetDialogueSystem(this);
        animator = GetComponent<Animator>();
        textComponent.text = string.Empty;
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
    private void Update()
    {
        CheckForDialogue();

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
        isReproducingDialogue = true;
        var dialogueToReproduce = dialogueQueue[0].dialogueStrings;
        for (int i = 0; i < dialogueToReproduce.Count; i++)
        {
            foreach (char character in dialogueToReproduce[i])
            {
                textComponent.text += character;
                yield return new WaitForSeconds(readSpeed);
                    
            }

            //if (Input.GetKeyDown(KeyCode.Return) && canSkipAllText)
            //{
            //    textComponent.text = dialogueToReproduce[i];
            //}


            if (dialogueQueue[0].dialogueStrings.Count == 1)
            {
                yield return new WaitForSeconds(timeOfDialogueToDisappear);

            }
            #region Si se quiere que el jugador pueda pasar el texto con el enter
            if (i < dialogueQueue[0].dialogueStrings.Count)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                {
                    textComponent.text = string.Empty;
                }
            }
            #endregion
            #region Si se quiere que el dialogo se reproduzca automaticamente
            //yield return new WaitForSeconds(timeOfDialogueToDisappear);
            //textComponent.text = string.Empty;
            #endregion
        }
        dialogueQueue.RemoveAt(0);
        isReproducingDialogue = false;
        animator.SetBool("Enabled", false);
    }

    #endregion
}
