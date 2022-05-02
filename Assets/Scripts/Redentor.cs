using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redentor : MonoBehaviour
{
   public int rutine;
   public float chrono;
   public float grade;
   public float maxHealth = 100;
   public float currentHealth;
   public float timeRemaining = 2;
   public bool attacking;
   
   public Animator ani;
   public Quaternion angle;
   public GameObject target;

   bool isDead;

   void Start()
   {
       ani = GetComponent<Animator>();
       target = GameObject.Find("Player");

       attacking = false;

       currentHealth = maxHealth;
   }

   public void EnemyBehavior()
   {
       if (Vector3.Distance(transform.position, target.transform.position) > 30 && !isDead)
       {
            ani.SetBool("run", false);
            chrono += 1 * Time.deltaTime;
            if (chrono >= 4)
            {
                rutine = Random.Range(0, 2);
                chrono = 0;
            }
            switch (rutine)
            {
            case 0:
                ani.SetBool("walk", false);
                break;
            
            case 1:
                grade = Random.Range(0, 360);
                angle = Quaternion.Euler(0, grade, 0);
                rutine++;
                break;
            
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                transform.Translate(Vector3.forward * -1 * Time.deltaTime);
                ani.SetBool("walk", true);
                break;
            }
       }
       else
       {
           if (Vector3.Distance(transform.position, target.transform.position) > 2 && !attacking && !isDead)
           {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, -3);
                ani.SetBool("walk", false);

                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * -4 * Time.deltaTime);

                ani.SetBool("attack", false);
           }
           else
           {
               ani.SetBool("walk", false);
               ani.SetBool("run", false);

               ani.SetBool("attack", true);
               attacking = true;
           }
       }
   }

   public void EndAnim()
    {
       Debug.Log ("fin ataque");
       ani.SetBool("attack", false);
       attacking = false; 
    }

   void Update()
    {
        EnemyBehavior();

        /* if (attacking = true && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        
        if (timeRemaining <= 0) 
        {
            timeRemaining = 2;
            Debug.Log ("0");
            EndAnim();
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            currentHealth --;

            if(currentHealth <= 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        isDead = true;

        ani.SetBool("Run", false);
        ani.SetBool("Walk", false);
        ani.SetBool("Attack", false);
        ani.SetBool("Dead", true);
    }
}
