using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    public GameObject target;

    void Start()
    {
        target = GameObject.Find("Player");
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 30)
        {
            Vector3 targetOrientation = _target.position - transform.position;
            Debug.DrawRay(transform.position, targetOrientation, Color.green);
            Quaternion targetOrientationQuaternion = Quaternion.LookRotation(targetOrientation);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetOrientationQuaternion, Time.deltaTime);
        }
    }
}