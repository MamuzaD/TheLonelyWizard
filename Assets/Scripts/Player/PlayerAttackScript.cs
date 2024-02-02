using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private Camera cam;
    [SerializeField] private float fireForce = 5f;

    [SerializeField] private float attackSpeed = 5f;
    [SerializeField] private float nextAttack = 0f;

    private Vector3 mousePos;
    private Vector2 lookDir;
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {

        if (Time.time >= nextAttack)
        {
            if (Input.GetButton("Fire1"))
            {
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                lookDir = (mousePos - firePoint.position).normalized;
                Shoot();
                nextAttack = Time.time + 1f / attackSpeed;
            }
        }
    }

    private void Shoot()
    {
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        firePoint.eulerAngles = new Vector3(0, 0, angle);

        //spawn bullet
        GameObject bullet = Instantiate(firePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        Destroy(bullet, 5f);
    }
}
