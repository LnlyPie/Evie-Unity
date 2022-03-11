using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 using UnityEngine.UI;
 using TMPro;

public class DebugController : MonoBehaviour
{
    public static DebugCommand EXIT_GAME;
    public static DebugCommand UNLIM_JUMP_MODE;
    public List<object> commandList;

    [SerializeField] private GameObject consoleBox;
    [SerializeField] private TMP_InputField consoleField;
    bool consoleStatus = false;
    string input;

    private void Start() {
        consoleBox.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            OpenConsole();
        }
        if (consoleStatus == true) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                input = consoleField.text.ToString();
                HandleInput();
            }
        }
    }

    private void OpenConsole() {
        if (consoleStatus == false) {
            consoleStatus = true;
            consoleBox.SetActive(true);
        }
        else if (consoleStatus == true) {
            consoleStatus = false;
            consoleBox.SetActive(false);
        }
    }

    private void Awake() {
        EXIT_GAME = new DebugCommand("exit", "Exits the game.", "exit", () => {
            Application.Quit();
        });

        commandList = new List<object> {
            EXIT_GAME
        };
    }

    private void HandleInput() {
        for (int i=0; i<commandList.Count; i++) {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandId)) {
                if (commandList[i] as DebugCommand != null) {
                    (commandList[i] as DebugCommand).Invoke();
                }
            }
        }
    }
}
