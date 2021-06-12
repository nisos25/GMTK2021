using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitFalls : MonoBehaviour
{
    // General variables
    Rigidbody2D rb;

    // Jump variables
    [Range(1, 10)]
    public float upVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // Check ground variables
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundMask;

    //private Animator spriteAnim;

    public float waitTime = 0.2f;


    // Scripts
    LifePoints lifeScript;
    public bool isRabbit = true;

    private void Awake()
    {
        lifeScript = GetComponent<LifePoints>();
        rb = GetComponent<Rigidbody2D>();
        
        //spriteAnim = GameObject.Find("Player Sprite").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if is on ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundMask);

        if (Input.GetKeyDown(KeyCode.J) && lifeScript.lifePoints > 0)
        {
            TransformIntoAnimal();
        }

        // Control jump fall speed
        if (isRabbit && !isGrounded)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

    }

    // Funcion donde se transforma de insecto a conejo y viceversa
    void TransformIntoAnimal()
    {
        if (isRabbit)
        {
            rb.velocity = Vector2.up *  upVelocity;
            StartCoroutine(WaitToRabbit());

        }
        else
        {
            rb.gravityScale = 1;
            isRabbit = true;
        }

    }

    private IEnumerator WaitToRabbit()
    {
        yield return new WaitForSeconds(waitTime);
        rb.gravityScale = 0;
        isRabbit = false;
    }

}
