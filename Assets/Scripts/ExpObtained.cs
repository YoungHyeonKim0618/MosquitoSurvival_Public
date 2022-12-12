using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpObtained : MonoBehaviour
{
    /*
     * ObtainRange에 들어왔을 때 생성함.
     * 
     */


    [SerializeField] private int expAmount;

    private Rigidbody2D _rb;
    private Vector2 _startPos;
    private float _curPos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        _curPos += Time.deltaTime * 1.5f;
        _rb.MovePosition(ConstMethods.Lerp(_startPos,PlayerController.instance.transform.position,_curPos));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerObtainCollider"))
        {
            PlayerStatManager.instance.ObtainEXP(expAmount);
            ObjectPool.instance.Push(this.gameObject,ObjectType.EXP_OBTAINED);
        }
    }

    /*
     * 생성되었을 때, 플레이어의 반대 방향으로 조금 갔다가 플레이어를 따라감.
     */
    public void Init(int amount)
    {
        _curPos = -0.75f;
        expAmount = amount;
        _startPos = transform.position;
    }

}
