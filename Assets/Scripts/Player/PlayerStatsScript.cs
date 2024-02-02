using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsScript : MonoBehaviour
{
    public float playerMaxHealth = 50;
    [SerializeField] private int playerMaxXP = 100;
    public int playerXP = 0;
    private int totalXP = 0;
    public int playerLvl = 0;
    public float playerHealth;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider xpBar;
    [SerializeField] private TextMeshProUGUI lvlText;
    private SpriteRenderer sprite;

    [SerializeField] private GameObject gameManager;
    private GameManagerScript gameManagerScript;
    // Start is called before the first frame update
    private void Start()
    {
        playerHealth = playerMaxHealth;
        sprite = GetComponentInChildren<SpriteRenderer>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        healthBar.maxValue = playerMaxHealth;
        healthBar.value = playerMaxHealth; 
        xpBar.maxValue = playerMaxXP;
        xpBar.value = playerXP;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    public void GainXP(int xp)
    {
        //set value
        totalXP += xp; //total for end of game
        playerXP += xp;
        xpBar.value = playerXP;
        if (playerXP == playerMaxXP)
        {
            playerLvl++;
            xpBar.value = 0;
            playerXP = 0;
            playerMaxXP += xp *  playerLvl; //or do 100 + xp * playerLvl
            xpBar.maxValue = playerMaxXP;
        }
        Debug.Log(playerMaxXP);
        Debug.Log(playerXP) ;
        lvlText.text = "Level: " + playerLvl;
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        healthBar.value = playerHealth;
        StartCoroutine(DamageEffectSequence(sprite, Color.red, .5f));
    }

    //code from stackOverFlow
    IEnumerator DamageEffectSequence(SpriteRenderer sr, Color dmgColor, float duration)
    {
        // save origin color
        Color originColor = sr.color;

        // tint the sprite with damage color
        sr.color = dmgColor;

        // lerp animation with given duration in seconds
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            sr.color = Color.Lerp(dmgColor, originColor, t);

            yield return null;
        }

        // restore origin color
        sr.color = originColor;
    }

    private void Die()
    {
        gameManagerScript.GameOver(); // death
        gameManagerScript.Score(totalXP); //show total xp
    }

}
