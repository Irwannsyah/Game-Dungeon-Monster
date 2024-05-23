using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("FireTrap Timer")]
    [SerializeField]private float activeDelayed;
    [SerializeField]private float activeTime;

    private Animator anim;
    private SpriteRenderer spriterend;

    private bool trigerred; //when the traps get triggered
    private bool active; //when the traps active and can hurt the player
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriterend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!trigerred)
            {
                StartCoroutine(activateFireTrap());
                //trigger api
            }
            if (active)
            {
                collision.GetComponent<health>().TakeDamage(damage);
            }
        }
    }
    private IEnumerator activateFireTrap()
    {
        //turn the sprite to notify the player and triggger the trap
        trigerred = true;
        spriterend.color = Color.red;

        //wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activeDelayed);
        spriterend.color = Color.white; //turn back to initial color
        active = true;
        anim.SetBool("active", true);

        //wait until .. second deactive the trap
        yield return new WaitForSeconds(activeTime);
        active = false;
        trigerred = false;
        anim.SetBool("active", false);


    }
}
