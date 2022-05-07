using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DebugController : MonoBehaviour
{
    GameJoltUIScript gamej = new GameJoltUIScript();

    // Commands
    public static DebugCommand EXIT_GAME;
    public static DebugCommand UNLIM_JUMP_MODE;
    public static DebugCommand SHOW_DEBUG;
    public static DebugCommand GHOST_MODE;
    public static DebugCommand FLY_MODE;

    public static DebugCommand<int> SET_TIME;
    public static DebugCommand<int> SET_HEALTH;
    public static DebugCommand<int> SET_MAX_HEALTH;
    public static DebugCommand<int> DAMAGE_PLAYER;

    public List<object> commandList;

    [SerializeField] private GameObject consoleBox;
    [SerializeField] private TMP_Text outputText;
    [SerializeField] private TMP_InputField consoleField;
    private static GameObject consoleBoxS;
    public static bool consoleStatus = false;

    bool showHelp;
    public static bool cursor_visible = false;
    public static bool debug_visible = false;
    string input;

    private void Start() {
        consoleBox.SetActive(false);
        consoleBoxS = consoleBox;
        outputText.text = "";
        input = null;
    }

    void Update() {
        if (consoleStatus) {
            if (gamej.playerLoggedIn) {
                gamej.UnlockTrophy(158872);
            }
            if (Input.GetKeyDown(KeyCode.Return)) {
                input = consoleField.text.ToString();
                HandleInput();
                consoleField.text = "";
            }
        }

        if (!cursor_visible) {
            Cursor.visible = false;
        } else {
            Cursor.visible = true;
        }
    }

    public static void OpenConsole() {
        if (!consoleStatus) {
            consoleStatus = true;
            cursor_visible = true;
            consoleBoxS.SetActive(true);
        }
        else if (consoleStatus) {
            consoleStatus = false;
            cursor_visible = false;
            consoleBoxS.SetActive(false);
        }
    }

    public void OnDebug(InputAction.CallbackContext context) {
        DebugController.OpenConsole();
    }

    private void Awake() {
        EXIT_GAME = new DebugCommand("exit", "Exits the game.", "exit", () => {
            Application.Quit();
        });

        UNLIM_JUMP_MODE = new DebugCommand("unlim_jump", "Enables/Disables Unlimited Jump Mode.", "unlim_jump", () => {
            if (Player.unlimitedJumpMode) {
                Player.unlimitedJumpMode = true;
            } else if (Player.unlimitedJumpMode == true) {
                Player.unlimitedJumpMode = false;
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
            if (Player.ghost_mode) {
                Player.bc.isTrigger = true;
                Player.ghost_mode = true;
            } else {
                Player.bc.isTrigger = false;
                Player.ghost_mode = false;
            }
        });

        FLY_MODE = new DebugCommand("fly", "Enables/Disables player physics", "fly", () => {
            if (Player.fly_mode) {
                Player.rb.gravityScale = 0.0f;
                Player.fly_mode = true;
            } else {
                Player.rb.gravityScale = Player.gravity;
                Player.fly_mode = false;
            }
        });

        SET_TIME = new DebugCommand<int>("set_time", "Sets timeScale", "set_time <num>", (x) => {
            Time.timeScale = x;
        });

        SET_HEALTH = new DebugCommand<int>("set_health", "Sets Player Health", "set_health <num>", (x) => {
            Health.SetHealth(x);
        });

        SET_MAX_HEALTH = new DebugCommand<int>("set_max_health", "Sets Player Max Health", "set_max_health <num>", (x) => {
            Health.SetMaxHealth(x);
        });

        DAMAGE_PLAYER = new DebugCommand<int>("damage_player", "Takes damage to the player", "damage_player <num>", (x) => {
            Health.DealDamage(x);
        });

        commandList = new List<object> {
            EXIT_GAME, UNLIM_JUMP_MODE, SHOW_DEBUG, 
            GHOST_MODE, FLY_MODE, SET_TIME, 
            SET_HEALTH, SET_MAX_HEALTH, DAMAGE_PLAYER
        };
    }

    private void HandleInput() {
        string[] properties = input.Split(' ');

        for (int i=0; i<commandList.Count; i++) {
            DebugCommandBase commandBase = (commandList[i] as DebugCommandBase);

            if (input.Contains(commandBase.commandId)) {
                if (!input.Any(Char.IsWhiteSpace)) {
                    Debug.Log(input);
                    (commandList[i] as DebugCommand).Invoke();
                } else {
                    Debug.Log(input);
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
            }
        }
    }
}