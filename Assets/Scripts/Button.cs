using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("Red")]
    public Animator red1;
    public BoxCollider colRed1;
    public Animator red2;
    public BoxCollider colRed2;

    [Header("Blue")]
    public Animator blue1;
    public BoxCollider colBlue1;
    public Animator blue2;
    public BoxCollider colBlue2;

    [Header("Green")]
    public Animator green1;
    public BoxCollider colGreen1;
    public Animator green2;
    public BoxCollider colGreen2;

    void Start()
    {
        colRed1.GetComponent<BoxCollider>().enabled = true;
        colRed2.GetComponent<BoxCollider>().enabled = false;
        
        red1.SetBool ("Open", false);
        red2.SetBool ("Open", true);

        colBlue1.GetComponent<BoxCollider>().enabled = true;
        colBlue2.GetComponent<BoxCollider>().enabled = false;

        blue1.SetBool ("Open", false);
        blue2.SetBool ("Open", true);

        colGreen1.GetComponent<BoxCollider>().enabled = false;
        colGreen2.GetComponent<BoxCollider>().enabled = true;

        green1.SetBool ("Open", true);
        green2.SetBool ("Open", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RedBox"))
        {
            colRed1.GetComponent<BoxCollider>().enabled = true;
            colRed2.GetComponent<BoxCollider>().enabled = false;

            red1.SetBool ("Open", false);
            red2.SetBool ("Open", true);
        }

        if (other.gameObject.CompareTag("BlueBox"))
        {
            colBlue1.GetComponent<BoxCollider>().enabled = true;
            colBlue2.GetComponent<BoxCollider>().enabled = false;

            blue1.SetBool ("Open", false);
            blue2.SetBool ("Open", true);
        }

        if (other.gameObject.CompareTag("GreenBox"))
        {
            colGreen1.GetComponent<BoxCollider>().enabled = true;
            colGreen2.GetComponent<BoxCollider>().enabled = false;

            green1.SetBool ("Open", false);
            green2.SetBool ("Open", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RedBox"))
        {
            colRed1.GetComponent<BoxCollider>().enabled = false;
            colRed2.GetComponent<BoxCollider>().enabled = true;

            red1.SetBool ("Open", true);
            red2.SetBool ("Open", false);
        }

        if (other.gameObject.CompareTag("BlueBox"))
        {
            colBlue1.GetComponent<BoxCollider>().enabled = false;
            colBlue2.GetComponent<BoxCollider>().enabled = true;

            blue1.SetBool ("Open", true);
            blue2.SetBool ("Open", false);
        }

        if (other.gameObject.CompareTag("GreenBox"))
        {
            colGreen1.GetComponent<BoxCollider>().enabled = false;
            colGreen2.GetComponent<BoxCollider>().enabled = true;
            
            green1.SetBool ("Open", true);
            green2.SetBool ("Open", false);
        }
    }
}
