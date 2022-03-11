using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public bool IsEnemy;

    [SerializeField] ParticleSystem[] Systems;
    [SerializeField] GameObject EnergyExplosionPrefab;


    Rigidbody bulletRigidbody;
    SphereCollider sphereCollider;
    bool isDead;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        string otherTag = other.gameObject.tag;

        if(otherTag == "Environment")
            Die();
        else
        {
            PlayerController otherPlayer = other.gameObject.GetComponent<PlayerController>();
            if(otherPlayer == null)
                return;

            if(IsEnemy && !otherPlayer.IsEnemy)
                Die();
            else if(!IsEnemy && otherPlayer.IsEnemy)
                Die();
        }
    }

    void Die()
    {
        if(isDead)
            return;
        isDead = true;
        Instantiate(EnergyExplosionPrefab, this.transform.position, Quaternion.identity);

        if(bulletRigidbody != null)
            bulletRigidbody.velocity = Vector3.zero;
        if(sphereCollider != null)
            sphereCollider.enabled = false;
        
        for (int i = 1; i < Systems.Length; i++)
            Destroy(Systems[i].gameObject);
    }
}
