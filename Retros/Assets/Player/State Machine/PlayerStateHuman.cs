using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStateHuman : PlayerStateBase
{
    bool methodsSubscribed;

    public PlayerStateHuman(PlayerStateManager context)
    {
        this.Context = context;
    }

    public override void Awake()
    {
        base.Awake();
        Subscribe();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Unsubscribe();
    }

    void Subscribe()
    {
        if(methodsSubscribed)
            return;
        
        GameEvents.GetInstance().EnemyHasReachedPlayerPortal += EnemyHasReachedPlayerPortal;
        methodsSubscribed = true;
    }

    void Unsubscribe()
    {
        if(!methodsSubscribed)
            return;
        
        GameEvents.GetInstance().EnemyHasReachedPlayerPortal -= EnemyHasReachedPlayerPortal;
        methodsSubscribed = false;
    }

    public override void StartState()
    {
        Context.NavMeshAgent.enabled = false;
    }

    public override void EnterState()
    {
        Subscribe();
    }

    public override void ExitState()
    {
        Unsubscribe();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        RecordStep(false);
    }

    public override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Portal")
        {
            PortalArea portalArea = other.gameObject.GetComponent<PortalArea>();
            if(portalArea.IsEnemy)
            {
                GameEvents.GetInstance().OnPlayerHasReachedEnemyPortal(Context.StepsRecord);
                PortalExtended portalExtended = portalArea.Portal.GetComponent<PortalExtended>();
                Context.TravelToPortal(portalExtended.PortalDoor.transform);
            }
        }
        
        base.OnCollisionEnter(other);
    }

    public void MovePlayer(float x, float z)
    {
        movementX = x;
        movementZ = z;
    }

    void RecordStep(bool isFire)
    {
        if(Context.StepsRecord == null)
            Context.StepsRecord = new Queue<Step>();
        
        Context.StepsRecord.Enqueue(new Step(){ IsFire = isFire, X = movementX, Z = movementZ });
    }

    public override void Fire()
    {
        base.Fire();
        RecordStep(true);
    }

    void EnemyHasReachedPlayerPortal()
    {
        Context.LockPlayer();
    }
}
