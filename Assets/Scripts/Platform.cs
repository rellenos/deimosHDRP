using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    
    public Rigidbody rb;

    private float fallDelay = 2.0f;
    private float shakeAmount = 2f;
    private float timeToDestroy = 5f;

    bool readyToShake = false;

    Vector3 originalPos;
     
    void Start ()
    {
        rb = GetComponent<Rigidbody> ();
    }

    void Update()
    {
        if(readyToShake)
        {
            Vector3 newPos = originalPos + Random.insideUnitSphere * (Time.deltaTime * shakeAmount);
            newPos.x = transform.position.x;
            newPos.y = transform.position.y;
            newPos.z = transform.position.z;

            transform.position = newPos;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //readyToShake = true;
            StartCoroutine(FallAfterDelay());
            Destroy(gameObject, timeToDestroy);
        }
    }
 
    IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(fallDelay);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}

