using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {

    private Vector3 goal;

    public NavMeshAgent myAgent;

    public Camera myCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {

            ShootRay();
        }
	}

   

    private void ShootRay()
    {

        RaycastHit hit;
        Vector3 mouse = myCamera.ScreenToWorldPoint(Input.mousePosition);

       // Debug.Log(mouse);
        Ray ray =  myCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            goal = hit.point;
            StartPath();
           /* if (hit.transform.gameObject.tag == "floor")
            {
                
               
            }*/
        }
        
    }

    private void StartPath()
    {
        myAgent.SetDestination(goal);
    }


    
}
