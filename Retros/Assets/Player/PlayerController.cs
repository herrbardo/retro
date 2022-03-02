using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] float movementSpeed, rotationSpeed;

    [SerializeField] Weapon weaponManager;

    [SerializeField] Material EnemyMaterial;

    [SerializeField] Renderer BodyRenderer;

    [SerializeField] float gravity;

    public bool IsEnemy;
    public bool IsMoving;

    private CharacterController characterController;
    private Animator animator;
    private Vector3 movementDirection = Vector3.zero;
    private bool playerGrounded;
    private float movementX;
    private float movementZ;
    private Queue<Step> stepsRecord;
    private bool canMove;
    private bool canShoot;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        GameEvents.GetInstance().EnemyHasReachedPlayerPortal += EnemyWin;
        GameEvents.GetInstance().PlayerHasReachedEnemyPortal += PlayerWin;
        GameEvents.GetInstance().RoundTimeout += LockPlayer;

        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        GameEvents.GetInstance().EnemyHasReachedPlayerPortal -= EnemyWin;
        GameEvents.GetInstance().PlayerHasReachedEnemyPortal -= PlayerWin;
        GameEvents.GetInstance().RoundTimeout -= LockPlayer;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        canMove = true;
        canShoot = true;

        if(IsEnemy)
        {
            Material[] mats = BodyRenderer.materials;
            mats[0] = EnemyMaterial;
            BodyRenderer.materials = mats;
        }
    }

    private void FixedUpdate()
    {
        if(IsEnemy)
            RetrieveStep();
        Move();
        if(canMove && !IsEnemy)
            RecordStep(false);
    }

    private void OnMove(InputValue movementValue)
    {
        if(IsEnemy)
            return;
        
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    private void RetrieveStep()
    {
        if(this.stepsRecord == null || this.stepsRecord.Count == 0)
            return;

        Step step = this.stepsRecord.Dequeue();
        movementX = -step.X;
        movementZ = -step.Z;

        if(step.IsFire && canShoot)
            weaponManager.StartFiring();
    }

    void Move()
    {
        if(!canMove)
            return;

        float horizontal = movementX;
        float vertical = movementZ;
        IsMoving = movementX != 0 || movementZ != 0;

        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical);
        movementDirection.Normalize();
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);

        if(movementDirection != Vector3.zero)
            LookAt(movementDirection);

        SetAnimations(horizontal, vertical);
    }

    void LookAt(Vector3 movementDirection)
    {
        Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);
    }

    void SetAnimations(float horizontal, float vertical)
    {
        bool enableIsRunning = horizontal != 0 || vertical != 0;
        animator.SetBool("isRunning", enableIsRunning);
    }

    void OnFire()
    {
        if(!IsEnemy && canShoot)
        {
            weaponManager.StartFiring();
            RecordStep(true);
        }
    }

    void RecordStep(bool isFire)
    {
        if(this.stepsRecord == null)
            this.stepsRecord = new Queue<Step>();
        
        this.stepsRecord.Enqueue(new Step(){ IsFire = isFire, X = movementX, Z = movementZ });
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Portal")
        {
            PortalArea portalArea = other.gameObject.GetComponent<PortalArea>();
            if(!IsEnemy && portalArea.IsEnemy)
            {
                LockPlayer();
                GameEvents.GetInstance().OnPlayerHasReachedEnemyPortal(this.stepsRecord);
            }
            else if(IsEnemy && !portalArea.IsEnemy)
            {
                LockPlayer();
                GameEvents.GetInstance().OnEnemyHasReachedPlayerPortal();
            }
        }
        else if(other.gameObject.tag == "Bullet")
        {
            BulletMovement bulletMovement = other.gameObject.GetComponent<BulletMovement>();
            if(bulletMovement.OwnerTag != this.gameObject.tag)
                Die();
        }
    }

    void LockPlayer()
    {
        canMove = false;
        canShoot = false;
        SetAnimations(0, 0);
    }

    public void SetSteps(Queue<Step> steps)
    {
        this.stepsRecord = steps;
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        this.canMove = false;
        this.canShoot = false;
        playerRigidbody.isKinematic = true;
    }

    void PlayerWin(Queue<Step> steps)
    {
        if(IsEnemy)
            LockPlayer();
    }

    void EnemyWin()
    {
        if(!IsEnemy)
            LockPlayer();
    }
}