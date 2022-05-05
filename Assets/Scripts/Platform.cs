using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Rigidbody rb;
    public float fallDelay = 0.3f;
    public float timeToDestroy = 5f;
     
    void Start ()
    {
        rb = GetComponent<Rigidbody> ();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log ("tocando");
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

