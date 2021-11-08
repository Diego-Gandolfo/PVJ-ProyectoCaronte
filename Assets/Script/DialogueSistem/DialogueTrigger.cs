using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]private DialogueSO dialogue;
    public void ReproduceDialogue()
    {
        DialogueSystem.instance.StartDialogue(dialogue);
    }
}
