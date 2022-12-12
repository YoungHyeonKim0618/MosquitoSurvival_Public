using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPrj : MonoBehaviour
{
    [SerializeField] private bool evolvedPrj;
    
    private bool isDisabled = false;
    /*
     * 기본 공격으로 나가는 투사체.
     * 공격력, 이동 속도, 관통
     */
    private float _dmg, _spd;
    private int _penetration;

    private Rigidbody2D _rb;
    private Collider2D _collider;
    private SpriteRenderer _sr;

    private Vector2 defaultSize;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();

        defaultSize = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDisabled) return;

        if (!gameObject.activeSelf) return;
        
        if (other.CompareTag("Slime"))
        {

            UnityEventManager.instance.OnPrjHit.Invoke(other.transform);
            other.GetComponent<Slime>().setDamage(_dmg);

            if (_penetration == 0)
            {
                StartCoroutine(waitAndPush());
            }
            else _penetration--;
        }
        else if (other.CompareTag("Wall"))
        {
            ObjectPool.instance.PushAutoPrj(this);
        }
    }


    public void setPrj(float dmg, float spd, int penet, Vector2 dir)
    {
        _dmg = dmg;
        _spd = spd;
        _penetration = penet;
        transform.rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg * Mathf.Atan2(dir.y,dir.x));

        transform.localScale = defaultSize *  (100 + PlayerStatManager.instance.SkillSize) / 100;
        _rb.velocity = dir.normalized * _spd;
    }

    IEnumerator waitAndPush()
    {
        //트레일렌더러 분리 -> 비활성화 -> wait -> 트레일렌더러 결합 -> push
        Color c = _sr.color;
        
        _sr.color = Color.clear;
        Transform child = transform.GetChild(0);
        child.SetParent(null);
        _rb.velocity = Vector2.zero;
        isDisabled = true;

        yield return new WaitForSeconds(0.4f);

        
        child.SetParent(transform);
        if(evolvedPrj)
            ObjectPool.instance.PushAutoPrj(this,true);
        else
        {
            ObjectPool.instance.PushAutoPrj(this);
        }
        isDisabled = false;
        _sr.color = c;
    }
}
