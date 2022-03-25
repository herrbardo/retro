using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase
{
    protected float movementX;
    protected float movementZ;
    public PlayerStateManager Context{ get; set;}

    public virtual void Awake()
    {
        GameEvents.GetInstance().RoundTimeout += RoundTimeout;
    }

    public virtual void OnDestroy()
    {
        GameEvents.GetInstance().RoundTimeout -= RoundTimeout;
    }

    public abstract void StartState();

    public virtual void FixedUpdateState()
    {
        Move();
    }

    public virtual void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            BulletMovement bulletMovement = other.gameObject.GetComponent<BulletMovement>();
            if(bulletMovement.OwnerTag != Context.gameObject.tag)
                Context.Die();
        }
    }

    public virtual void OnCollisionExit(Collision other)
    {
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {

    }

    public abstract void EnterState();
    public abstract void ExitState();

    void Move()
    {
        float horizontal = movementX;
        float vertical = movementZ;
        Context.IsMoving = movementX != 0 || movementZ != 0;

        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical);
        movementDirection.Normalize();
        Context.gameObject.transform.Translate(movementDirection * Context.MovementSpeed * Time.deltaTime, Space.World);

        if(movementDirection != Vector3.zero)
            LookAt(movementDirection, Context.gameObject.transform, Context.RotationSpeed);

        SetAnimations(horizontal, vertical, Context.Animator);
    }

    void LookAt(Vector3 movementDirection, Transform transform, float rotationSpeed)
    {
        Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);
    }

    void SetAnimations(float horizontal, float vertical, Animator animator)
    {
        bool enableIsRunning = horizontal != 0 || vertical != 0;
        animator.SetBool("isRunning", enableIsRunning);
    }

    public virtual void Fire()
    {
        Context.WeaponManager.StartFiring();
    }

    void RoundTimeout()
    {
        Context.LockPlayer();
    }
}
