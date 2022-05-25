using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private GameObject EnemyBullet;
    [SerializeField] public GameObject target;

    [SerializeField] private float timer = 0.33f;

    [SerializeField] private int counter;
    [SerializeField] private int maxCounter = 200;

    public PoolManager poolManager;
    [SerializeField] Transform spawnPoint;

    Transform Player;

    public bool playerDetected = false;
     public int bulletType;

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
        for(int i=0; i<maxCounter && playerDetected; i++)
        {
            counter++;
            GameObject EnemyBullet = PoolManager.instance.GetPooledObject(bulletType);
            EnemyBullet.transform.position = spawnPoint.position;
            EnemyBullet.SetActive(true);
            yield return new WaitForSeconds(timer);
        }
    }
}
