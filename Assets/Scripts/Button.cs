using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator Door1;
    public Animator Door2;

    void Start()
    {
        Door1.SetBool ("Open", false);
        Door2.SetBool ("Open", true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RedBox"))
        {
            Door1.SetBool ("Open", true);
            Door2.SetBool ("Open", false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RedBox"))
        {
            Door1.SetBool ("Open", false);
            Door2.SetBool ("Open", true);
        }
    }
}
