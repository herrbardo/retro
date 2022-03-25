using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAutomated : PlayerStateBase
{
    Vector3 currentDestination;

    public override void StartState()
    {
        
    }

    public override void EnterState()
    {
        Context.NavMeshAgent.enabled = true;
        Context.Animator.SetBool("isRunning", true);
        currentDestination = Context.PlayerPrevPortalArea.position;
    }

    public override void ExitState()
    {
        Context.NavMeshAgent.enabled = false;
    }

    public override void FixedUpdateState()
    {
        if(Context.NavMeshAgent.enabled)
            Context.NavMeshAgent.destination = currentDestination;
        else
        {
            float step =  Context.MovementSpeed * Time.deltaTime;
            Context.transform.position = Vector3.MoveTowards(Context.transform.position, currentDestination, step);
        }
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
        base.OnCollisionEnter(other);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PrevPortal")
        {
            Context.NavMeshAgent.enabled = false;
            currentDestination = Context.PlayerPortalArea.position;
        }
            
        base.OnTriggerEnter(other);
    }
}
