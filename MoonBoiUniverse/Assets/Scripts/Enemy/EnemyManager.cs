using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyManager : MonoBehaviour
{
    public float health = 100;
    private float maxHealth;
    public EnemyAnimator _animator;
    public EnemyMovement _movement;
    public Transform healthIndicator;
   
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        //changes health bar according to current health
        healthIndicator.localScale = new Vector3(health/maxHealth * 100, healthIndicator.localScale.y, healthIndicator.localScale.z);

        //triggers death, if health gets to zero
        if (health <= 0)
        {
           
            healthIndicator.gameObject.SetActive(false);
            _movement.agent.enabled = false;
            _animator._pigAnimator.SetTrigger("Death");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (health > 0)
        {
            //taking standard sword damage
            if (other.tag == "AttackZone")
            {
                if (other.transform.parent.parent.GetComponent<PlayerManager>()._combat.comboTimer <= 1)
                {
                    other.transform.parent.parent.GetComponent<PlayerManager>()._combat.comboCounter += 1;
                }

                health -= 30 + PlayerPrefs.GetInt("SwordPower");
                StartCoroutine(attacked());
                


            }

            //taking special damage
            if (other.tag == "specialAttackZone")
            {
                health -= 60 + PlayerPrefs.GetInt("SpecialPower");
                StartCoroutine(attacked());
            }
        }
    }

    //camera shake and change color momentarily
    IEnumerator attacked()
    {
        Camera.main.transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.x, 0, Camera.main.transform.rotation.z + 0.2f);
        _animator.transform.GetComponent<SpriteRenderer>().color = new Color(1, 0.7470201f, 0.7138364f);
        yield return new WaitForSeconds(.1f);
        _animator.transform.GetComponent<SpriteRenderer>().color = new Color(0.7169812f, 0.7169812f, 0.7169812f);
        Camera.main.transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.x , 0, Camera.main.transform.rotation.z - 0.2f);
    }
}
