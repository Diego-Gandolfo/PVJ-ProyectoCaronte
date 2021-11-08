using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueObject", menuName = "Default Dialogue", order = 0)]
public class DialogueSO : ScriptableObject
{
    [Header("Dialogue")]
    public List<string> dialogueStrings = new List<string>();
}

