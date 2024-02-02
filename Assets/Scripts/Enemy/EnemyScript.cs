using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private int enemyMaxHealth = 50;
    public float enemyHealth;
    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    [SerializeField] private EnemyWalkScript enemyWalk; //for flipping attackmask

    // to update xp of player
    private GameObject player;
    private PlayerStatsScript playerStats;

    //for enemy healthbar
    [SerializeField] private EnemyHealthBarScript healthBar;
    [SerializeField] private Image hp;
    [SerializeField] private Image border;

    private Collider2D enemyCollider;
    private bool isWalking;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStatsScript>();

        enemyHealth = enemyMaxHealth;
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        hp = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        isWalking = !(playerStats.playerHealth <= 0);
        animator.SetBool("IsWalking", isWalking);
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        healthBar.SetHealth(enemyHealth);
        if (enemyHealth <= 0)
        {
            animator.ResetTrigger("Attack");
            animator.SetBool("IsDead", true);
            Destroy(enemyCollider);
            Die();
        }
        else
        {
            animator.SetTrigger("IsHit");
        }

    }

    private void Die()
    {
        sprite.sortingOrder = 3;
        playerStats.GainXP(5);
        Destroy(hp, .5f);
        Destroy(border, .5f);
        Destroy(gameObject, 20f);
    }

    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int attackDamage = 15;
    [SerializeField] private LayerMask attackMask;

    public void Attack()
    {
        if (playerStats.playerHealth <= 0)
        {
            animator.SetBool("Idle", true);
        }
        Vector3 pos = transform.position;
        if (rb.position.x < player.transform.position.x) // flipped position change flip attack collider
        {
            attackOffset.x = -attackOffset.x;
        }
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerStatsScript>().TakeDamage(attackDamage);
            Debug.Log(playerStats.playerHealth);
            
        }

    }
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);
    }

}
