using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] public Renderer BodyRenderer;
    [SerializeField] public Material EnemyMaterial;
    [SerializeField] public Color EnemyAuraColor;
    [SerializeField] public ParticleSystem AuraPlayer;
    [SerializeField] public Weapon WeaponManager;
    [SerializeField] public Queue<Step> StepsRecord;
    [SerializeField] public float MovementSpeed;
    [SerializeField] public float RotationSpeed;
    [SerializeField] GameObject EnergyWavePrefab;
    [SerializeField] bool Inmortal;
    [NonSerialized] public bool IsMoving;
    [NonSerialized] public Animator Animator;
    [NonSerialized] public Rigidbody PlayerRigidbody;

    PlayerStateBase currentState;
    

    public PlayerStateBase CurrentState { get { return this.currentState;} }

    public PlayerStateManager()
    {
        currentState = new PlayerStateHuman(this);
    }

    void Awake()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        currentState.Awake();
    }

    void OnDestroy()
    {
        currentState.OnDestroy();
    }

    void Start()
    {
        Animator = GetComponent<Animator>();
        currentState.StartState();
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    void OnCollisionEnter(Collision other)
    {
        currentState.OnCollisionEnter(other);
    }

    void OnCollisionExit(Collision other)
    {
        currentState.OnCollisionExit(other);
    }

    private void OnMove(InputValue movementValue)
    {   
        Vector2 movementVector = movementValue.Get<Vector2>();
        PlayerStateHuman human = currentState as PlayerStateHuman;
        if(human != null)
            human.MovePlayer(movementVector.x, movementVector.y);
    }

    void OnFire()
    {
        PlayerStateHuman human = currentState as PlayerStateHuman;
        if(human != null)
            human.Fire();
    }

    public void SetState(PlayerStateBase newState)
    {
        this.currentState.ExitState();
        this.currentState = newState;
        this.currentState.Context = this;
        this.currentState.EnterState();
    }

    public void LockPlayer()
    {
        SetState(new PlayerStateLocked(this));
    }

    public void Die()
    {
        if(Inmortal)
            return;

        SetState(new PlayerStateDead(this));
    }

    public void TravelToPortal(Transform targetPosition)
    {
        SetState(new PlayerStateTravelling(this, targetPosition));
    }

    public void Dissapear()
    {
        Instantiate(this.EnergyWavePrefab, this.transform.position, Quaternion.identity);
        Invoke("FinallyDissapear", .5f);
    }

    void FinallyDissapear()
    {
        Destroy(this.gameObject);
    }
}
