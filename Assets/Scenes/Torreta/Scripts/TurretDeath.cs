using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDeath : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public float damage = 33;
    public float destroyDelay = 1;

    public GameObject deathExplosion;
    //public AudioSource deathSound;

    void Start()
    {
        Global.isTurretDead = false;
        currentHealth = maxHealth;
    }
    
    private void OnCollisionEnter(Collision other)    
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            currentHealth = currentHealth - damage;
            //Debug.Log ("muerte");

            if(currentHealth <= 0)
            {
                Global.isTurretDead = true;
                StartCoroutine(Destroy());
            }
        }
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
