using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCharacter : MonoBehaviour
{
    
    [Header("Player")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] public GameObject character1, character2, ScrpPick;

    [Header("Animator")]
    [SerializeField] Animator animAlythea;
    [SerializeField] Animator animIR;

    private InputAction changeAction;
    private bool groundedPlayer;
    Vector3 originalPos;

    private void Awake()
    {

        changeAction = playerInput.actions["Change"];
        animAlythea = GameObject.Find("Alythea").GetComponent<Animator>();
        animIR = GameObject.Find("IR_67").GetComponent<Animator>();

    }

    private void OnEnable()
    {

        changeAction.performed += _ => ChangeCharacter();

    }


    private void OnDisable()
    {
        changeAction.performed -= _ => ChangeCharacter();
    }

    void Update()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }


    // Start is called before the first frame update
    void Start()
    {
        character1.gameObject.SetActive (true);
        character2.gameObject.SetActive (false);

    }

    // Update is called once per frame

    public void ChangeCharacter()
    {
        if (Global.groundedPlayer == true)
        {
            switch (Global.witchAvatarIsOn)
            {
                case 1:
                    Global.witchAvatarIsOn = 2;

                    animAlythea.keepAnimatorControllerStateOnDisable = true;
                    character1.gameObject.SetActive(false);
                    character2.gameObject.SetActive(true);
                    //animAlythea.Rebind();
                    //animIR.Rebind();
                    gameObject.transform.position = originalPos;
                    break;

                case 2:
                    Global.witchAvatarIsOn = 1;


                    //DROP OBJECT POSSIBLE CANVIAR A FUNCIO EN EL PICKUPOBEJC
                    //pickUpObj();

                    animIR.keepAnimatorControllerStateOnDisable = true;
                    character1.gameObject.SetActive(true);
                    character2.gameObject.SetActive(false);
                    gameObject.transform.position = originalPos;
                    //animAlythea.Rebind();
                    //animIR.Rebind();
                    break;
            }
        }
    }
    
}
