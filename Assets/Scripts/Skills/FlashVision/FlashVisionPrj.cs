using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashVisionPrj : MonoBehaviour
{
    private float _dmg;

    public void SetDmg(float dmg)
    {
        _dmg = dmg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Slime"))
        {
            UnityEventManager.instance.OnPrjHit.Invoke(other.transform);
            other.GetComponent<Slime>().setDamage(_dmg);
        }
    }
    
    

}
