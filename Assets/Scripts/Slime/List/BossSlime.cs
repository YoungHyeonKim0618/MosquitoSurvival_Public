using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossSlime : Slime
{
    public override void OnDie()
    {
        RewardManager.instance.OnKilledBossSlime(transform.position);
        if(wa != null)
            StopCoroutine(wa);
        Destroy(this.gameObject);
    }

}
