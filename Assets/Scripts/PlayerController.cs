using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    
    [Header("Values")]
    [SerializeField] float playerSpeed = 7;
    [SerializeField] float jumpHeight = 1;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float rotationSpeed = 100;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;
    [SerializeField] float damage = 10;
    [SerializeField] CharacterController controller;

    [Header("Character")]
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;
    [SerializeField] public Animator animAlythea;
    [SerializeField] public Animator animIR;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] GameObject GroundChecker;

    [Header("Gun")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform barrelTransform;
    [SerializeField] Transform bulletParent;
    [SerializeField] float bulletHitMissDistance = 25;

    [Header("UI")]
    public TextMeshProUGUI ammoDisplay;
    public Image healthBar;
    public GameObject gameOver;

    [Header("Interaction")]
    [SerializeField] public bool jump;
    [SerializeField] bool saltfunciona = false;

    private float x, y;
    private Vector3 playerVelocity;
    private Transform cameraTransform;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction changeAction;
    private InputAction realoadActio;

    private int bulletscount;

    //public bool isDead;

    private void Start()
    {
        currentHealth = maxHealth;
        ammoDisplay.text = (7 - bulletscount) + " / 7";
    }
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        animAlythea = GameObject.Find("Alythea").GetComponent<Animator>();
        animIR = GameObject.Find("IR_67").GetComponent<Animator>();

        cameraTransform = Camera.main.transform;

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        changeAction = playerInput.actions["Change"];
        realoadActio = playerInput.actions["Reload"];

        bulletscount = 0;
        Global.reloading = false;

        animAlythea.SetBool("jump", false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        shootAction.performed += _ => ShootGun();
        jumpAction.performed += _ => JumpUp();
        realoadActio.performed += _ => Reload();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => ShootGun();
        jumpAction.performed -= _ => JumpUp();
        realoadActio.performed -= _ => Reload();
    }

    private void ShootGun()
    {
        if (Global.ISaim == true && Global.witchAvatarIsOn == 1 && Global.reloading == false)
        {
            RaycastHit hit;
            GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
            BulletController bulletController = bullet.GetComponent<BulletController>();
                      
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
            {
                bulletController.target = hit.point;
                bulletController.hit = true;
            }
            else
            {
                bulletController.target = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
                bulletController.hit = false;
            }
            bulletscount ++;
            ammoDisplay.text = bulletscount + " / 7";
            //Debug.Log(bulletscount);
            if (bulletscount == 7){
                Global.reloading = true;
                StartCoroutine(ReloadWait());
            }
        }
    }

    void Update()
    {
        Global.groundedPlayer = controller.isGrounded;
        if (Global.groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        /*if (Global.groundedPlayer)
        {
            animAlythea.SetBool("jump", false);
        }
        else
        {
            animAlythea.SetBool("jump", true);
        }*/

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        //controller.Jump(animAlythea.SetBool("jump", true));

        playerVelocity.y += gravityValue * Time.deltaTime;       
        controller.Move(playerVelocity * Time.deltaTime);

        animAlythea.SetFloat("VelX", input.x);
        animAlythea.SetFloat("VelY", input.y);
        animIR.SetFloat("VelX", input.x);
        animIR.SetFloat("VelY", input.y);

        //Rotacio camera direccio
        //comprobar que no hi ha input de moviment
        if(input != Vector2.zero || Global.ISaim == true){
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


        if (Global.totalJump == 2)
        {
            Global.totalJump = 0;
        }
        if (Global.totalJump <= 2 && Global.witchAvatarIsOn == 2)
        {
            Global.totalJump = 0;
        }

        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void Reload(){
        Global.reloading = true;
        StartCoroutine(ReloadWait());
    }

    private void JumpUp()
    {
        if (Global.totalJump == 1)
        {
            playerVelocity.y = 0;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            Global.totalJump++;
        }
    

        if (Global.groundedPlayer == true && Global.totalJump == 0)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            Global.totalJump++;
            //animAlythea.SetBool("jump", true);
        }
        //else
        //{
            //animAlythea.SetBool("jump", false);
        //}
    }

    IEnumerator ReloadWait()
    {
        Debug.Log("Reloading: " + Global.reloading);
        //Debug.Log("PreReaload");
        yield return new WaitForSeconds(3);
        //Debug.Log("PosReaload");
        bulletscount = 0;
        Global.reloading = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        Death();

        if (other.gameObject.CompareTag("Punch"))
        {
            currentHealth = currentHealth - damage;
            //Debug.Log ("hit");

            if(currentHealth <= 0)
            {
                Death();
            }
        }
    }

    /*void OnTriggerStay(Collider GroundChecker)
    {
        if (GroundChecker.gameObject.CompareTag("Wall"))
        {
            animAlythea.SetBool("jump", false);
        }
        else
        {
            animAlythea.SetBool("jump", true);
        }
    }*/

    void Death()
    {
        //Destroy(Player1);
        //Destroy(Player2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //gameOver.gameObject.SetActive(true);
    }
}
