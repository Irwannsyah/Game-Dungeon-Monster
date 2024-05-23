
using UnityEngine;

public class healthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<health>().addHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
