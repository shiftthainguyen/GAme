using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    bool isAttacking = false;

    public Animator anim;

    public GameObject attackPoint01;
    public GameObject attackPoint02;
    public GameObject attackPoint03;
    public GameObject attackPoint04;
   
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        attackPoint01.SetActive(false);
        attackPoint02.SetActive(false);
        attackPoint03.SetActive(false);
        attackPoint04.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool sword = anim.GetBool("sword");
        if (sword)
        {          
                if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
                {
                    isAttacking = true;
                    StartCoroutine(Attack01());
                    Invoke("ResetAttack", .4f);
                }
                if (Input.GetKeyDown(KeyCode.K) && !isAttacking)
                {
                    isAttacking = true;
                    StartCoroutine(Attack02());
                    Invoke("ResetAttack", .4f);
                }
                if (Input.GetKeyDown(KeyCode.L) && !isAttacking)
                {
                    isAttacking = true; 
                    StartCoroutine(Attack03());
                    Invoke("ResetAttack", .4f);
                }
                if (Input.GetKeyDown(KeyCode.H) && !isAttacking)
                {
                    isAttacking = true;
                    StartCoroutine(Attack04());
                    Invoke("ResetAttack", .4f);
                }                  
        }      
    }
    //reset attack
    void ResetAttack()
    {
        isAttacking = false;
    }
    //Do Attack 
    IEnumerator Attack01()
    {
        anim.SetTrigger("attack01");
        yield return new WaitForSeconds(0.2f);
        attackPoint01.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackPoint01.SetActive(false);
        
    }
    IEnumerator Attack02()
    {
        anim.SetTrigger("attack02");
        yield return new WaitForSeconds(0.3f);
        attackPoint02.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackPoint02.SetActive(false);
    }
    IEnumerator Attack03()
    {
        anim.SetTrigger("attack03");
        yield return new WaitForSeconds(0.4f);
        attackPoint03.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackPoint03.SetActive(false);
    }
    IEnumerator Attack04()
    {
        anim.SetTrigger("attackHard");
        yield return new WaitForSeconds(0.4f);
        attackPoint04.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackPoint04.SetActive(false);
    }

}
