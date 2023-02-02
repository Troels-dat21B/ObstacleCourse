using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("References")]
    public Transform spawnPoint;
    public GameObject projectilePrefab;

    [Header("Stats")]
    [Tooltip("Time between shots in seconds(Set to 0 for random fire rate)")]
    public float fireRate = 1f;

    [Tooltip("Random fire (True/False)")]
    public bool randomFire = false;

    private float lastFireTime = 0f;

    // Update is called once per frame
    void Update(){   
        fire();
    }

    //Controls the firing of the projectile
    void fire(){
        
        if(!randomFire){
            //Fire at a set rate
            if(Time.time > lastFireTime + fireRate){
                lastFireTime = Time.time;
                Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }else{
            //Fire at a random rate
            if(Time.time > lastFireTime + fireRate){
                lastFireTime = Time.time;
                fireRate = Random.Range(0.2f, 1.3f);
                Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
