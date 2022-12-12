using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSlime : Slime
{
    public override void OnDie()
    {
        RewardManager.instance.OnKilledGoldSlime(transform.position);
        if(wa != null)
            StopCoroutine(wa);
        Destroy(this.gameObject);
    }
}
