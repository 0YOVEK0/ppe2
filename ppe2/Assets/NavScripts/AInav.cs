using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//https://www.youtube.com/watch?v=HOAPvQONpsU video  que usamos de referencia

public class AInav : MonoBehaviour
{
    
    public NavMeshAgent agent;

    
    private Vector3 initialPosition;

    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        


        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }

       
        initialPosition = transform.position;
    }

    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            
            if (Physics.Raycast(ray, out hit))
            {
                
                agent.SetDestination(hit.point);
            }
        }
    }

    // Trigger when another collider enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Hide the GameObject by disabling its renderer and collider
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Optionally, stop the NavMeshAgent
        agent.isStopped = true;

        // Start the coroutine to respawn the GameObject
        StartCoroutine(Respawn());
    }

    // Coroutine to respawn the GameObject after a delay
    IEnumerator Respawn()
    {
        // Wait for 1 second before respawning
        yield return new WaitForSeconds(1f);

        // Reset the position of the GameObject to its initial position
        transform.position = initialPosition;

        // Re-enable the renderer and collider
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;

        // Resume the NavMeshAgent
        agent.isStopped = false;
    }
}

