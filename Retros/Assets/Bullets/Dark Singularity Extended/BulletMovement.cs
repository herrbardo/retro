using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float Speed;
    public string OwnerTag;
    public Transform TargetTransform;

    Vector3 targetDirection;
    Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        targetDirection = (TargetTransform.position - transform.position).normalized;
        RotateTowards();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = targetDirection * Speed;
    }

    private void RotateTowards()
    {
        var offset = -90f;
        Vector2 direction = (Vector2) TargetTransform.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
