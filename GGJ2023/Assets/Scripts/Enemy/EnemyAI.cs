using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Enemy Fields
    public LayerMask GroundLayer, PlayerLayer;
    public NavMeshAgent Agent;
    public Transform Player;

    // Patroling
    public bool CanChase = true;

    // Attacking
    public float AttackCoolDownTime;
    public bool AlreadyAttacked;

    // States
    public float AttackRange;
    private bool PlayerInAttackRange;
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        RunEnemy();
    }
    public void RunEnemy()
    {
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerLayer);

        if (!PlayerInAttackRange) ChasePlayer();
        else AttackPlayer();


        //Check for sight and attack range
        //PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, PlayerLayer);

        /*if (PlayerInSightRange && !PlayerInAttackRange) ChasePlayer();
        if (PlayerInAttackRange && PlayerInSightRange) AttackPlayer(false);*/
    }
    public void ChasePlayer()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        if (CanChase)
        {
            Agent.SetDestination(Player.position);
        }
    }
    public delegate void Callback();
    public IEnumerator ResetAttack(float attackCoolDown, Callback boolToReset)
    {
        //Debug.Log("Reseting");
        yield return new WaitForSeconds(attackCoolDown);
        boolToReset();
        //Debug.Log("Done");
    }
    public void AttackPlayer()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        //Make sure enemy doesn't move
        Agent.SetDestination(transform.position);

        //transform.LookAt(Player);
        var lookPos = Player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Agent.angularSpeed);

        if (!AlreadyAttacked)
        {
            //Attack code here
            //attack
            //End of attack code

            AlreadyAttacked = true;
            StartCoroutine(ResetAttack(AttackCoolDownTime, delegate () { AlreadyAttacked = false; }));
        }

    }
}
