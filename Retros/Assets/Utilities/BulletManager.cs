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
        PlayerController otherPlayer = other.gameObject.GetComponent<PlayerController>();

        if((otherTag == "Player" && IsEnemy && !otherPlayer.IsEnemy)
        || (otherTag == "Enemy" && !IsEnemy && otherPlayer.IsEnemy)
        || (otherTag == "Environment"))
        {
            Die();
            Instantiate(EnergyExplosionPrefab, this.transform.position, Quaternion.identity);
        }
    }

    void Die()
    {
        if(isDead)
            return;
        
        if(bulletRigidbody != null)
            bulletRigidbody.velocity = Vector3.zero;
        if(sphereCollider != null)
            sphereCollider.enabled = false;
        
        isDead = true;
        for (int i = 1; i < Systems.Length; i++)
        {
            Destroy(Systems[i].gameObject);
        }
    }
}
