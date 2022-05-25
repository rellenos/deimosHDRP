using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject hitExplosion;
   
    private float speed = 50f;
    private float timeToDestroy = 3f;

    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void OnEnable()
    {
        //Destroy(gameObject, timeToDestroy);
        StartCoroutine(LateCall(timeToDestroy));
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target ) < .01f)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
        //ContactPoint contact = other.GetContact(0);
        explode();
        //return;
        }
    }

    void explode()
    {
        GameObject explosion = (GameObject)Instantiate(hitExplosion, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
        Destroy(explosion, 1f);
    }

    IEnumerator LateCall(float seconds)
     {
        if (gameObject.activeInHierarchy)
            gameObject.SetActive(true);
         
        yield return new WaitForSeconds(seconds);
  
        gameObject.SetActive(false);
     }
}
