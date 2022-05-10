using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretBullet : MonoBehaviour
{
    //public GameObject hitExplosion;
    private GameObject bullet;
    private float speed = 50f;
    private float timeToDestroy = 1f;
    
    //public GameObject targetJugador;
    //public Transform _target;
    //public Vector3 target;

    private Vector3 target;

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void SetBullet(Vector3 target)
    {
        this.target = target;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //bullet.transform.position = Vector3.MoveTowards(targetJugador.transform.position,_target.position,speed*Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        ContactPoint contact = other.GetContact(0);
        //explode();
        return;
    }

    /* void explode()
    {
        GameObject explosion = (GameObject)Instantiate(hitExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explosion, 1f);
    }*/
}
