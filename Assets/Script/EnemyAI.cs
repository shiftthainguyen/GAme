using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;

    [SerializeField]
    private bool run = false;

    [SerializeField]
    private bool isAttacking = false;

    [SerializeField]
    public Transform target;

    [SerializeField]
    public bool lookingRight = true;

    [SerializeField]
    float rangeChase = 3;

    [SerializeField]
    float rangeAttack = 1;

    [SerializeField]
    private LayerMask layerMark ;

    [SerializeField]
    Transform castPoint;

    [SerializeField]
    public GameObject attackPoint;

    public Rigidbody2D rb;
    public Animator anim;
    void Awake()
    {
        attackPoint.SetActive(false);
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("run", run);
        
        //Can see Player and Flip direction
        if (target.transform.position.x > this.transform.position.x)
        {
            lookingRight = true;            
            Flip(lookingRight);            

        }
        if (target.transform.position.x < this.transform.position.x)
        {
            lookingRight = false;          
            Flip(lookingRight);
           
        }
        //Player collison enemy chase zone
        if (CanSeePlayer(rangeChase) && !CanAttackPlayer(rangeAttack))
        {
            if (lookingRight)
            {
                run = true;
                rb.velocity = new Vector2(-speed, 0);
            }
            else
            {
                run = true;
                rb.velocity = new Vector2(speed, 0);
            }
        }
        /*Enemy can see player and player into rangeAttack and Attack status is false*/
        else if(CanSeePlayer(rangeChase) && CanAttackPlayer(rangeAttack) && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
            Invoke("ResetAttack", 2f);
        }
        else
        {
            run = false;
            rb.velocity = new Vector2(0, 0);
        }

    }
    void ResetAttack()
    {
        isAttacking = false;
    }
    IEnumerator Attack()
    {
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(.4f);
        attackPoint.SetActive(true);
        yield return new WaitForSeconds(.1f);
        attackPoint.SetActive(false);
    }
    bool CanSeePlayer(float dis)
    {       
        float castDist = dis;

        //change direction
        if (lookingRight)
        {
            castDist = -dis;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, layerMark);
        Color raycolor;
        if (hit.collider != null)
        {
            raycolor = Color.red;
        }
        else
        {
            raycolor = Color.green;
        }
        Debug.DrawRay(castPoint.position, Vector2.right * (endPos), raycolor);
        return hit.collider != null;
    }
    bool CanAttackPlayer(float dis)
    {
        bool val = false;
        float castDist = dis;
        if (lookingRight)
        {
            castDist = -dis;
        }
        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, layerMark);
        if (hit.collider != null)
        {
            val = true;
        }
        else
        {
            val = false;
        }
        return val;
    }

    private void Flip(bool dir)
    {
        if (dir)
        {
            lookingRight = !lookingRight;
            Vector3 Scale;
            Scale = transform.localScale;
            Scale.x = 1;
            transform.localScale = Scale;
        }
        else
        {
            lookingRight = !lookingRight;
            Vector3 Scale;
            Scale = transform.localScale;
            Scale.x = -1;
            transform.localScale = Scale;
        }
        
    }
    
}
