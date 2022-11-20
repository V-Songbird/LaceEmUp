using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appendPlayerAsChildOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.transform.CompareTag("Player"))
        {
            collisionInfo.transform.parent = transform;
        }
    }
    
    private void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.transform.CompareTag("Player"))
        {
            collisionInfo.transform.parent = null;
        }
    }
}
