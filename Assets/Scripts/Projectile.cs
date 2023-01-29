using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("References")]
    public Transform trans;

    [Header("Stats")]
    [Tooltip("The speed of the projectile in units per second")]
    public float speed = 34;

    [Tooltip("The distance the projectile will travel before being destroyed")]
    public float range = 70;

    private Vector3 spawnPoint;



    // Start is called before the first frame update
    void Start()
    {
        spawnPoint =trans.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Move the projectile along its local Z axis (forward)
        trans.Translate(0,0,speed * Time.deltaTime, Space.Self);

        //If the projectile has traveled further than its range, destroy it
        if(Vector3.Distance(trans.position, spawnPoint) >= range)
        {
            Destroy(gameObject);
        }
    }
}
