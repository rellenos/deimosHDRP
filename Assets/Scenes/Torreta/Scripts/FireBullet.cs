using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] public GameObject target;

    [SerializeField] private float timer = 0.5f;

    [SerializeField] private int counter;
    [SerializeField] private int maxCounter = 200;

    Transform Player;
    public Transform head;

    void Start()
    {   
        target = GameObject.Find("Player");
        if (Vector3.Distance(transform.position, target.transform.position) < 30)
        {
            head.LookAt(Player);
            StartCoroutine(FireBullets_CR());
            return;
        }
    }

    void Update()
    {
        
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
