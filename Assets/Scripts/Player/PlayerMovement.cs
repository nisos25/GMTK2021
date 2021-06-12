using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;

    public float moveSpeed = 12f;
    private Vector2 moveInput;

    private SpriteRenderer mySR;

    private Animator flipAnim;
    // private Animator spriteAnim;

    RabbitFalls fallScript;

    // Start is called before the first frame update
    void Start()
    {
        fallScript = GetComponent<RabbitFalls>();

        rb = GetComponent<Rigidbody2D>();
        mySR = GetComponentInChildren<SpriteRenderer>();
        flipAnim = GameObject.Find("Flipper").GetComponent<Animator>();
        // spriteAnim = GameObject.Find("Player Sprite").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        
    }

    private void FixedUpdate()
    {
        if (fallScript.isRabbit)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        }
        //spriteAnim.SetFloat("Move Speed", rb.velocity.magnitude);

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
