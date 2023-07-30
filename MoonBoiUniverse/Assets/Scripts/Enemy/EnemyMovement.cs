using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    private EnemyManager _manager;
    public float maxSearchDistance = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _manager = GetComponent<EnemyManager>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //while(Vector3.Distance(transform.position, player.position) <= 1f)
        {
            //gent.destination = -player.position;
        }
        if(Vector3.Distance(transform.position, player.position) >= 1.5f  && Vector3.Distance(transform.position, player.position) <= maxSearchDistance)
        {
            agent.destination = player.position;
        }
        else
        {
            agent.destination = transform.position;
        }
        
        if(Vector3.Distance(transform.position, player.position) <= 2 && Vector3.Distance(transform.position, player.position) >= 1 )
        {
            _manager._animator._pigAnimator.SetTrigger("Attack");
        }
        
    }
}
