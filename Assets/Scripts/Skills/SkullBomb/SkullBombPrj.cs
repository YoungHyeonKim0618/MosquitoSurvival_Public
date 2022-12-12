using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBombPrj : MonoBehaviour
{
    private float _dmg, _range, _spd;
    private int _penetration = 0;

    private SkullBomb _skullBomb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Slime")) return;
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        
        UnityEventManager.instance.OnPrjHit.Invoke(other.transform);
        _skullBomb.Explode(other.transform.position);
        if(_penetration == 0)
        {
            //트레일렌더러 제거.
            if (transform.childCount != 0)
            {
                Transform child = transform.GetChild(0);
                child.SetParent(null);
                Destroy(child.gameObject, 0.25f);
            }

            Destroy(this.gameObject);
        }
        else
        {
            _penetration--;
        }
    }


    public void setPrj(float spd, Vector2 dir, SkullBomb skullBomb, int penet = 0)
    {
        _skullBomb = skullBomb;
        _spd = spd;
        transform.rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg * Mathf.Atan2(dir.y,dir.x));
        _penetration = penet;

        transform.localScale *= (100 + PlayerStatManager.instance.SkillSize) / 100;
        GetComponent<Rigidbody2D>().velocity = dir.normalized * _spd;
    }
}
