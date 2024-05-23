using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] public float attackRange = 0.5f;
    [SerializeField] public int attackDamage = 40;
    public Transform attackPoint;
    public LayerMask enemyLayer; // Ubah nama variabel LayerMask menjadi enemyLayer agar lebih deskriptif
    private Animator anim;
    private playerMovement playerMovement;
    private float cooldownTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<playerMovement>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime; // Update cooldown timer first

        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()) // Moved cooldown timer check here
        {
            Attack();
        }
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<health>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
