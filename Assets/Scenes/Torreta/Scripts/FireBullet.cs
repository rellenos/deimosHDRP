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

    public bool playerDetected = false;

    void Start()
    {   
        target = GameObject.Find("Player");
        //StartCoroutine(FireBullets_CR());
    }

    void Update()
    {

        if(Vector3.Distance(transform.position, target.transform.position) < 25 && !playerDetected)
        {
            playerDetected = true;
            StartCoroutine(FireBullets_CR());
        }
        else if(Vector3.Distance(transform.position, target.transform.position) > 25)
        {
            StopCoroutine(FireBullets_CR());
            playerDetected = false;
        }
    }

    void OnTriggerEnter(Collider other)
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
        for(int i=0; i<maxCounter && playerDetected; i++)
        {
            counter++;
            Instantiate(bullet, transform.position, transform.rotation);
            yield return new WaitForSeconds(timer);
        }
    }
}
