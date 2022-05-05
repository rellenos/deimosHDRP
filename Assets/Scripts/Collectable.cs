using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public GameObject img;
    public GameObject text;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
            img.SetActive(true);
            text.SetActive(false);
            gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Cerrar");
            img.SetActive(false);
        } 
    } 
}
