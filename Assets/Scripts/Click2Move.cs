using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Click2Move : MonoBehaviour
{
    private NavMeshAgent NavAgent;

    private void Start()
    {
       NavAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Create ray from camera to the position of the mouse
            Ray R = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit RChit;

            //Check if ray points to the ground via NavMesh
            if (Physics.Raycast(R, out RChit, Mathf.Infinity, NavMesh.AllAreas)) 
            {
                //Move the agent to the position that is clicked
                NavAgent.SetDestination(RChit.point);
            }
        }
    }
}
