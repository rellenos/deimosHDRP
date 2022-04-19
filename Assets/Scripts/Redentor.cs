using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redentor : MonoBehaviour
{
   public int rutine;
   public float chrono;
   public Animator ani;
   public Quaternion angle;
   public float grade;

   public GameObject target;
   public bool attacking;

   void Start()
   {
       ani = GetComponent<Animator>();
       target = GameObject.Find("Player");
   }

   public void EnemyBehavior()
   {
       if (Vector3.Distance(transform.position, target.transform.position) > 10)
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
           if (Vector3.Distance(transform.position, target.transform.position) > 2 && !attacking)
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
       ani.SetBool("attack", false);
       attacking = false;
   }

   void Update()
   {
       EnemyBehavior();
   }
}