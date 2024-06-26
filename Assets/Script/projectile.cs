/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private Collider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        boxCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit)
        {
            return;
        }
        float movementSpeed = speed * Time.deltaTime *direction;
        transform.Translate(movementSpeed, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }
    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactive()
    {
        gameObject.SetActive(false);
    }

}*/
