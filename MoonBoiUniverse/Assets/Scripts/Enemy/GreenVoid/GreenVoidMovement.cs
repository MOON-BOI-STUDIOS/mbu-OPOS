using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GreenVoidMovement : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    private GreenVoidEnemyManager _manager;
    public float maxSearchDistance = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _manager = GetComponent<GreenVoidEnemyManager>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player comes in the maxSearchDistance, starts moving towards them
        if (Vector3.Distance(transform.position, player.position) >= 1.5f  && Vector3.Distance(transform.position, player.position) <= maxSearchDistance)
        {
            agent.destination = player.position;
        }
        else
        //stops moving
        {
            agent.destination = transform.position;
        }
        
        //if close enough, triggers the attack
        if(Vector3.Distance(transform.position, player.position) <= 2 && Vector3.Distance(transform.position, player.position) >= 1 )
        {
            _manager._animator._pigAnimator.SetTrigger("Attack");
        }
        
    }
}
