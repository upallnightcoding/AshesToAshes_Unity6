using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInputCntrl : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Fire { get; set; }
    public bool Dash { get; set; }
    public bool AttackLight { get; set; }

    private InputActionMap inputActionMap;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction fireAction;
    private InputAction dashAction;
    private InputAction lightAction;

    private void Awake()
    {
        inputActionMap = playerInput.currentActionMap;

        moveAction = inputActionMap.FindAction("Move");
        lookAction = inputActionMap.FindAction("Look");
        fireAction = inputActionMap.FindAction("Fire");
        dashAction = inputActionMap.FindAction("Dash");
        lightAction = inputActionMap.FindAction("AttackLight");

        moveAction.performed += OnMove;
        lookAction.performed += OnLook;
        fireAction.performed += OnFire;
        dashAction.performed += OnDash;
        lightAction.performed += OnLightAttack;

        moveAction.canceled += OnMove;
        lookAction.canceled += OnLook;
        fireAction.canceled += OnFire;
        dashAction.canceled += OnDash;
        lightAction.canceled += OnLightAttack;
    }

    private void OnLightAttack(InputAction.CallbackContext context)
    {
        AttackLight = context.ReadValueAsButton();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        Fire = context.ReadValueAsButton();
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
