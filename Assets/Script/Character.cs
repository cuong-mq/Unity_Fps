using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public CharacterController controller;

    public float speed;
    public float sprintSpeed;
    public float gravity;
    public float jumpHeight;

    public Vector3 velocity;

    public Transform groundChecker;
    public float groundeDistance;
    public LayerMask groundMask;

    public bool isGrounded;

    public bool isSprinting;
    public bool isWalking;
    public Slider sprintSlider;
    public float Stamina;
    public float staminaDecay;
    public float staminaHeal;
    public float maxStamina;
    public bool emtyStamina;

    public string walkAnim;
    public string sprintAnim;
    public string sprintReturnAnim;
    public string sprintAnim2;
    public string jumpAnim;
    public string shakeAnim;

    public GameObject sprintAnimObject;
    public GameObject sprintAnim2Object;
    public GameObject walkAnimObject;
    public GameObject jumpAnimObject;
    public GameObject shakeAnimObject;


    public GameObject sprintSoundObject;

    public PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (isSprinting)
            {
                shakeAnimObject.GetComponent<Animation>().Play(shakeAnim);
            }
            if(Stamina < maxStamina)
            {
                sprintSlider.gameObject.SetActive(true);
            }
            else
            {
                sprintSlider.gameObject.SetActive(false);
            }

            //Audio
            if (isSprinting)
            {
                sprintSoundObject.SetActive(true);
            }
            else
            {
                sprintSoundObject.SetActive(false);
            }
            //Animation
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                sprintAnimObject.GetComponent<Animation>().Play(sprintAnim);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                sprintAnimObject.GetComponent<Animation>().Play(sprintReturnAnim);
            }

            if (isWalking)
            {
                walkAnimObject.GetComponent<Animation>().Play(walkAnim);
            }

            if (isSprinting)
            {
                sprintAnim2Object.GetComponent<Animation>().Play(sprintAnim2);
            }

            //Sprinting
            sprintSlider.value = Stamina;
            sprintSlider.maxValue = maxStamina;
            if (Stamina > 10)
            {
                emtyStamina = false;
            }

            if (Stamina <= 0)
            {
                emtyStamina = true;
            }

            if (emtyStamina)
            {
                isSprinting = false;
            }

            if (Input.GetKey(KeyCode.LeftShift) && !emtyStamina)
            {
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
            }
            if (isSprinting && Stamina > 0)
            {
                Stamina -= staminaDecay * Time.deltaTime;
            }
            else
            {
                if (Stamina < maxStamina)
                {
                    Stamina += staminaHeal * Time.deltaTime;
                }
            }
            //

            //Movement
            if (Input.GetKeyUp(KeyCode.W) && !isSprinting || Input.GetKeyUp(KeyCode.A) && !isSprinting || Input.GetKeyUp(KeyCode.S) && !isSprinting || Input.GetKeyUp(KeyCode.D) && !isSprinting)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            if (!isSprinting)
            {
                controller.Move(move * speed * Time.deltaTime);
            }
            else
            {
                controller.Move(move * sprintSpeed * Time.deltaTime);
            }

            velocity.y -= gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            isGrounded = Physics.CheckSphere(groundChecker.position, groundeDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }

        }
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        jumpAnimObject.GetComponent<Animation>().Play(jumpAnim);

    }
}
