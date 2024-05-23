using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowTraps : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrow;
    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;
        int arrowIndex = FindArrow();

        // Set posisi dan rotasi panah sesuai dengan firePoint
        arrow[arrowIndex].transform.position = firePoint.position;
        arrow[arrowIndex].transform.rotation = firePoint.rotation;

        // Aktifkan panah
        arrow[arrowIndex].SetActive(true);
        arrow[arrowIndex].GetComponent<enemyProjectile>().activateProjectile();
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrow.Length; i++)
        {
            if (!arrow[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
