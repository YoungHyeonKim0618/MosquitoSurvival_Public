using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class Slime : MonoBehaviour
{
    public static int numOfSlimes = 0;
    
    
    [SerializeField] private float _defaultHp, _defaultSpd;
    
    protected float _hp;
    protected float _spd;


    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _sr;
    protected Vector2 _dir = Vector2.zero;
    protected IEnumerator wa;

    public float _Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (value < 0)
            {
                _hp = 0;
                OnDie();
            }
        }
    }

    private float slowRemaining = 0;

    public void AddSlow(float t)
    {
        if (slowRemaining < t)
            slowRemaining = t;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        
        
    }



    public void OnSpawn()
    {
        numOfSlimes++;
        _Hp = _defaultHp;
        _spd = _defaultSpd; //* Random.Range(0.85f, 1.15f);
        
        wa = waitAndSetDirection();
        StartCoroutine(wa);
        StartCoroutine(waitAndDecreaseBuffTime());
    }

    public virtual void OnDie()
    {
        numOfSlimes--;
        RewardManager.instance.OnKilledNormalSlime(transform.position, _defaultHp);
        if(wa != null)
            StopCoroutine(wa);
        StopCoroutine(waitAndDecreaseBuffTime());

        slowRemaining = 0;
        Push();
    }

    public virtual void Push()
    {
        ObjectPool.instance.Push(gameObject,ObjectType.SLIME);
    }
    void setDirection()
    {
        _dir = (PlayerStatManager.instance.transform.position - transform.position).normalized;
        _rigidbody.velocity = slowRemaining <= 0 ?  _dir * (_spd * 0.13f) : _dir * (_spd* 0.07f * _rigidbody.mass);
        if (_dir.x >= 0) _sr.flipX = true;
        else _sr.flipX = false;
    }

    public void setHpCoef(float c)
    {
        _defaultHp *= c;
    }

    IEnumerator waitAndSetDirection()
    {
        WaitForSeconds ws = new WaitForSeconds(0.2f);
        while (true)
        {
            setDirection();
            yield return ws;
        }
    }
    
    public virtual void setDamage(float dmg)
    {
        float r = Random.Range(0.875f, 1.125f);
        dmg *= r;
        
        _Hp -= dmg;
        _rigidbody.AddForce(-_dir * (_spd * 0.3f),ForceMode2D.Impulse);

        UnityEventManager.instance.onDamageSlime.Invoke(dmg,transform.position);
        SoundObject.instance.HitSound();
    }

    IEnumerator waitAndDecreaseBuffTime()
    {
        WaitForSeconds ws = new WaitForSeconds(0.25f);
        while (true)
        {
            if (slowRemaining > 0)
                slowRemaining -= 0.25f;

            yield return ws;
        }
    }
}
