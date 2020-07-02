using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerRanged : MonoBehaviour
{
    //public NavMeshAgent agent;

    public Animator anim;

    public enum AIState
    {
        isIdle, isAttacking,
    };
    public AIState currentState;

    public float attackRange;

    public float timeBetweenAttacks;
    private float attackCounter;

    public GameObject bullet;
    public Transform bulletSlot;
    public float shootSpeed;

    public Quaternion originalRotation;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        switch (currentState)
        {
            case AIState.isIdle:

                transform.rotation = originalRotation;

                if (distanceToPlayer < attackRange)
                {
                    currentState = AIState.isAttacking;
                }

                break;

            case AIState.isAttacking:

                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                attackCounter -= Time.deltaTime;
                if (attackCounter <= 0)
                {
                    if (distanceToPlayer < attackRange)
                    {
                        anim.SetTrigger("Attack");

                        GameObject bulletInstance = Instantiate(bullet, bulletSlot.position, bullet.transform.rotation);
                        bulletInstance.transform.rotation = Quaternion.LookRotation(-bulletSlot.forward);
                        Rigidbody bulletRig = bulletInstance.GetComponent<Rigidbody>();

                        bulletRig.AddForce(bulletSlot.forward * shootSpeed, ForceMode.Impulse);

                        attackCounter = timeBetweenAttacks;
                    }

                    else
                    {
                        currentState = AIState.isIdle;
                    }
                }

                break;

        }
    }
}
