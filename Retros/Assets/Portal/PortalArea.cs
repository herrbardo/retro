using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PortalArea : MonoBehaviour
{
    [SerializeField] public bool IsEnemy;
    [SerializeField] public GameObject Portal;
    public bool IsEmpty { get { return puta == 0;} }
    
    int puta = 0;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
            puta++;
    }

    private void OnCollisionStay(Collision other)
    {
        
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
            puta--;
    }
}
