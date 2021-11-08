using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    private Animator animator;
    [Header("Properties")]
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float readSpeed;
    [SerializeField] private float timeOfDialogueToDisappear;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void StartDialogue(DialogueSO dialog)
    {
        animator.SetBool("Enabled", true);
        StartCoroutine(TypeLine(dialog));
        
    }
    IEnumerator TypeLine(DialogueSO dialogToRead)
    {
        //foreach (char character in dialogToRead.dialogueStrings[]())
        //{
            //textComponent.text += character;
            //yield return new WaitForSeconds(readSpeed);
        //}
        for (int i = 0; i < dialogToRead.dialogueStrings.Count; i++)
        {
            foreach (char character in dialogToRead.dialogueStrings[i])
            {
                textComponent.text += character;
                if (Input.GetKey(KeyCode.Return))
                {
                    yield return new WaitForSeconds(0);
                }
                else
                {
                    yield return new WaitForSeconds(readSpeed);
                }
            }
            if (dialogToRead.dialogueStrings.Count == 1)
            {
                yield return new WaitForSeconds(timeOfDialogueToDisappear);
            }
            #region Si se quiere que el jugador pueda pasar el texto con el enter
            if (i < dialogToRead.dialogueStrings.Count)
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
        animator.SetBool("Enabled", false);
        
    }
}
