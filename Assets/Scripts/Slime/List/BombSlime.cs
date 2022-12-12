using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSlime : Slime
{
    public override void OnDie()
    {
        UnityEventManager.instance.NeutralExplode(transform.position);
        base.OnDie();
    }
}
