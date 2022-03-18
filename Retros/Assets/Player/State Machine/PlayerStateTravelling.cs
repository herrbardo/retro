using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateTravelling : PlayerStateBase
{
    Transform targetPosition;

    public PlayerStateTravelling(PlayerStateManager context, Transform targetPoistion)
    {
        Context = context;
        this.targetPosition = targetPoistion;
    }

    public override void StartState()
    {

    }

    public override void EnterState()
    {
        Context.Animator.SetBool("isFloating", true);
        Context.PlayerRigidbody.useGravity = false;

        Vector3 v = new Vector3(this.targetPosition.position.x, this.targetPosition.position.y + .5f, this.targetPosition.position.z);
        Context.StartCoroutine(MoveToPortal(3f, Context.gameObject.transform, v));
    }

    public override void ExitState()
    {
        
    }

    public override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "PortalDoor")
        {
            Context.StartCoroutine(ScaleDown(.5f, Context.gameObject.transform, new Vector3(0f, 0f, 0f)));
        }
    }

    IEnumerator MoveToPortal(float duration, Transform transform, Vector3 targetPosition)
    {
        float currentTime = 0;
        Vector3 initialPosition = transform.position;

        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, currentTime / duration);
            yield return null;
        }

        yield break;
    }

    IEnumerator ScaleDown(float duration, Transform transform, Vector3 targetScale)
    {
        float currentTime = 0;
        Vector3 initialScale = transform.localScale;
        Context.Dissapear();
        
        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, currentTime / duration);
            yield return null;
        }

        yield break;
    }
}
