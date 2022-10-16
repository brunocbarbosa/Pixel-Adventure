using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    public float speed;
    public float jumpForce;
    public int life;
    public int melon;
    
    [Header("Components")]
    private Vector2 direction;
    private bool isGrounded;
    private bool recovery;

    [Header("UI")]
    public TextMeshProUGUI melonText;
    public TextMeshProUGUI lifeText;
    public Rigidbody2D rigidBody;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = life.ToString();
        Time.timeScale = 1;

        //Load player in new scene with statistics 
        DontDestroyOnLoad(gameObject);
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

    public void Trampoline()
    {
        
        animator.SetInteger("transition", 2);
        rigidBody.AddForce(new Vector2(0, jumpForce + 5), ForceMode2D.Impulse);
        isGrounded = false;
        
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
        life--;
        if (life <= 0)
        {
            Death();
        }
        lifeText.text = life.ToString();
        recovery = true;
        for (int i = 0; i < 4; i++)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.2f);
        }

        recovery = false;
    }

    void Death()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }


    public void IncreaseScore()
    {
        melon++;
        melonText.text = melon.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }
}
