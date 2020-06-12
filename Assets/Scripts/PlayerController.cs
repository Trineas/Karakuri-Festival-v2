using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float jumpForce;
    public float lowJumpForce;
    public float gravityScale = 5f;
    public float rotateSpeed;
    public float bounceForce = 8f;

    public float wallSlideSpeed = 3f;
    private int wallJumpLimit = 1;
    private int wallJumpCounter = 0;

    private int attackLimit = 1;
    private int attackCounter = 0;

    private Vector3 moveDirection;

    public CharacterController charController;
    private Camera mainCam;
    public GameObject[] characterModels;
    public Animator[] characterAnimators;

    public bool isKnocking;
    public float knockBackLength = 0.5f;
    private float knockBackCounter;
    public Vector2 knockbackPower;

    public GameObject[] playerPieces;

    public bool stopMove;
    public bool canTalkTo;
    public bool canInteractWith, canInteractWithUp, canInteractWithDown;
    public bool attackingUnlocked = true;

    public bool isInteracting;

    public int jumpSoundToPlay, attackSoundToPlay, rangedAttackSoundToPlay;

    public GameObject speechEffect;

    public GameObject bowl, bomb, bullet;
    public Transform rangedWeaponSlot;
    public float throwingSpeed, throwingHeight;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mainCam = Camera.main;
        isInteracting = false;
    }

    void Update()
    {
        // Movement
        if (!isKnocking && !stopMove && !isInteracting)
        {
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = 0f;
                wallJumpCounter = 0;

                if (Input.GetButtonDown("Jump"))
                {
                    AudioManager.instance.PlaySFX(jumpSoundToPlay);
                    moveDirection.y = jumpForce;
                }
            }

            if (moveDirection.y > 0 && !Input.GetButton("Jump"))
            {
                moveDirection.y += Physics.gravity.y * Time.deltaTime * lowJumpForce;
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, mainCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                characterModels[0].transform.rotation = Quaternion.Slerp(characterModels[0].transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
                characterModels[1].transform.rotation = Quaternion.Slerp(characterModels[1].transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
                characterModels[2].transform.rotation = Quaternion.Slerp(characterModels[2].transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
        }

        if (isKnocking)
        {
            knockBackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = characterModels[0].transform.forward * -knockbackPower.x;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = 0f;
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

            if (knockBackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        if (stopMove)
        {
            moveDirection = Vector3.zero;
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            charController.Move(moveDirection);
        }

        // Character Stats
        if (CharacterSwitch.instance.currentCharacter == 1)
        {
            characterModels[0].SetActive(true);
            characterModels[1].SetActive(false);
            characterModels[2].SetActive(false);

            jumpSoundToPlay = 7;
            attackSoundToPlay = 6;

            moveSpeed = 5f;
            jumpForce = 15f;
            throwingSpeed = 15;
            throwingHeight = 0f;
            charController.center = new Vector3(0f, 0.56f, 0f);
            charController.radius = 0.29f;
            charController.height = 1f;
            knockbackPower = new Vector2(3f, 8f);

            characterAnimators[0].SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
            characterAnimators[0].SetBool("Grounded", charController.isGrounded);
        }

        if (CharacterSwitch.instance.currentCharacter == 2)
        {
            characterModels[0].SetActive(false);
            characterModels[1].SetActive(true);
            characterModels[2].SetActive(false);

            jumpSoundToPlay = 4;
            attackSoundToPlay = 3;

            moveSpeed = 3.5f;
            jumpForce = 12.5f;
            throwingSpeed = 5;
            throwingHeight = 5f;
            charController.center = new Vector3(0f, 0.56f, 0f);
            charController.radius = 0.29f;
            charController.height = 1f;
            knockbackPower = new Vector2(2f, 7f);

            characterAnimators[1].SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
            characterAnimators[1].SetBool("Grounded", charController.isGrounded);
        }

        if (CharacterSwitch.instance.currentCharacter == 3)
        {
            characterModels[0].SetActive(false);
            characterModels[1].SetActive(false);
            characterModels[2].SetActive(true);

            jumpSoundToPlay = 13;
            attackSoundToPlay = 12;

            moveSpeed = 6.5f;
            jumpForce = 15f;
            throwingSpeed = 25;
            throwingHeight = 0f;
            charController.center = new Vector3(0f, 0.56f, 0f);
            charController.radius = 0.29f;
            charController.height = 1f;
            knockbackPower = new Vector2(3f, 8f);

            characterAnimators[2].SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
            characterAnimators[2].SetBool("Grounded", charController.isGrounded);
        }

        // Character Switch
        if (charController.isGrounded && !isInteracting)
        {
            if (Input.GetButtonDown("Character Switch Right"))
            {
                CharacterSwitch.instance.CharSwitchRight();
            }

            if (Input.GetButtonDown("Character Switch Left"))
            {
                CharacterSwitch.instance.CharSwitchLeft();
            }
        }

        if (speechEffect.activeInHierarchy && !canTalkTo)
        {
            speechEffect.SetActive(false);
        }

        // Interact
        // Dialogue
        if (canTalkTo && !isInteracting)
        {
            speechEffect.SetActive(true);

            if (Input.GetButtonDown("Interact"))
            {
                stopMove = true;
                isInteracting = true;
                DialogueTrigger.instance.TriggerDialogue();
            }
        }

        if (Input.GetButtonDown("Submit"))
        {
            if (DialogueManager.instance.speechInProgress)
            {
                DialogueManager.instance.SetSpeechNext();
            }
        }

        // Elevator
        if (canInteractWithUp && !isInteracting)
        {
            speechEffect.SetActive(true);

            if (Input.GetButtonDown("Interact"))
            {
                stopMove = true;
                isInteracting = true;
                ElevatorControllerUp.instance.MoveElevator();
            }
        }

        if (canInteractWithDown && !isInteracting)
        {
            speechEffect.SetActive(true);

            if (Input.GetButtonDown("Interact"))
            {
                stopMove = true;
                isInteracting = true;
                ElevatorControllerDown.instance.MoveElevator();
            }
        }

        // Char Unlocker
        if (canInteractWith && !isInteracting)
        {
            speechEffect.SetActive(true);

            if (Input.GetButtonDown("Interact"))
            {
                stopMove = true;
                isInteracting = true;
                CharUnlocker.instance.Unlock();
            }
        }

        // Attack
        if (!isInteracting && charController.isGrounded && attackingUnlocked)
        {
            if (Input.GetButtonDown("Attack"))
            {
                if (attackCounter < attackLimit)
                {
                    if (CharacterSwitch.instance.currentCharacter == 1)
                    {
                        characterAnimators[0].SetTrigger("Attacking");
                        StartCoroutine(AttackCo());
                    }

                    else if (CharacterSwitch.instance.currentCharacter == 2)
                    {
                        characterAnimators[1].SetTrigger("Attacking");
                        StartCoroutine(AttackCo());
                    }

                    else
                    {
                        characterAnimators[2].SetTrigger("Attacking");
                        StartCoroutine(AttackCo());
                    }
                }
            }
        }

        // Ranged Attack
        if (!isInteracting && charController.isGrounded && attackingUnlocked)
        {
            if (Input.GetButtonDown("Ranged Attack"))
            {
                if (attackCounter < attackLimit)
                {
                    if (CharacterSwitch.instance.currentCharacter == 1)
                    {
                        characterAnimators[0].SetTrigger("RangedAttacking");
                        StartCoroutine(RangedAttackCo());
                    }

                    else if (CharacterSwitch.instance.currentCharacter == 2)
                    {
                        characterAnimators[1].SetTrigger("RangedAttacking");
                        StartCoroutine(RangedAttackCo());
                    }

                    else
                    {
                        characterAnimators[2].SetTrigger("RangedAttacking");
                        StartCoroutine(RangedAttackCo());
                    }
                }
            }
        }
    }

    public void Knockback()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        moveDirection.y = knockbackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }

    public void Bounce()
    {
        moveDirection.y = bounceForce;
        charController.Move(moveDirection * Time.deltaTime);
    }

    IEnumerator AttackCo()
    {
        stopMove = true;
        attackCounter++;
        AudioManager.instance.PlaySFX(attackSoundToPlay);

        yield return new WaitForSeconds(0.85f);

        attackCounter = 0;
        stopMove = false;
    }

    IEnumerator RangedAttackCo()
    {
        stopMove = true;
        attackCounter++;
        AudioManager.instance.PlaySFX(rangedAttackSoundToPlay);

        if (CharacterSwitch.instance.currentCharacter == 1)
        {
            GameObject rangedWeaponInstance = Instantiate(bowl, rangedWeaponSlot.position, bowl.transform.rotation);
            rangedWeaponInstance.transform.rotation = Quaternion.LookRotation(-rangedWeaponSlot.forward);
            Rigidbody rangedWeaponRig = rangedWeaponInstance.GetComponent<Rigidbody>();

            rangedWeaponRig.AddForce(rangedWeaponSlot.forward * throwingSpeed, ForceMode.Impulse);
            rangedWeaponRig.AddForce(rangedWeaponSlot.up * throwingHeight, ForceMode.Impulse);
        }

        else if (CharacterSwitch.instance.currentCharacter == 2)
        {
            GameObject rangedWeaponInstance = Instantiate(bomb, rangedWeaponSlot.position, bomb.transform.rotation);
            rangedWeaponInstance.transform.rotation = Quaternion.LookRotation(-rangedWeaponSlot.forward);
            Rigidbody rangedWeaponRig = rangedWeaponInstance.GetComponent<Rigidbody>();

            rangedWeaponRig.AddForce(rangedWeaponSlot.forward * moveSpeed, ForceMode.Impulse);
            rangedWeaponRig.AddForce(rangedWeaponSlot.up * throwingHeight, ForceMode.Impulse);
        }

        else
        {
            GameObject rangedWeaponInstance = Instantiate(bullet, rangedWeaponSlot.position, bullet.transform.rotation);
            rangedWeaponInstance.transform.rotation = Quaternion.LookRotation(-rangedWeaponSlot.forward);
            Rigidbody rangedWeaponRig = rangedWeaponInstance.GetComponent<Rigidbody>();

            rangedWeaponRig.AddForce(rangedWeaponSlot.forward * moveSpeed, ForceMode.Impulse);
            rangedWeaponRig.AddForce(rangedWeaponSlot.up * throwingHeight, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(0.85f);

        attackCounter = 0;
        stopMove = false;

        yield return new WaitForSeconds(0.85f);

        attackCounter = 0;
    }

    // Walljump
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!charController.isGrounded && hit.normal.y < 0.1f && CharacterSwitch.instance.currentCharacter == 3)
        {
            if (moveDirection.y < -wallSlideSpeed)
            {
                moveDirection.y = -wallSlideSpeed;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (wallJumpCounter < wallJumpLimit)
                {
                    AudioManager.instance.PlaySFX(jumpSoundToPlay);
                    moveDirection.y = jumpForce;
                    wallJumpCounter++;
                }
            }
        }
    }
}
