using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueSystem : MonoBehaviour
{
    public enum Dialogues
    {
        Mission1,
        Oxigen
    }
    public static DialogueSystem instance;
    private Image dialogueBox;
    private Animator animator;
    [Header("Properties")]
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float readSpeed;
    [SerializeField] private float timeOfDialogueToDisappear;
    [Header("Advices")]
    [SerializeField] private string outOfOxigen;
    [Header("Missions")]
    [SerializeField] private string mission1;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        dialogueBox = GetComponent<Image>();
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ShowDialog(Dialogues dialog)
    {
        switch (dialog)
        {
            case Dialogues.Oxigen:
                StartDialogue(outOfOxigen);
                break;
            case Dialogues.Mission1:
                StartDialogue(mission1);
                break;

        }
    }



    void StartDialogue(string dialog)
    {
        animator.SetBool("Enabled", true);
        StartCoroutine(TypeLine(dialog));
        
    }
    IEnumerator TypeLine(string dialogToRead)
    {
        foreach (char character in dialogToRead.ToCharArray())
        {
            textComponent.text += character;
            yield return new WaitForSeconds(readSpeed); 
        }
        yield return new WaitForSeconds(timeOfDialogueToDisappear);
        animator.SetBool("Enabled", false);
        
    }
}
