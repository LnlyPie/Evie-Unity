using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 using UnityEngine.UI;
 using TMPro;

public class DebugController : MonoBehaviour
{
    GameJoltUIScript gamej = new GameJoltUIScript();

    // Commands
    public static DebugCommand EXIT_GAME;
    public static DebugCommand UNLIM_JUMP_MODE;
    public static DebugCommand CURSOR_VIS;
    public static DebugCommand SHOW_DEBUG;
    public static DebugCommand GHOST_MODE;
    public static DebugCommand FLY_MODE;
    public static DebugCommand HELP;

    public List<object> commandList;

    [SerializeField] private GameObject consoleBox;
    [SerializeField] private TMP_Text outputText;
    [SerializeField] private TMP_InputField consoleField;
    bool consoleStatus = false;

    bool showHelp;
    bool cursor_visible = true;
    public static bool debug_visible = false;
    string input;

    private void Start() {
        consoleBox.SetActive(false);
        outputText.text = "";
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown("joystick button 7")) {
            OpenConsole();
        }

        if (consoleStatus == true) {
            if (gamej.playerLoggedIn == true) {
                gamej.UnlockTrophy(158872);
            }
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

        CURSOR_VIS = new DebugCommand("cursor_vis", "Shows/Hides cursor.", "cursor_vis", () => {
            if (cursor_visible) {
                Cursor.visible = false;
                cursor_visible = false;
            } else {
                Cursor.visible = true;
                cursor_visible = true;
            }
        });

        SHOW_DEBUG = new DebugCommand("show_debug", "Shows/Hides debug info.", "show_debug", () => {
            if (debug_visible) {
                debug_visible = false;
            } else {
                debug_visible = true;
            }
        });

        GHOST_MODE = new DebugCommand("ghost", "Enables/Disables player collisions", "ghost", () => {
            if (Player.ghost_mode == false) {
                Player.bc.isTrigger = true;
                Player.ghost_mode = true;
            } else {
                Player.bc.isTrigger = false;
                Player.ghost_mode = false;
            }
        });

        FLY_MODE = new DebugCommand("fly", "Enables/Disables player physics", "fly", () => {
            if (Player.fly_mode == false) {
                Player.rb.gravityScale = 0.0f;
                Player.fly_mode = true;
            } else {
                Player.rb.gravityScale = Player.gravity;
                Player.fly_mode = false;
            }
        });

        HELP = new DebugCommand("help", "Shows a list of commands", "help", () => {
            // temporary done like this
            outputText.text = "\'help\' - Shows a list of commands \n\'exit\' - Exits the game \n\'unlim_jump\' - Enables/Disables Unlimited Jump Mode \n\'cursor_vis\' - Shows/Hides cursor\n\'show_fps\' - Shows/Hides framerate";
        });

        commandList = new List<object> {
            HELP,
            EXIT_GAME,
            UNLIM_JUMP_MODE,
            CURSOR_VIS,
            SHOW_DEBUG,
            GHOST_MODE,
            FLY_MODE
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