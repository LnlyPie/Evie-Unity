using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static int health;
    public static int numberOfHearts;

    [Header("Hearts Count")]
    public int numOfHearts;

    [Header("Hearts Sprites")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start() {
        numberOfHearts = numOfHearts;
        health = numberOfHearts;
    }

    void Update() {
        if (health > numberOfHearts) {
            health = numberOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++) {

            if (i < health) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numberOfHearts) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

    public static void TakeDamage(int dmg) {
        health -= dmg;
    }

    public static void SetHealth(int h) {
        health = h;
    }

    public static void SetMaxHealth(int h) {
        numberOfHearts = h;
    }
}
