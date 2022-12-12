using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IronSlime : Slime
{
    private void Start()
    {
        Destroy(this.gameObject,60);
    }

    public override void Push()
    {
        base.Push();
    }

    public override void OnDie()
    {
        if(wa != null)
            StopCoroutine(wa);
        Destroy(this.gameObject);
    }

    public override void setDamage(float dmg)
    {
        if (dmg > 10)
        {
            dmg = 10 + Mathf.Sqrt(dmg - 10);
        }
        
        float r = Random.Range(0.875f, 1.125f);
        dmg *= r;
        
        _Hp -= dmg;

        UnityEventManager.instance.onDamageSlime.Invoke(dmg,transform.position);
    }
}
