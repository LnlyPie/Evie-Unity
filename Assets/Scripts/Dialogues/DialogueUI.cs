using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    public bool isOpen { get; private set; }

    private TypeWriterEffect typeWriterEffect;

    private void Start() {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject) {
        Player.canMove = false;
        isOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject) {
        foreach (string dialogue in dialogueObject.Dialogue) {
            yield return typeWriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Gamepad.current.buttonWest.wasPressedThisFrame);
        }

        CloseDialogueBox();
    }

    private void CloseDialogueBox() {
        Player.canMove = true;
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
