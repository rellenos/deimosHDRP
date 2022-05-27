using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public GameObject particlesCircle, particlesSplash;
    public AudioSource monolith;
    public Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetBool("activated", false);
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Player")
        {
            //PlayerController.lastCheckpointPos = transform.position;
            ani.SetBool("activated", true);
            monolith.Play();
            particlesCircle.SetActive(true);
            particlesSplash.SetActive(true);
        }
    }
}
