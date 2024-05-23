using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Menambahkan variabel untuk kekuatan lompatan
    [SerializeField] private float jumpPower = 20f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private TrailRenderer tr;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpColdown;
    private int jumpRemaining = 2; //batas lompatan
    private float horizontalInput;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private Vector3 respawnPoint;
    public GameObject fallDetector;

    public GameObject PauseMenu;
    public GameObject MainMenu;


    private void Awake()
    {
        // Mengambil referensi rigidbody dan animator dari objek
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        MainMenu.SetActive(false);
    }

    private void Update()
    {
         horizontalInput = Input.GetAxis("Horizontal");
        // Membalik karakter saat bergerak ke kanan atau kiri
        if (isDashing)
        {
            return;
        }

        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        

        // Menetapkan parameter animator   
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        if(wallJumpColdown < 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if(onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
                //menghilangkan batasan lompatan ketika menyentuh dinding
                jumpRemaining = int.MaxValue;
            }
            else
            {
                body.gravityScale = 7;
            }
            if (Input.GetKeyDown(KeyCode.Space)) // Menggunakan GetKeyDown untuk lompatan agar hanya berlaku sekali per pemencetan tombol
            {
                Jump();
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void Jump()
    {
        if (isGrounded()|| jumpRemaining > 0)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower); // Menetapkan kecepatan y ke kekuatan lompatan
            anim.SetTrigger("jump");
            jumpRemaining--;
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpColdown = 0;
           

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded())
        {
            jumpRemaining = 2; //mereset jumlah lompatan
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        anim.SetTrigger("dash");
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Replay()
    {
        SceneManager.LoadScene("level1");
    }
}
