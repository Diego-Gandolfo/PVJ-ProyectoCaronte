using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    #region Serialized Fields

    [Header("Properties")]
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float readSpeed;
    [SerializeField] private float timeOfDialogueToDisappear;

    #endregion

    #region Private Fields

    private Animator animator;

    #endregion

    #region Unity Methods

    void Start()
    {
        DialogueManager.Instance.SetDialogueSystem(this);
        animator = GetComponent<Animator>();
        textComponent.text = string.Empty;
    }

    #endregion

    #region Public Methods

    public void StartDialogue(DialogueSO dialog)
    {
        animator.SetBool("Enabled", true);
        StartCoroutine(TypeLine(dialog));
    }

    #endregion

    #region Coroutines

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

    #endregion
}
