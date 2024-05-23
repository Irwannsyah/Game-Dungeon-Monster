using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBos : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOffFlashes;
    private SpriteRenderer spriterend;

    [Header("components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    public GameObject GameCompletePanel;



    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriterend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            StartCoroutine(invunerability());
        }
        else
        {
            //player die
            if (!dead)
            {
                Die();
            }
        }
    }

    public void addHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOffFlashes; i++)
        {
            spriterend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));
            spriterend.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

   

    public void Respawn()
    {
        addHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");

        foreach (Behaviour component in components)
            component.enabled = true;

        dead = false; // Pastikan untuk mengatur kembali kehidupan pemain ke false setelah respawn
    }

    private void Die()
    {
        if (GetComponent<playerMovement>() != null)
        {
            GetComponent<playerMovement>().enabled = false;
        }
        if (GetComponentInParent<enemypatrol>() != null)
        {
            GetComponentInParent<enemypatrol>().enabled = false;
        }
        if (GetComponent<meleeEnemy>() != null)
        {
            GetComponent<meleeEnemy>().enabled = false;
            dead = true;
        }

        anim.SetBool("grounded", true);
        anim.SetTrigger("die");
        dead = true;
      
       






    }

    
}
