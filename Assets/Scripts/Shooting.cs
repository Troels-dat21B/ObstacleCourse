using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [Header("References")]
    public Transform spawnPoint;
    public GameObject projectilePrefab;

    [Header("Stats")]
    [Tooltip("Time between shots in seconds")]
    public float fireRate = 1f;

    private float lastFireTime = 0f;



    // Update is called once per frame
    void Update()
    {
        if(Time.time >= lastFireTime + fireRate)
        {
            lastFireTime = Time.time;
            Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
