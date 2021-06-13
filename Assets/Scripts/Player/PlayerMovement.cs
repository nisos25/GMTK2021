using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject Flies;
    
    Rigidbody2D rb;

    public float moveSpeed = 12f;
    private Vector2 moveInput;

    private SpriteRenderer mySR;

    private Animator flipAnim;
    private Animator spriteAnim;
    
    // Scripts
    RabbitFalls fallScript;
    LifePoints lifeScript;

    // Start is called before the first frame update
    void Start()
    {
        fallScript = GetComponent<RabbitFalls>();
        lifeScript = GetComponent<LifePoints>();


        rb = GetComponent<Rigidbody2D>();
        mySR = GetComponentInChildren<SpriteRenderer>();
        flipAnim = GameObject.Find("Flipper").GetComponent<Animator>();
        spriteAnim = GameObject.Find("Player Sprite").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
     
        if(lifeScript.lifePoints > 0)
        {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");
            moveInput.Normalize();
        }
        else
        {
            moveInput = Vector2.zero;
        }

        
    }

    private void FixedUpdate()
    {
        if (fallScript.isRabbit)
        {
            Flies.SetActive(false);
            lifeScript.Sprite.gameObject.SetActive(true);
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

            // Animation Walk/Idle
            if (fallScript.isGrounded)
            {
                if (moveInput.x != 0)
                {
                    // Walk animation
                    spriteAnim.SetInteger("moveAnim", 1);
                }
                else
                {
                    // Idle animation
                    spriteAnim.SetInteger("moveAnim", 0);
                }
            }
            else
            {
                // Fall animation
                spriteAnim.SetInteger("moveAnim", 2);
            }

        }
        else
        {
            // Fly animation
            Flies.SetActive(true);
            lifeScript.Sprite.gameObject.SetActive(false);
            spriteAnim.SetInteger("moveAnim", 3);
            rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        }


        FlipAnim();
    }


    void FlipAnim()
    {
        // Flipping in X
        if (!mySR.flipX && moveInput.x < 0)
        {
            mySR.flipX = true;
            flipAnim.SetTrigger("Flip");
        }
        else if (mySR.flipX && moveInput.x > 0)
        {
            mySR.flipX = false;
            flipAnim.SetTrigger("Flip");
        }

    }

}
