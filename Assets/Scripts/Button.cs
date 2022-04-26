using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator red1;
    public Animator red2;

    public Animator blue1;
    public Animator blue2;

    public Animator green1;
    public Animator green2;

    void Start()
    {
        red1.SetBool ("Open", false);
        red2.SetBool ("Open", true);

        blue1.SetBool ("Open", false);
        blue2.SetBool ("Open", true);

        green1.SetBool ("Open", true);
        green2.SetBool ("Open", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RedBox"))
        {
            red1.SetBool ("Open", false);
            red2.SetBool ("Open", true);
        }

        if (other.gameObject.CompareTag("BlueBox"))
        {
            blue1.SetBool ("Open", false);
            blue2.SetBool ("Open", true);
        }

        if (other.gameObject.CompareTag("GreenBox"))
        {
            green1.SetBool ("Open", false);
            green2.SetBool ("Open", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RedBox"))
        {
            red1.SetBool ("Open", true);
            red2.SetBool ("Open", false);
        }

        if (other.gameObject.CompareTag("BlueBox"))
        {
            blue1.SetBool ("Open", true);
            blue2.SetBool ("Open", false);
        }

        if (other.gameObject.CompareTag("GreenBox"))
        {
            green1.SetBool ("Open", true);
            green2.SetBool ("Open", false);
        }
    }
}
