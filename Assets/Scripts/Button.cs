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

    /* public GameObject redDoor1;
    public GameObject redDoor2;
    public GameObject blueDoor;
    public GameObject yellowDoor; */

    void Start()
    {
        red1.SetBool ("Open", false);
        red2.SetBool ("Open", true);

        blue1.SetBool ("Open", false);
        blue2.SetBool ("Open", true);

        green1.SetBool ("Open", false);
        green2.SetBool ("Open", true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RedBox"))
        {
            red1.SetBool ("Open", true);
            red2.SetBool ("Open", false);

            //redDoor1.transform.position = new Vector3(0,2,0) * Time.deltaTime;
            //redDoor2.transform.position = new Vector3(0,-2,0) * Time.deltaTime;
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

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RedBox"))
        {
            red1.SetBool ("Open", false);
            red2.SetBool ("Open", true);

            //redDoor1.transform.position = new Vector3(0,-2,0) * Time.deltaTime;
            //redDoor2.transform.position = new Vector3(0,2,0) * Time.deltaTime;
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
}
