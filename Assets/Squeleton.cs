using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Squeleton : MonoBehaviour
{

    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Moove(Vector3 position)
    {
        GetComponent<NavMeshAgent>().SetDestination(position);
    }
}
