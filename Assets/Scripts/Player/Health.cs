using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

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

    [Header("Damage")]
    public Tilemap damageTilemap;
    float timeOnCollision = 0f;
    public float timeThreshold;

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

    public static void DealDamage(int dmg) {
        health -= dmg;
    }

    public static void SetHealth(int h) {
        health = h;
    }

    public static void SetMaxHealth(int h) {
        numberOfHearts = h;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            // Timer Reset
            timeOnCollision = 0f;

            DealDamage(1);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            if (timeOnCollision < timeThreshold) {
                timeOnCollision += Time.deltaTime;
            } else {
                DealDamage(1);

                timeOnCollision = 0f;
            }
        }
    }
}
