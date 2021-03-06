using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public GameObject target;
    public float damageAmount = 5;
    //public float BigdamageAmount = 5;
    Animator anim;
    NavMeshAgent agent;
    public bool iShooting=false;
    

    enum STATE { IDLE,WANDER,ATTACK,CHASE,DEAD};
    STATE state = STATE.IDLE;
   

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        
    }
    void TurnOffTriggers()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isDead", false);
    }

    float DistanceToPlayer()
    {
        return Vector3.Distance(target.transform.position, this.transform.position);
    }
   bool CanSeePlayer()
    {
        if (DistanceToPlayer() < 10)
        {
            return true;
        }
        else
            return false;
    }
    bool ForgetPlayer() { if (DistanceToPlayer() > 15) return true; else { return false; } }

    public void DamagePlayer()
    {
        target.GetComponent<PlayerBehaviour>().TakeDamage(10);
    }
    //public void BigDamagePlayer()
    //{
    //    target.GetComponent<PlayerBehaviour>().TakeDamage(20);
    //}
    // Update is called once per frame
    void Update()
    {
        //if (target = null)
        //{
        //    target = GameObject.FindWithTag("Player");
        //    return;
        //}
        switch (state)
        {
            case STATE.IDLE:
                TurnOffTriggers();
                if (CanSeePlayer()) { state = STATE.CHASE; }
                else { 
                    state = STATE.WANDER;
                }
                break;
            case STATE.WANDER:
                if (!agent.hasPath) { 
                    float newX = this.transform.position.x + Random.Range(-5, 5);
                    float newZ = this.transform.position.z + Random.Range(-5, 5);
                    //float newY = Terrain.activeTerrain.SampleHeight(new Vector3(newX, 0, newZ));
                    Vector3 dest = new Vector3(newX, 0, newZ);
                    agent.SetDestination(dest);
                    agent.stoppingDistance = 5;
                    TurnOffTriggers();
                    anim.SetBool("isWalking", true);
                }
                if (CanSeePlayer()) { state = STATE.CHASE; }
                if (iShooting == true)
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isDead", true);
                    agent.SetDestination(this.transform.position);
                }
                break;
            case STATE.CHASE:
                if (iShooting == false)
                {
                    agent.SetDestination(target.transform.position);
                    agent.stoppingDistance = 2;
                    TurnOffTriggers();
                    anim.SetBool("isRunning", true);
                    if (agent.remainingDistance <= agent.stoppingDistance) { state = STATE.ATTACK; }

                    if (ForgetPlayer())
                    {
                        state = STATE.WANDER;
                        agent.ResetPath();
                    }
                }
                else 
                { 
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isDead", true);
                    agent.SetDestination(this.transform.position);
                }
                break;
            case STATE.ATTACK:
                if (iShooting == false) { 
                    TurnOffTriggers();
                anim.SetBool("isAttacking", true);
                //this.transform.LookAt(target.transform.position);
                if (DistanceToPlayer() > agent.stoppingDistance+3) { state = STATE.CHASE; }
                }
                else
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isDead", true);
                    agent.SetDestination(this.transform.position);
                }
                break;
            case STATE.DEAD:
                
                break;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            iShooting = true;
        }
    }
    
}
