using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public static Vector3 lastCheckpointPos = new Vector3(146, 2, 135);

    [Header("Character")]
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;
    [SerializeField] public Animator animAlythea;
    [SerializeField] public Animator animIR;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] GameObject jetpackParticles;

    [Header("Gun")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform barrelTransform;
    [SerializeField] Transform bulletParent;
    [SerializeField] float bulletHitMissDistance = 25;
    [SerializeField] public PoolManager poolManager;
    [SerializeField] Transform spawnPoint;

    [Header("UI")]
    public TextMeshProUGUI ammoDisplay;
    public Image healthBar;
    public GameObject dialogueStart;
    public GameObject dialogueLab;
    public GameObject dialogueAntenna;
    //public GameObject gameOver;

    [Header("Interaction")]
    [SerializeField] public bool jump;
    [SerializeField] public bool inGround;

    private float x, y;
    private Vector3 playerVelocity;
    private Transform cameraTransform;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction changeAction;
    private InputAction reloadAction;

    private int bulletsCount;
    private int ammo;
    public int bulletType;

    int layerMask;

    public Vector2 velocity;

    private void Start()
    {
        Global.isDead = false;
        
        ammoDisplay.text = bulletsCount + " / 7";

        layerMask = 1 << 11;
        layerMask = ~layerMask;
    }
    
    private void Awake()
    {
        currentHealth = maxHealth;

        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        animAlythea = GameObject.Find("Alythea").GetComponent<Animator>();
        animIR = GameObject.Find("IR_67").GetComponent<Animator>();

        cameraTransform = Camera.main.transform;

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        changeAction = playerInput.actions["Change"];
        reloadAction = playerInput.actions["Reload"];

        bulletsCount = 7;
        Global.reloading = false;

        Cursor.lockState = CursorLockMode.Locked;

        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckpointPos;
    }

    private void OnEnable()
    {
        shootAction.performed += _ => ShootGun();
        //jumpAction.performed += _ => JumpUp();
        reloadAction.performed += _ => Reload();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => ShootGun();
        //jumpAction.performed -= _ => JumpUp();
        reloadAction.performed -= _ => Reload();
    }

    void Update()
    {
        Global.groundedPlayer = controller.isGrounded;
        if (Global.groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpUp();
        } 

        Movement();

        GroundChecker();

        AimAnimations();

        GrapplingAnimations();

        PickAnimations();

        if (Global.totalJump == 2)
        {
            Global.totalJump = 0;
            //Debug.Log("Tocando suelo");
        }
        if (Global.totalJump <= 2 && Global.witchAvatarIsOn == 2)
        {
            Global.totalJump = 0;
        }

        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void Movement()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        playerVelocity.y += gravityValue * Time.deltaTime;       
        controller.Move(playerVelocity * Time.deltaTime);

        animAlythea.SetFloat("VelX", input.x);
        animAlythea.SetFloat("VelY", input.y);
        animIR.SetFloat("VelX", input.x);
        animIR.SetFloat("VelY", input.y);

        //Rotacio camera direccio
        //comprobar que no hi ha input de moviment
        if(input != Vector2.zero || Global.ISaim == true)
        {
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        velocity = input;

        if(input == Vector2.zero)
        {
            Global.moving = false;
        }
        else
        {
            Global.moving = true;
        }
    }

    public void AimAnimations()
    {
        if (Global.ISaim && !Global.moving)
        {
            animAlythea.SetBool("runAim", false);
            animAlythea.SetBool("aim", true);
        }
        else if (Global.ISaim && Global.moving)
        {
            animAlythea.SetBool("runAim", true);
            animAlythea.SetBool("aim", true);
        }
        else if(!Global.ISaim && !Global.moving)
        {
            animAlythea.SetBool("runAim", false);
            animAlythea.SetBool("aim", false);
        }
        else if(Global.ISaim && !Global.moving)
        {
            animAlythea.SetBool("runAim", false);
            animAlythea.SetBool("aim", true);
        }
        else if(!Global.ISaim && Global.moving)
        {
            animAlythea.SetBool("runAim", false);
            animAlythea.SetBool("aim", false);
        }
    }

    public void GrapplingAnimations()
    {
        if (Global.ISaim && !Global.moving)
        {
            animIR.SetBool("runGrap", false);
            animIR.SetBool("aimGrap", true);
        }
        else if (Global.ISaim && Global.moving)
        {
            animIR.SetBool("runGrap", true);
            animIR.SetBool("aimGrap", true);
        }
        else if(!Global.ISaim && !Global.moving)
        {
            animIR.SetBool("runGrap", false);
            animIR.SetBool("aimGrap", false);
        }
        else if(Global.ISaim && !Global.moving)
        {
            animIR.SetBool("runGrap", false);
            animIR.SetBool("aimGrap", true);
        }
        else if(!Global.ISaim && Global.moving)
        {
            animIR.SetBool("runGrap", false);
            animIR.SetBool("aimGrap", false);
        }
    }

    public void PickAnimations()
    {
        if (Global.ISpicking && !Global.moving)
        {
            animIR.SetBool("pick", true);
            //animIR.SetBool("pickRun", false);
            //animIR.SetBool("pickIdle", true);
        }
        else if (Global.ISpicking && Global.moving)
        {
            animIR.SetBool("pickRun", true);
            animIR.SetBool("pickIdle", false);
        }
        else if (Global.ISpicking && !Global.moving)
        {
            animIR.SetBool("pickIdle", true);
            animIR.SetBool("pickRun", false);
        }
        else if (!Global.ISpicking && !Global.moving)
        {
            animIR.SetBool("pickRun", false);
            animIR.SetBool("pick", false);
            //animIR.SetBool("pickIdle", true);
        }
        else if (!Global.ISpicking && Global.moving)
        {
            //animIR.SetBool("pickRun", false);
            //animIR.SetBool("pickIdle", false);
        }
    }

    private void ShootGun()
    {
        if (Global.ISaim == true && Global.witchAvatarIsOn == 1 && Global.reloading == false)
        {
            RaycastHit hit;
            GameObject bullet = PoolManager.instance.GetPooledObject(bulletType);
            bullet.transform.position = spawnPoint.position;
            bullet.SetActive(true);
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

            bulletsCount --;

            if (bulletsCount <= 0){
                Global.reloading = true;
                StartCoroutine(ReloadWait());
            }

            ammoDisplay.text = bulletsCount + " / 7";
        }
    }
    
    public void Reload()
    {
        Global.reloading = true;
        StartCoroutine(ReloadWait());
    }

    private void JumpUp()
    {
        if (Global.totalJump == 1)
        {
            //Debug.Log("Jetpack");
            GameObject particles = (GameObject)Instantiate(jetpackParticles, transform.position, transform.rotation);
            Destroy(particles, 2f);
            playerVelocity.y = 0;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            Global.totalJump++;
        }
        
        if (Global.groundedPlayer == true && Global.totalJump == 0)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            Global.totalJump++;
        }
    }

    private void GroundChecker()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1.3f, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.3f, layerMask))
        {
            animAlythea.SetBool("jump", false);
            animIR.SetBool("jump", false);
            inGround = true;
        }
        else
        {
            animAlythea.SetBool("jump", true);
            animIR.SetBool("jump", true);
            inGround = false;
        }
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

        if (other.gameObject.CompareTag("DialogueStart"))
        {
            dialogueStart.SetActive(true);
        }

        if (other.gameObject.CompareTag("DialogueLab"))
        {
            dialogueLab.SetActive(true);
        }

        if (other.gameObject.CompareTag("DialogueAntenna"))
        {
            dialogueAntenna.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision other)    
    {
        if (other.gameObject.CompareTag("BulletEnemy"))
        {
            currentHealth = currentHealth - damage;
            //Debug.Log ("muerte");

            if(currentHealth <= 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        Global.isDead = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ReloadWait()
    {
        //Debug.Log("Reloading: " + Global.reloading);
        //Debug.Log("PreReaload");
        yield return new WaitForSeconds(3);
        //Debug.Log("PosReaload");
        bulletsCount = 7;
        Global.reloading = false;
    }
}
