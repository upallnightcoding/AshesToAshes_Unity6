using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInputCntrl : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    //public bool lightAttack { get; set; }
    public bool Dash { get; set; }
    public bool AttackLight { get; set; }
    public bool AttackHeavy { get; set; }

    private InputActionMap inputActionMap;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction lightAction;
    private InputAction heavyAction;
    //private InputAction dashAction;
    private InputAction lightAttackAction;

    private void Awake()
    {
        inputActionMap = playerInput.currentActionMap;

        moveAction = inputActionMap.FindAction("Move");
        lookAction = inputActionMap.FindAction("Look");
        lightAction = inputActionMap.FindAction("LightAttack");
        heavyAction = inputActionMap.FindAction("HeavyAttack");

        //dashAction = inputActionMap.FindAction("Dash");
        //lightAction = inputActionMap.FindAction("AttackLight");

        moveAction.performed += OnMove;
        lookAction.performed += OnLook;
        lightAction.performed += OnLightAttack;
        heavyAction.performed += OnHeavyAttack;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
        //dashAction.performed += OnDash;
        //lightAttackAction.performed += OnHeavyAttack;

        moveAction.canceled += OnMove;
        lookAction.canceled += OnLook;
        lightAction.canceled += OnLightAttack;
        heavyAction.performed += OnHeavyAttack;
        //dashAction.canceled += OnDash;
        //lightAttackAction.canceled += OnHeavyAttack;
    }

    private void OnHeavyAttack(InputAction.CallbackContext context)
    {
        AttackHeavy = context.ReadValueAsButton();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    private void OnLightAttack(InputAction.CallbackContext context)
    {
        AttackLight = context.ReadValueAsButton();
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        Dash = context.ReadValueAsButton();
    }

    private void OnEnable()
    {
        inputActionMap.Enable();
    }

    private void OnDisable()
    {
        inputActionMap.Disable();
    }
}
