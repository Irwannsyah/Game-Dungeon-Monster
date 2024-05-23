using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBattle : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private Animator anim;

   

    private void Awake()
    {
        // Mengambil referensi komponen Animator dari objek ini
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = 100;
    }

    public void ambilDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("enemyDie");
        anim.SetBool("enemyDie", true);
        this.enabled = false;

       
    }
}
