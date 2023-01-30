using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
  //Consts
    private const float ratotionSlerpAmount = .68f;

    [Header("References")]
    public Transform trans;
    public Transform modelHolder;

    [Header("Patrol Settings")]
    public float patrolMoveSpeed = 30;

    //private variables
    private int currentPatrolPointIndex;
    private Transform currentPatrolPoint;
    private Transform[] patrolPoints;

  

    void Start()
    {
        //Get unsorted list of patrol points
        List<Transform> unsortedPatrolPoints = GetUnsortedPatrolPoints();

        //Only continue if there are patrol points
        if (unsortedPatrolPoints.Count > 0)
        {
            //Prepare the array of patrol points
            patrolPoints = new Transform[unsortedPatrolPoints.Count];

            for (int i = 0; i < unsortedPatrolPoints.Count; i++)
            {
                //Quick reference to current patrol point
                Transform currentPatrolPoint = unsortedPatrolPoints[i];

                //isolate just the patrol point number within the name
                int closingParentesisIndex = currentPatrolPoint.gameObject.name.IndexOf(")");
                string patrolPointNumberString = currentPatrolPoint.gameObject.name.Substring(14, closingParentesisIndex - 14);

                //Convert the patrol point number string to an int
                int index = int.Parse(patrolPointNumberString);

                //Set a reference in the script patrolPoints array
                patrolPoints[index] = currentPatrolPoint;

                //unparent the patrol point so it doesnt move with the patroller
                currentPatrolPoint.SetParent(null);

                //Hide patrol point in the hierarchy
                currentPatrolPoint.gameObject.hideFlags = HideFlags.HideInHierarchy;
                                
            }
            //Start patrolling at the first patrol point
            SetCurrentPatrolPoint(0);
        }
    }
    
    void Update()
    {
        //Only operate GameObject if there are patrol points
        if(currentPatrolPoint != null){
            
            //Move root GameObject towards the current patrol point
            trans.position = Vector3.MoveTowards(trans.position, currentPatrolPoint.position, patrolMoveSpeed * Time.deltaTime);


            //If the patroller is already on top of the point already, change the current patrol point
            if(trans.position == currentPatrolPoint.position){

                //If the patroller is at the last patrol point
                if(currentPatrolPointIndex >= patrolPoints.Length - 1){
                    //Start at the first patrol point
                    SetCurrentPatrolPoint(0);
                }else{

             //Go to the next patrol point
             SetCurrentPatrolPoint(currentPatrolPointIndex + 1);
                //Go to index after the current
            }


        }else{
           //Else if the patroller is not on the point yet, rotate towards the point
            Quaternion lookRotation = Quaternion.LookRotation((currentPatrolPoint.position - trans.position).normalized);
            modelHolder.rotation = Quaternion.Slerp(modelHolder.rotation, lookRotation, ratotionSlerpAmount);
        }
        
    }
}
    private void SetCurrentPatrolPoint(int index)
    {
        //Set the current patrol point index
        currentPatrolPointIndex = index;

        //Set the current patrol point
        currentPatrolPoint = patrolPoints[currentPatrolPointIndex];
    }

    //Returns a List conataining all the transform of each child
    //With a name that starts with "Patrol Point ("
    private List<Transform> GetUnsortedPatrolPoints()
    {
        //Get the Transfomr of each Child in the Patroller
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();

        //Declare a local List stroing Transforms
        List<Transform> patrolPoints = new List<Transform>();
        for (int i = 0; i < children.Length; i++)
        {
            //If the name of the child starts with "Patrol Point ("
            if (children[i].name.StartsWith("Patrol Point ("))
            {
                //Add the child to the list
                patrolPoints.Add(children[i]);
            }
        }
        return patrolPoints;
    }
}
