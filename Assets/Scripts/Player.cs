using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int life;
    
    private Vector2 direction;
    private bool isGrounded;
    private bool recovery;

    public Rigidbody2D rigidBody;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Jump();
        PlayAnimation();
    }
    
    //Update used for fisic functions
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        rigidBody.velocity = new Vector2(direction.x * speed, rigidBody.velocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            animator.SetInteger("transition", 2);
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void Death()
    {

    }

    //Animations
    void PlayAnimation()
    {
        if (isGrounded)
        {
            //Walking to the right
            if (direction.x > 0)
            {
                animator.SetInteger("transition", 1);
                transform.eulerAngles = Vector2.zero; // Vector2.zero is the same than new Vector2(0,0)
            }

            //Walking to the left
            if (direction.x < 0)
            {
                animator.SetInteger("transition", 1);
                transform.eulerAngles = new Vector2(0, 180);
            }

            //Stoped
            if (direction.x == 0)
            {
                animator.SetInteger("transition", 0);
            }
        }

    }

    public void Hit()
    {
     
        if (!recovery)
        {
            StartCoroutine(Flick());
        }
    }

    IEnumerator Flick()
    {
        recovery = true;
        spriteRenderer.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(1, 1, 1, 1);
        life--;
        recovery = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }
}
