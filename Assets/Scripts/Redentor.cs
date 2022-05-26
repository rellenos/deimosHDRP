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
   public float damage = 7;
   public float destroyDelay = 2.2f;

   public bool attacking;
   public bool isDead;

   public GameObject deathExplosion;
   public GameObject target;
   public Animator ani;
   public Quaternion angle;
   //public AudioSource deathSound;
   

   void Start()
   {
       ani = GetComponent<Animator>();
       target = GameObject.Find("Player");

       attacking = false;
       Global.isRedentorDead = false;

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
                transform.Translate(Vector3.forward * -6 * Time.deltaTime);

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
       //Debug.Log ("fin ataque");
       ani.SetBool("attack", false);
       attacking = false; 
    }

    void Update()
    {
        if(!isDead)
        {
            EnemyBehavior();
            //Debug.Log (currentHealth);
        }
    }

    private void OnCollisionEnter(Collision other)    
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            currentHealth = currentHealth - damage;
            //Debug.Log ("muerte");

            if(currentHealth <= 0)
            {
                Global.isRedentorDead = true;
                Death();
            }
        }
    }

    void Death()
    {
        isDead = true;
        ani.SetTrigger("dead");
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
        GameObject explosion = (GameObject)Instantiate(deathExplosion, transform.position, transform.rotation);
        //deathSound.Play();
        Destroy(explosion, 2f);
    }
}
