using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireAttackScript : MonoBehaviour
{
    public float damage = 12.5f;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject particleEffect;

    // Start is called before the first frame update
    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyScript enemy = collision.GetComponent<EnemyScript>();
        PlayerControllerScript player = collision.GetComponent<PlayerControllerScript>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if (player == null) {
            GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
            GameObject particles = Instantiate(particleEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(effect, 1f);
            Destroy(particles, 3f);
        }

    }

}
