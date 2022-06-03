using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class enableDisable : MonoBehaviour
{
    public CinemachineImpulseSource impulse;
    public Player player;
    private PlayerControls GameActions;
    public Vector2 _moveInput { get; private set; }
    public int _moveInputNormalX { get; private set; }
    public int _moveInputNormalY { get; private set; }
    public GameObject _inventory;
    public InventoryObject inventory;
    public bool _display = false;
    public bool passThrough;
    [SerializeField]
    private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;

    public bool jumpInputStop {get; private set; }
    public bool dashInput {get; private set; }
    public bool dashInputStop {get; private set; }
    private float dashInputStartTime;

    public float RawDashDirectionInput {get; private set; }
    private PlayerInput playerInput;
    private Camera Cam;

    public bool Interacting;


    public bool NextDialogue  { get; private set; }
    public bool attackInput { get; private set; }

    public bool JumpInput { get; private set; }

    public bool DialogueOver { get; private set; }


    void Awake()
    {
        GameActions = new PlayerControls();
    }
    void Start()
    {
        GeneralControlsEnable();
        playerInput = GetComponent<PlayerInput>();
        Cam = Camera.main;
    }

    public void GeneralControlsEnable()
    {
        GameActions.GeneralControls.Enable();
        //GetComponent<attackBehavior>().OnEnable();
    }   
    
    public void GeneralControlsDisable()
    {
        GameActions.GeneralControls.Disable();
        //GetComponent<attackBehavior>().OnDisable();
    }

    public void MenuControlsEnable()
    {
        GameActions.MenuControls.Enable();
    }

    public void MenuControlsDisable()
    {
        GameActions.MenuControls.Disable();
    }
    void FixedUpdate()
    {
        _moveInput = GameActions.GeneralControls.Movement.ReadValue<Vector2>();
        _moveInputNormalX = (int)(_moveInput * Vector2.right).normalized.x;
        _moveInputNormalY = (int)(_moveInput * Vector2.up).normalized.y;

        
        GameActions.GeneralControls.Jump.performed += _ => Jump();
        GameActions.GeneralControls.Jump.canceled += _ => DownwardForce();

        GameActions.GeneralControls.Dash.performed+= _ => OnDashInput();
        GameActions.GeneralControls.Dash.canceled += _ => OnDashCancel();

        GameActions.GeneralControls.LightAttack.performed += _ => OnAttackInput();
        GameActions.GeneralControls.LightAttack.performed += _ => StartCoroutine(holdAttackInput());
        GameActions.GeneralControls.Interact.performed += _ => OnInteractInput();
        GameActions.GeneralControls.Interact.canceled += _ => OnInteractCancel();

        GameActions.MenuControls.NextDialogue.performed += _ => OnNextDialogue();
    
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    private void Jump()
    {
        JumpInput = true;
        jumpInputStop = false;
        jumpInputStartTime = Time.time;
    }

    private void DownwardForce()
    {
        jumpInputStop = true;
    }
    
    
    
    public void UseJumpInput() => JumpInput = false;
    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void OnDashInput()
    {
        dashInput = true;
        dashInputStop = false;
        dashInputStartTime = Time.time;
        RawDashDirectionInput = (float)player.FacingDirection;
        impulse.GenerateImpulse();
    }
    public void OnDashCancel() => dashInputStop = true;
    public void UseDashInput() => dashInput = false;

    public void OnAttackInput() => attackInput = true;
    public void UseAttackInput() => attackInput = false;
    private IEnumerator holdAttackInput()
    {
        yield return new WaitForSeconds(player.playerData.attackInputHoldTime);
        attackInput = false;
    }

    public void OnInteractInput() => Interacting = true;
    public void OnInteractCancel() => Interacting = false;

    public void OnNextDialogue() => NextDialogue = true;
    public void NextDialogueFalse() => NextDialogue = false;

    public void DialogueFinished() => DialogueOver = true;
    public void ResetDialogue() => DialogueOver = false;

    private void CheckDashInputHoldTime()
    {
        if(Time.time > dashInputStartTime + inputHoldTime)
        {
            dashInput = false;
        }
    }

    /*


    //Dodge with Cooldown
    GameActions.GeneralControls.Dodge.performed += _ => checkForDodge();

    //Open Inventory
    GameActions.GeneralControls.Inventory.performed += _ => openInventory();

    //Close Inventory
    GameActions.MenuControls.Inventory.performed += _ => closeInventory();

    //Grapple
    GameActions.GeneralControls.Grapple.performed += _ => onGrapple();

    //FallThrough Platform
    GameActions.GeneralControls.FallThrough.performed += _ => fallThrough();

    GameActions.GeneralControls.FallThrough.canceled += _ => noFallThrough();
}

private void checkForDodge()
{
    if(GetComponent<basicMovement>()._canDodge)
    {
        GetComponent<basicMovement>().onDodge();
    }
}

private void openInventory()
{
    _inventory.SetActive(true);
    _display = true;
    ShowDisplay();
    GameActions.MenuControls.Enable();
    GeneralControlsDisable();
}

private void closeInventory()
{
    _inventory.SetActive(false);
    _display = false;
    ClearDisplay();
    GetComponent<PlayerInventory>().Save();
    GameActions.MenuControls.Disable();
    GeneralControlsEnable();
}

public void ClearDisplay()
{
    Debug.Log("Cleared");
    GameObject[] inventorySlots;
    inventorySlots = GameObject.FindGameObjectsWithTag("Inventory UI");
    foreach(GameObject slot in inventorySlots)
    {
        slot.GetComponent<Image>().enabled = false;
        slot.GetComponent<Button>().enabled = false;               
        slot.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }
}
public void ShowDisplay()
{
    GameObject[] inventorySlots;
    inventorySlots = GameObject.FindGameObjectsWithTag("Inventory UI");
    foreach(GameObject slot in inventorySlots)
    {
        slot.GetComponent<Image>().enabled = true;
        slot.GetComponent<Button>().enabled = true;
        slot.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }
}

private void onGrapple()
{
    if(grappleBehavior._grappleRange)
    {
        GetComponent<basicMovement>().Grapple();
    }
}


private void fallThrough()
{
    passThrough = true;
}

private void noFallThrough()
{
    passThrough = false;
}
*/
}
