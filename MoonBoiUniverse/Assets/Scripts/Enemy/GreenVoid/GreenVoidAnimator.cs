using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GreenVoidAnimator : MonoBehaviour
{
    public GreenVoidEnemyManager _manager;
    public Animator _pigAnimator;
    public Transform leftAttackSourcePoint, rightAttackSourcePoint;
    private Transform currentAttackSourcePoint;
    public AudioClip attackSound;
    public GameObject solanaCoin;
    public GameObject powerUpCan;
    public GameObject projectile;
   

    // Update is called once per frame
    void Update()
    {
       //flips the void(mirrors it), according to the direction of movement. It also switches the object used as the source point of the projectiles.
        if (_manager.transform.position.x < _manager._movement.player.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            currentAttackSourcePoint = leftAttackSourcePoint;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            currentAttackSourcePoint = rightAttackSourcePoint;
        }
    }

    //throws projectiles, according to the direction the void is facing
    public void projectileThrow()
    {
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            GameObject projectileObject = Instantiate(projectile, leftAttackSourcePoint.position, Quaternion.identity);
           
            projectile.transform.localScale = new Vector3(1, 1, 1);
            Destroy(projectileObject, 2);
        }
        else
        {
            GameObject projectileObject = Instantiate(projectile, rightAttackSourcePoint.position, Quaternion.identity);
          
            projectile.transform.localScale = new Vector3(-1, 1, 1);
            Destroy(projectileObject, 2);
        }
        
    }
    public void attackSoundEffect()
    {
        //plays attack sound
        GetComponent<AudioSource>().PlayOneShot(attackSound);
    }

    //stops void from moving while attacking
    public void attackStart()
    {
        //start of the attack
        _manager.GetComponent<NavMeshAgent>().enabled = false;
    }
    public void attackEnd()
    {
        //end of the attack
        _manager.GetComponent<NavMeshAgent>().enabled = true;

    }
    
    //triggered through an event in death animation
    public void coinSpawn()
    {
        //spawns coin upon death
        GameObject coin = Instantiate(solanaCoin, transform.parent.position, Quaternion.identity);
        coin.transform.GetComponent<Rigidbody>().AddForce((Vector3.up + new Vector3(Random.Range(1, -1), 0, 0)) * 3 + new Vector3(Random.Range(1, -1), 0, 0), ForceMode.Impulse);

        //Spawns the power up can, after round 3 (one in 20 chance)
        int powerUpChance = Random.Range(0, 20);
        if(PlayerPrefs.GetInt("Round") >= 3 && powerUpChance == 5)
        {
            GameObject can = Instantiate(powerUpCan, transform.parent.position, Quaternion.identity);
            can.transform.GetComponent<Rigidbody>().AddForce((Vector3.up + new Vector3(Random.Range(1, -1), 0, 0)) * 3 + new Vector3(Random.Range(1, -1), 0, 0), ForceMode.Impulse);
        }
    }

    //triggered through an event in death animation
    public void Death()
    {
       
        //destroys game object
        Destroy(transform.parent.gameObject);
    }
}
