using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    public bool IsOpen { get; private set; }

    private TypeWriterEffect typeWriterEffect;

    private void Start() {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject) {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject) {
        foreach (string dialogue in dialogueObject.Dialogue) {
            yield return typeWriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 3")));
        }

        CloseDialogueBox();
    }

    private void CloseDialogueBox() {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
