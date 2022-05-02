using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletDecal;
    public GameObject hitExplosion;
   

    private float speed = 50f;
    private float timeToDestroy = 5f;
    int shootableMask;

    public Vector3 target { get; set; }
    public bool hit { get; set; }

    public int damage = 50;
    

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target ) < .01f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        ContactPoint contact = other.GetContact(0);
        //GameObject.Instantiate(bulletDecal, contact.point + contact.normal * 0.0001f, Quaternion.LookRotation(contact.normal));
        
    
        //Instantiate (hitExplosion, bulletDecal.transform.position, bulletDecal.transform.rotation);
        //Destroy(gameObject);
        explode();
        //return;
    }

    void explode()
    {
        if (hitExplosion  != null)
        {
            GameObject explosion = (GameObject)Instantiate(hitExplosion, transform.position, transform.rotation);
            Destroy(gameObject, 0.5f);
            Destroy(explosion, 1f);
        }
    }
}
