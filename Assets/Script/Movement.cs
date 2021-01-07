using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    public Rigidbody2D r2;   
    public Animator anim;
    public GameObject colliderx;
    
    float movement =0f;
    private bool movingRight = true;

    private bool sword = false;

    //Dashing
    public float dashDistance = 4f;
    bool isDashing;
    float doubleTaptime;
    KeyCode lastKeycode;


    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
       
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(movement));
        anim.SetBool("sword", sword);


        //jump
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            r2.AddForce(Vector2.up * 300f);
        }
        
        //change status
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sword = !sword;
        }     

        //Dash

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash(movingRight));
        }
    }
    IEnumerator Dash(bool direction)
    {
        isDashing = true;
        if (direction)
        {
            r2.velocity = new Vector2(r2.velocity.x, 0f);
            r2.AddForce(new Vector2(dashDistance * 1, 0f), ForceMode2D.Impulse);
            float gravity = r2.gravityScale;
            r2.gravityScale = 0;
            anim.SetTrigger("dash");
            yield return new WaitForSeconds(0.4f);
            isDashing = false;
            r2.gravityScale = gravity;
        }
        else
        {
            r2.velocity = new Vector2(r2.velocity.x, 0f);
            r2.AddForce(new Vector2(dashDistance * -1, 0f), ForceMode2D.Impulse);
            float gravity = r2.gravityScale;
            r2.gravityScale = 0;
            colliderx.SetActive(false);
            anim.SetTrigger("dash");
            yield return new WaitForSeconds(0.3f);
            colliderx.SetActive(true);
            isDashing = false;
            r2.gravityScale = gravity;
            // 
        }


    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            movement = Input.GetAxis("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;
        }
        
        if (movement > 0 && !movingRight)
        {
            Flip();
        }
        if (movement<0 && movingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        movingRight = !movingRight;
        Vector3 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
    
}
