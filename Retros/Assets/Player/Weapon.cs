using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool IsFiring;
    [SerializeField] ParticleSystem MuzzleFlash;
    [SerializeField] GameObject SingularBulletPrefab;
    [SerializeField] Transform AimPoint;
    [SerializeField] GameObject Player;

    void Start()
    {
        StopFiring();
    }

    void Update()
    {
        
    }

    public void StartFiring()
    {
        IsFiring = true;
        MuzzleFlash.Emit(1);
        GameObject clone = Instantiate(SingularBulletPrefab, transform.position, Quaternion.identity);
        BulletMovement bulletMovement = clone.GetComponent<BulletMovement>();
        bulletMovement.TargetTransform = AimPoint;
        bulletMovement.OwnerTag = Player.tag;
        
        PlayerStateManager playerManager = Player.GetComponent<PlayerStateManager>();
        BulletManager bulletManager = clone.GetComponent<BulletManager>(); 
        bulletManager.IsEnemy = playerManager.CurrentState is PlayerStateEnemy;
    }

    public void StopFiring()
    {
        IsFiring = false;
        MuzzleFlash.Stop();
    }
}
