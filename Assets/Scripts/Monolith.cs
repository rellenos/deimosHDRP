using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monolith : MonoBehaviour
{
    bool activated;
    public Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetBool("activated", false);
        activated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log ("activado");
            ani.SetBool("activated", true);
        }
    }
}
