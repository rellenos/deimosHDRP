using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{

    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _timer = 2f;
    private float timerCount = 20;

    [SerializeField]
    private int _counter;
    private int _maxCounter = 20;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireBullets_CR());   
    }

    // Update is called once per frame
    void Update()
    {/*
        timerCount += Time.deltaTime;

        if(timeCount>timer)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            timerCount = 0f;
        }
     */
    }

    IEnumerator FireBullets_CR()
    {
        Debug.Log("Inicio Coroutine");
        for(int i=0; i<_maxCounter; i++)
        {
            _counter++;
            Instantiate(_bullet, transform.position, transform.rotation);
            yield return new WaitForSeconds(_timer);
        }
        Debug.Log("Fin coroutine");
    }
}
