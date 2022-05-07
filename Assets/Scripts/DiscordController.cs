using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public static Discord.Discord discord = new Discord.Discord(956575345862139954, (System.UInt64)Discord.CreateFlags.Default);

    public static void ChangeActivity(string details, string largeIcon, string largeText) {
        /*var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity {
            Details = details,
            State = Variables.version,
            Assets = {
                LargeImage = largeIcon,
                LargeText = largeText
            }
        };
        activityManager.UpdateActivity(activity, (res) => {
            if (res == Discord.Result.Ok) {
                Debug.Log("Discord Status Set!");
            } else {
                Debug.LogError("Discord Status Failed!");
            }
        });*/
    }

    void Update()
    {
        // discord.RunCallbacks();
    }
}
