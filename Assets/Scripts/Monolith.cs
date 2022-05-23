using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Monolith : MonoBehaviour
{
    public Animator ani;
    public GameObject particlesCircle;
    public GameObject particlesSplash;
    public GameObject UI;
    [SerializeField] private PlayerInput playerInput;

    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetBool("activated", false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log ("activado");
                PlayerController.lastCheckpointPos = transform.position;
                ani.SetBool("activated", true);
                particlesCircle.SetActive(true);
                particlesSplash.SetActive(true);
                Destroy(UI);
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UI.SetActive(false);
    }
}
