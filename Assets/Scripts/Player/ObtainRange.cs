using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObtainRange : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EXP1"))
        {
            _pool.Push(other.gameObject,ObjectType.EXP1);
            GameObject go = _pool.Pop(ObjectType.EXP_OBTAINED);
            
            ExpObtained exp = go.GetComponent<ExpObtained>();
            go.transform.position = other.transform.position;
            exp.Init(1);
        }
        else if(other.CompareTag("EXP5"))
        {
            _pool.Push(other.gameObject,ObjectType.EXP5);
            ExpObtained exp = _pool.Pop(ObjectType.EXP_OBTAINED).GetComponent<ExpObtained>();
            exp.transform.position = other.transform.position;
            exp.Init(5);
        }
        else if(other.CompareTag("EXP25"))
        {
            PlayerStatManager.instance.ObtainEXP(25);
            _pool.Push(other.gameObject,ObjectType.EXP25);
        }
        else if (other.CompareTag("Chest"))
        {
            RewardManager.instance.ObtainChest();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            PlayerStatManager.instance.CurHp += 20;
        }
    }
}
