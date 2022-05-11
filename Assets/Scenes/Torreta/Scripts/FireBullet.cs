using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] public GameObject target;

    [SerializeField] private float timer = 0.33f;

    [SerializeField] private int counter;
    [SerializeField] private int maxCounter = 200;

    Transform Player;

    void Start()
    {   
        target = GameObject.Find("Player");
        StartCoroutine(FireBullets_CR());
    }

    void Update()
    {
        
    }

    void OnTriggerEnterCollider(Collider other)
    {
        //if (Vector3.Distance(transform.position, target.transform.position) < 30)
        //{
            //Debug.Log("Disprando");
            //StartCoroutine(FireBullets_CR());
        //}

        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Disprando");
            //StartCoroutine(FireBullets_CR());
        }
    }

    IEnumerator FireBullets_CR()
    {
        for(int i=0; i<maxCounter; i++)
        {
            counter++;
            Instantiate(bullet, transform.position, transform.rotation);
            yield return new WaitForSeconds(timer);
        }
    }
}
