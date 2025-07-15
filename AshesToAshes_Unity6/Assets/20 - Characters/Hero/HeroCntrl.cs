using UnityEngine;

public class HeroCntrl : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private PlayerInputCntrl playerInputCntrl;
    [SerializeField] private float rotationSpeed = 400.0f;

    private Animator animator;

    private Vector3 moveDirection;
    private Vector3 playerDirection;

    private Vector2 playerMove;

    private int speedId;
    private int dashSpeedId;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        speedId = Animator.StringToHash("move");
        dashSpeedId = Animator.StringToHash("dashspeed");
    }

    // Update is called once per frame
    void Update()
    {
        playerMove = playerInputCntrl.Move;

        Move(Time.deltaTime);
    }

    public bool IsMoving()
    {
        return (playerMove.magnitude > 0.0f);
    }

    public void Move(float dt)
    {
        playerDirection.x = playerMove.x; // Horizontal
        playerDirection.y = 0.0f;
        playerDirection.z = playerMove.y; // Vertical

        float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);

        animator.SetFloat(speedId, inputMagnitude, 0.05f, dt);

        moveDirection = mainCamera.transform.TransformDirection(playerDirection);
        moveDirection.y = 0.0f;

        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * dt);
        }
    }
}
