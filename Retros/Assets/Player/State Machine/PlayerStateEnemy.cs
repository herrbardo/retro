using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateEnemy : PlayerStateBase
{
    bool methodsSubscribed;

    public override void Awake()
    {
        base.Awake();
        Susbscribe();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Unsusbcribe();
    }

    public override void StartState()
    {
        Material[] mats = Context.BodyRenderer.materials;
        mats[0] = Context.EnemyMaterial;
        Context.BodyRenderer.materials = mats;

        var particleSettings = Context.AuraPlayer.main;
        particleSettings.startColor = new Color(Context.EnemyAuraColor.r, Context.EnemyAuraColor.g, Context.EnemyAuraColor.b, Context.EnemyAuraColor.a);
    }

    public override void EnterState()
    {
        Susbscribe();
    }

    public override void ExitState()
    {
        Unsusbcribe();
    }

    public override void FixedUpdateState()
    {
        RetrieveStep();
        base.FixedUpdateState();
    }

    public override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Portal")
        {
            PortalArea portalArea = other.gameObject.GetComponent<PortalArea>();
            if(!portalArea.IsEnemy)
            {
                GameEvents.GetInstance().OnEnemyHasReachedPlayerPortal();
                PortalExtended portalExtended = portalArea.Portal.GetComponent<PortalExtended>();
                Context.TravelToPortal(portalExtended.PortalDoor.transform);
            }
        }

        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            Collider myCollider = Context.GetComponent<Collider>();
            Collider otherCollider = other.gameObject.GetComponent<Collider>();
            Physics.IgnoreCollision(otherCollider, myCollider);
        }

        base.OnCollisionEnter(other);
    }

    public override void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Portal")
        {
            PortalArea portalArea = other.gameObject.GetComponent<PortalArea>();
            if(portalArea.IsEnemy)
                GameEvents.GetInstance().OnEnemyLeftPortal();
        }
        
        base.OnCollisionExit(other);
    }

    private void RetrieveStep()
    {
        if(Context.StepsRecord == null || Context.StepsRecord.Count == 0)
            return;

        Step step = Context.StepsRecord.Dequeue();
        movementX = -step.X;
        movementZ = -step.Z;

        if(step.IsFire)
            Context.WeaponManager.StartFiring();
    }

    void PlayerWin(Queue<Step> steps)
    {
        Context.LockPlayer();
    }

    void Susbscribe()
    {
        if(methodsSubscribed)
            return;
        
        GameEvents.GetInstance().PlayerHasReachedEnemyPortal += PlayerWin;
        methodsSubscribed = true;
    }

    void Unsusbcribe()
    {
        if(!methodsSubscribed)
            return;

        GameEvents.GetInstance().PlayerHasReachedEnemyPortal -= PlayerWin;
        methodsSubscribed = false;
    }
}
