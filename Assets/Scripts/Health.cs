using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;

    // This is only for player;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite midHeart;
    public Sprite emptyHeart;

    [SerializeField]
    private bool isDead = false;

    public GameObject gameOver;


    private void FixedUpdate()
    {
        if (tag == "player") UpdateHeartSprites();
    }

    private void UpdateHeartSprites()
    {
        
        for (int i = 0; i < 4; i++)
        {
            if (i * 2 + 2  <= currentHealth) hearts[i].sprite = fullHeart;
            else if (i * 2 + 1 <= currentHealth) hearts[i].sprite = midHeart;
            else hearts[i].sprite = emptyHeart;
        }
        
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        
        if (isDead) return;
        if (sender.layer == gameObject.layer) return;

        currentHealth -= amount;

        if (currentHealth <= 0) 
        {
            if (tag != "player")
            {
                isDead = true;
                GameManager.Instance.player._killCounter++;
                Destroy(gameObject);
            }
            else
            {
                gameOver.SetActive(true);
                Time.timeScale = 0f;
                GameManager.Instance.player.audioManager.musicSource.Stop();
            }
        }
        
    }
}
