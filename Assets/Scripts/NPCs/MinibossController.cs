using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinibossController : MonoBehaviour
{
    public Transform returnPoint;
    public Transform teleportPointOne, teleportPointTwo, teleportPointThree;

    public Animator anim;

    public enum AIState
    {
        isIdle, isAttacking, isTeleporting, isWaiting,
    };
    public AIState currentState;

    public float attackRange;

    public float timeBetweenAttacks;
    private float attackCounter;

    private int attacks;
    public int maxAttacks;

    public float waitingLength;
    private float waitCounter;

    public float teleportDelay;
    private float teleportCounter;

    public GameObject bullet;
    public Transform bulletSlot;
    public float shootSpeed;

    public GameObject teleportEffect;
    public int teleportSound, attackSound;

    public Quaternion originalRotation;
    public Transform originalPosition;

    public GameObject oldDoor, newDoor;

    void Start()
    {
        waitCounter = waitingLength;
        teleportCounter = teleportDelay;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        if (distanceToPlayer <= 10f)
        {
            oldDoor.SetActive(false);
            newDoor.SetActive(true);
        }

        EnemyHealthManager healthMan = GetComponent<EnemyHealthManager>();

        if (GameManager.instance.isRespawning)
        {
            currentState = AIState.isIdle;

            oldDoor.SetActive(true);
            newDoor.SetActive(false);
            transform.rotation = originalRotation;
            transform.position = originalPosition.position;

            healthMan.currentHealth = healthMan.maxHealth;

            GameManager.instance.isRespawning = false;
        }

        switch (currentState)
        {
            case AIState.isIdle:

                if (distanceToPlayer < attackRange && PlayerController.instance.transform.position.y == 5.52f)
                {
                    currentState = AIState.isAttacking;
                }

                if (healthMan.isTakingDamage)
                {
                    currentState = AIState.isTeleporting;

                    Instantiate(teleportEffect, transform.position, transform.rotation);
                    AudioManager.instance.PlaySFX(teleportSound);
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

                        AudioManager.instance.PlaySFX(attackSound);
                        GameObject bulletInstance = Instantiate(bullet, bulletSlot.position, bullet.transform.rotation);
                        bulletInstance.transform.rotation = Quaternion.LookRotation(-bulletSlot.forward);
                        Rigidbody bulletRig = bulletInstance.GetComponent<Rigidbody>();

                        bulletRig.AddForce(bulletSlot.forward * shootSpeed, ForceMode.Impulse);

                        attackCounter = timeBetweenAttacks;
                        attacks++;

                        if (attacks >= maxAttacks)
                        {
                            currentState = AIState.isWaiting;
                            attacks = 0;
                        }
                    }

                    else
                    {
                        currentState = AIState.isIdle;
                        attacks = 0;
                    }

                    if (healthMan.isTakingDamage)
                    {
                        currentState = AIState.isTeleporting;

                        Instantiate(teleportEffect, transform.position, transform.rotation);
                        AudioManager.instance.PlaySFX(teleportSound);
                    }
                }

                break;

            case AIState.isTeleporting:

                if (transform.position == originalPosition.position)
                {
                    transform.position = teleportPointOne.position;

                    currentState = AIState.isAttacking;
                    attackCounter = timeBetweenAttacks;
                }

                else if (transform.position == teleportPointOne.position)
                {
                    transform.position = teleportPointTwo.position;

                    currentState = AIState.isAttacking;
                    attackCounter = timeBetweenAttacks;
                }

                else if (transform.position == teleportPointTwo.position)
                {
                    transform.position = teleportPointThree.position;

                    currentState = AIState.isAttacking;
                    attackCounter = timeBetweenAttacks;
                }

                else if (transform.position == teleportPointThree.position)
                {
                    transform.position = originalPosition.position;

                    currentState = AIState.isAttacking;
                    attackCounter = timeBetweenAttacks;
                }

                break;

            case AIState.isWaiting:

                if (healthMan.isTakingDamage)
                {
                    currentState = AIState.isTeleporting;

                    Instantiate(teleportEffect, transform.position, transform.rotation);
                    AudioManager.instance.PlaySFX(teleportSound);
                }

                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }

                else
                {
                    currentState = AIState.isAttacking;
                    attackCounter = timeBetweenAttacks;
                    waitCounter = waitingLength;
                }

                break;
        }
    }
}
