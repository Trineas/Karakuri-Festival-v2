using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int currentPatrolPoint;

    public NavMeshAgent agent;

    public Animator anim;

    public enum AIState
    {
        isIdle, isPatroling, isFacing,
    };
    public AIState currentState;

    private float waitAtPoint;
    private float waitCounter;

    public float faceRange;

    void Start()
    {
        waitAtPoint = Random.Range(2.0f, 6.0f);
        waitCounter = waitAtPoint;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        switch (currentState)
        {
            case AIState.isIdle:

                anim.SetBool("IsMoving", false);

                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }

                else
                {
                    currentState = AIState.isPatroling;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }

                if (distanceToPlayer <= faceRange)
                {
                    currentState = AIState.isFacing;
                }

                break;

            case AIState.isPatroling:

                if (agent.remainingDistance <= 0.2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }

                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;
                }

                if (distanceToPlayer <= faceRange)
                {
                    currentState = AIState.isFacing;
                    waitCounter = waitAtPoint;
                }

                anim.SetBool("IsMoving", true);

                break;

            case AIState.isFacing:

                anim.SetBool("IsMoving", false);

                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                agent.isStopped = true;

                if (distanceToPlayer >= faceRange)
                {
                    if (waitCounter > 0)
                    {
                        waitCounter -= Time.deltaTime;
                    }

                    else
                    {
                        agent.isStopped = false;
                        currentState = AIState.isPatroling;
                    }
                }

                break;
        }
    }
}
