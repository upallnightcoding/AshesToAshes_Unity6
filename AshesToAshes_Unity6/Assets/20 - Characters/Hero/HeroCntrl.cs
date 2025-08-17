using System.Collections;
using UnityEngine;

public class HeroCntrl : MonoBehaviour
{
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private PlayerInputCntrl playerInputCntrl;

    private GameObject mainCamera;

    private float rotationSpeed = 400.0f;

    [SerializeField] private ProjectileSO projectileSO;

    private Animator animator;

    private Vector3 moveDirection;
    private Vector3 playerDirection;

    private Vector2 playerMove;

    private int speedId;
    private int dashSpeedId;

    private Fsm fsm = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        fsm = new Fsm();
        fsm.AddState(new HeroIdleState(this));
        fsm.AddState(new HeroMoveState(this));

        speedId = Animator.StringToHash("move");
        dashSpeedId = Animator.StringToHash("dashspeed");
    }

    public void Initialize(GameObject mainCamera)
    {
        this.mainCamera = mainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        playerMove = playerInputCntrl.Move;

        if (playerInputCntrl.AttackLight || playerInputCntrl.AttackHeavy)
        {
            ExecuteLightAttack();
        }

        fsm.OnUpdate(Time.deltaTime);
    }

    public void ExecuteLightAttack()
    {
        StartCoroutine(FireProjectile());

        playerInputCntrl.AttackLight = false;
        playerInputCntrl.AttackHeavy = false;
    }

    private IEnumerator FireProjectile()
    {
        //GameObject projectile = Instantiate(projectileSO.projectilePrefab, muzzlePoint.position, Quaternion.identity);
        //projectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSO.force);
        //projectile.GetComponent<ProjectileCntrl>().Set(projectileSO, projectile.transform, muzzlePoint, transform);
        //Destroy(projectile, projectileSO.duration);

        projectileSO.lunch(transform, muzzlePoint);

        yield return null;
    }

    public bool IsMoving()
    {
        return (playerMove.magnitude > 0.2f);
    }

    public void StopMovement()
    {
        animator.SetFloat(speedId, 0.0f);
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
