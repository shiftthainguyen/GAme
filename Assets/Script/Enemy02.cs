using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02 : MonoBehaviour
{
    [SerializeField]
    private bool lookingRight = true;

    [SerializeField]
    public Transform castPoint;

    [SerializeField]
    public Transform target;

    [SerializeField]
    private LayerMask layerMark;

    [SerializeField]
    public float rangeChase = 6;
    [SerializeField]
    public float rangeAttack = 2;

    [SerializeField]
    public float speed = 2f;

    [SerializeField]
    public float bullet = 5;

    [SerializeField]
    private bool run = false;

    [SerializeField]
    private bool reload = false;

    [SerializeField]
    private bool isAttacking = false;

    [SerializeField]
    private bool stopShoot = false;

    [SerializeField]
    public Rigidbody2D r2b;

    [SerializeField]
    public Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        r2b = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("run", run);
        anim.SetBool("shoot", isAttacking);
        isAttacking = false;
        anim.SetBool("stop", stopShoot);
        reload = false;
        anim.SetBool("reload", reload);


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
        //attack
        if (CanSeePlayer(rangeChase) && !CanAttackPlayer(rangeAttack) && !isAttacking)
        {
            run = true;
            Follow();                
            isAttacking = false;
            stopShoot = true;
        }
        else if (CanAttackPlayer(rangeAttack) && CanAttackPlayer(rangeAttack) && !isAttacking)
        {
            run = false;
            if (bullet > 0)
            {
                StartCoroutine(Attack());
                Invoke("ResetAttack", 2f);
                              
            }
            else
            {
                reload = true;
                bullet = 5;
            }                      
        }
        else
        {                    
            run = false;           
            r2b.velocity = new Vector2(0, 0);
        }
    }
    private void Follow()
    {
        if (lookingRight)
        {            
            r2b.velocity = new Vector2(-speed, 0);
        }
        else
        {          
            r2b.velocity = new Vector2(speed, 0);
        }
    }
    IEnumerator Attack()
    {
        isAttacking = true;
        bullet--;
        Debug.Log(bullet);
        yield return new WaitForSeconds(.4f);
    }
    void ResetAttack()
    {
        isAttacking = true;
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
    private void Flip(bool dir)
    {
        if (dir)
        {
            lookingRight = !lookingRight;
            Vector3 scale;
            scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
        else
        {
            lookingRight = !lookingRight;
            Vector3 scale;
            scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }
        
    }
}
