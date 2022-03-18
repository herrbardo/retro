using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalExtended : MonoBehaviour
{
    [SerializeField] public Transform PortalDoor;
    [SerializeField] float RotationsPerMinute;

    private void Update()
    {
        //PortalDoor.Rotate(0 , 6.0f * RotationsPerMinute * Time.deltaTime, 0);
    }
}
