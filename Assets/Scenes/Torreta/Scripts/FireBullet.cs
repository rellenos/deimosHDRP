using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float timer = 2f;
    private float timerCount = 20;

    private int counter;
    [SerializeField]
    private int maxCounter = 20;

    public GameObject target;

    void Start()
    {
        //StartCoroutine(FireBullets_CR());   
        target = GameObject.Find("Player");
    }

    void Update()
    {/*
        timerCount += Time.deltaTime;

        if(timeCount>timer)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            timerCount = 0f;
        }
     */

        if (Vector3.Distance(transform.position, target.transform.position) < 30)
        {
            //StartCoroutine(FireBullets_CR());
            Instantiate(bullet, transform.position, transform.rotation);
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
