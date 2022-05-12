using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public bool activated = false;
    public static GameObject[] CheckPointsList;
    
    void Start()
    {
        CheckPointsList = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    private void ActivateCheckPoint()
    {
        foreach (GameObject cp in CheckPointsList)
        {
            cp.GetComponent().activated = false;
            cp.GetComponent().SetBool("Active", false);
        }

        activated = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ActivateCheckPoint();
        }
    }

    public static Vector3 GetActiveCheckPointPosition()
    {
        Vector3 result = new Vector3(0, 0, 0);

        if (CheckPointsList != null)
        {
            foreach (GameObject cp in CheckPointsList)
            {
                if (cp.GetComponent().activated)
                {
                    result = cp.transform.position;
                    break;
                }
            }
        }

        return result;
    }
}
