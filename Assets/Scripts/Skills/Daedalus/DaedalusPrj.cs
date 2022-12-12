using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using Random = UnityEngine.Random;

public class DaedalusPrj : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    private float _dmg;
    private float _spd;
    private float _lifeTime;

    private bool _penet = false;

    private Transform t;

    private void Awake()
    {
        t = transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 position = t.position;
        if (other.CompareTag("Slime"))
        {
            if(!_penet)
                bounce(other.ClosestPoint(position) - position);
            other.GetComponent<Slime>().setDamage(_dmg);
        }
        else if (other.CompareTag("BounceWall"))
        {
            bounce(other.ClosestPoint(position) - position);
        }
    }

    public void InitPrj(float dmg, float spd, float lifeTime, bool penet = false)
    {
        Vector2 dest;
        int r = Random.Range(0, 100);

        if (r < 25)
            dest = new Vector2(1, 1);
        else if (r < 50)
            dest = new Vector2(1, -1);
        else if (r < 75)
            dest = new Vector2(-1, 1);
        else dest = new Vector2(-1, -1);
        _dmg = dmg;
        _spd = spd;
        _lifeTime = lifeTime;
        _penet = penet;

        _rb.velocity = dest.normalized * spd;
        setRotation();

        
        Destroy(gameObject,lifeTime);
    }

    void bounce(Vector2 collision)
    {
        Vector2 curDest = _rb.velocity;
        Vector2 dest;
        //현재 방향이 우상단을 향할 때
        if (curDest.x >= 0 && curDest.y >= 0)
        {
            if (collision.y >= collision.x)
                dest = new Vector2(1, -1);
            else dest = new Vector2(-1, 1);
        }
        //우하단을 향할 때
        else if (curDest.x >= 0 && curDest.y < 0)
        {
            if (collision.y >= -collision.x)
                dest = new Vector2(-1, -1);
            else dest = new Vector2(1, 1);
        }
        //좌상단을 향할 때
        else if (curDest.x < 0 && curDest.y >= 0)
        {
            if (collision.y >= -collision.x)
                dest = new Vector2(-1, -1);
            else dest = new Vector2(1, 1);
        }
        //좌하단을 향할 때
        else
        {
            if (collision.y >= collision.x)
                dest = new Vector2(1, -1);
            else dest = new Vector2(-1, 1);
        }

        _rb.velocity = dest.normalized * _spd;
        setRotation();
    }

    void setRotation()
    {
        transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(_rb.velocity.y,_rb.velocity.x)*Mathf.Rad2Deg);
    }
}
