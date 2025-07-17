using System.Collections;
using UnityEngine;

public class HeroCntrl : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private PlayerInputCntrl playerInputCntrl;
    [SerializeField] private float rotationSpeed = 400.0f;
    [SerializeField] private Transform muzzlePoint;

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

    // Update is called once per frame
    void Update()
    {

        playerMove = playerInputCntrl.Move;

        if (playerInputCntrl.Fire)
        {
            Fire();
        }

        fsm.OnUpdate(Time.deltaTime);

        //Move(Time.deltaTime);
    }

    public void Fire()
    {
        StartCoroutine(FireProjectile());

        playerInputCntrl.Fire = false;
    }

    private IEnumerator FireProjectile()
    {
        GameObject projectile = Instantiate(projectileSO.projectilePrefab, muzzlePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSO.force);
        projectile.GetComponent<ProjectileCntrl>().Set(projectileSO, projectile.transform, muzzlePoint, transform);
        Destroy(projectile, projectileSO.duration);
        yield return null;
    }

    public bool IsMoving()
    {
        return (playerMove.magnitude > 0.2f);
    }

    public void StopAnimation()
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
