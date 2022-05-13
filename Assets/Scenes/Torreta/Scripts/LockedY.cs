using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedY : MonoBehaviour
{
    [SerializeField]
    public Transform _target;
    public GameObject target;

    Transform t;
    public float fixedRotation = 5;

    void Start()
    {
        target = GameObject.Find("Player");
        _target = GameObject.Find("Player").transform;
        t = transform;
    }
    
    void FixedUpdate()
    {
        t.eulerAngles = new Vector3 (fixedRotation, t.eulerAngles.y, t.eulerAngles.z);
        
        if (Vector3.Distance(transform.position, target.transform.position) < 30)
        {
            Vector3 targetOrientation = _target.position - transform.position;
            Quaternion targetOrientationQuaternion = Quaternion.LookRotation(targetOrientation);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetOrientationQuaternion, Time.deltaTime * 8);
            Debug.DrawRay(transform.position, targetOrientation, Color.green);
        }
    }
}