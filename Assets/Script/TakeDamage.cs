using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "attack01")
        {
            anim.SetTrigger("hit");
            Debug.Log("Attack01 da hoat dong");
        }
        if (collision.gameObject.name == "attack02")
        {
            anim.SetTrigger("hit");
            Debug.Log("Attack02 da hoat dong");
        }
        if (collision.gameObject.name == "attack03")
        {
            anim.SetTrigger("hit");
            Debug.Log("Attack03 da hoat dong");
        }
        if (collision.gameObject.name == "attack04")
        {
            anim.SetTrigger("hit");
            Debug.Log("AttackHard da hoat dong");
        }

    }
}
