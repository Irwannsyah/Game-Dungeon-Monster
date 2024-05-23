using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<health>();
    }

    public void Respawn()
    {
        playerHealth.Respawn(); //Restore player health and reset animation
        transform.position = currentCheckpoint.position; //Move player to checkpoint location
        EnableMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform;
            collision.enabled = false;
            collision.GetComponent<Animator>().SetTrigger("activate");
        }
    }

    private void EnableMovement()
    {
        if (GetComponent<playerMovement>() != null)
        {
            GetComponent<playerMovement>().enabled = true;
        }
        if (GetComponentInParent<enemypatrol>() != null)
        {
            GetComponentInParent<enemypatrol>().enabled = true;
        }
        if (GetComponent<meleeEnemy>() != null)
        {
            GetComponent<meleeEnemy>().enabled = true;
        }
    }
}
