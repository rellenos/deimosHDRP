using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public GameObject img;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                img.SetActive(true);
                gameObject.SetActive(false);
            } 
        }
    }

    public void Button()
    {
        Debug.Log ("cerrar");
        img.SetActive(false);
    }
}
