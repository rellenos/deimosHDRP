using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Monolith : MonoBehaviour
{
    public Animator ani;
    public GameObject particlesDeco;
    public GameObject UI;
    [SerializeField]
    private PlayerInput playerInput;
    private InputAction eAction;

    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetBool("activated", false);
        eAction = playerInput.actions["Grab"];
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log ("activado");
                ani.SetBool("activated", true);
                particlesDeco.SetActive(true);
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UI.SetActive(false);
    }
}
