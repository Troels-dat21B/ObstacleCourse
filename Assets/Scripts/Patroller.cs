using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{

    private Transform[] patrolPoints;

    void Start()
    {
        patrolPoints = GetComponentsInChildren<Transform>();
    }

}
