using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatellitePrj : MonoBehaviour
{
    private Satellite _satellite;

    private void Awake()
    {
        _satellite = GetComponentInParent<Satellite>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Slime"))
        {
            UnityEventManager.instance.OnPrjHit.Invoke(other.transform);
            _satellite.OnHitSlime(other.GetComponent<Slime>());
        }
            
    }
}
