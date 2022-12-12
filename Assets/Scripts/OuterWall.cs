using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EXP1"))
        {
            ObjectPool.instance.Push(other.gameObject,ObjectType.EXP1);
        }
        else if (other.CompareTag("EXP5"))
        {
            ObjectPool.instance.Push(other.gameObject,ObjectType.EXP5);
        }
    }
}
