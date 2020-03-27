using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;
    public float rotateSpeed;
    public float bounceForce = 8f;

    public float wallSlideSpeed = 3f;
    private int wallJumpLimit = 1;
    private int wallJumpCounter = 0;

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

    public bool isInteracting;

    public int jumpSoundToplay;

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
                    AudioManager.instance.PlaySFX(jumpSoundToplay);
                    moveDirection.y = jumpForce;
                }
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

            jumpSoundToplay = 7;

            moveSpeed = 5f;
            jumpForce = 15f;
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

            jumpSoundToplay = 4;

            moveSpeed = 3.5f;
            jumpForce = 12.5f;
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

            jumpSoundToplay = 13;

            moveSpeed = 6.5f;
            jumpForce = 15f;
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

        // Interact
        if (DialogueTrigger.instance.canTalkTo && !isInteracting)
        {
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
                    AudioManager.instance.PlaySFX(jumpSoundToplay);
                    moveDirection.y = jumpForce;
                    wallJumpCounter++;
                }
            }
        }
    }
}
