using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 using UnityEngine.UI;
 using TMPro;

public class DebugController : MonoBehaviour
{
    // Commands
    public static DebugCommand EXIT_GAME;
    public static DebugCommand UNLIM_JUMP_MODE;
    public static DebugCommand HELP;

    public List<object> commandList;

    [SerializeField] private GameObject consoleBox;
    [SerializeField] private TMP_Text outputText;
    [SerializeField] private TMP_InputField consoleField;
    bool consoleStatus = false;
    bool showHelp;
    string input;

    private void Start() {
        consoleBox.SetActive(false);
        outputText.text = "";
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            OpenConsole();
        }

        if (consoleStatus == true) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                input = consoleField.text.ToString();
                HandleInput();
                consoleField.text = "";
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

        UNLIM_JUMP_MODE = new DebugCommand("unlim_jump", "Enables/Disables Unlimited Jump Mode.", "unlim_jump", () => {
            if (Player.unlimitedJumpMode == false) {
                Player.unlimitedJumpMode = true;
            } else if (Player.unlimitedJumpMode == true) {
                Player.unlimitedJumpMode = false;
            }
        });

        HELP = new DebugCommand("help", "Shows a list of commands", "help", () => {
            // temporary done like this
            outputText.text = "\'help\' - Shows a list of commands \n\'exit\' - Exits the game \n\'unlim_jump\' - Enables/Disables Unlimited Jump Mode";
        });

        commandList = new List<object> {
            HELP,
            EXIT_GAME,
            UNLIM_JUMP_MODE
        };
    }

    private void HandleInput() {
        string[] properties = input.Split(' ');

        for (int i=0; i<commandList.Count; i++) {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (input.Contains(commandBase.commandId)) {
                (commandList[i] as DebugCommand).Invoke();
            }
        }
    }
}