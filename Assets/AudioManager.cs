using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource redentor;
    public AudioSource turret;
    public AudioSource bullet;
    public AudioSource monolith;
    public AudioSource grap;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Global.isTurretDead == true){
            turret.Play();
        }
    }
}
