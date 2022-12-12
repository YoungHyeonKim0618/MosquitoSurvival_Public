using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatellitePrjEvolved : MonoBehaviour
{
    

    [SerializeField]
    private float r;

    private float theta;

    float Theta
    {
        get
        {
            return theta;
        }
        set
        {
            if (theta >= 360)
                value = 0;

            theta = value;
        }
    }

    private Satellite _satellite;

    private void Awake()
    {
        _satellite = GetComponentInParent<Satellite>();
    }

    public void InitRotate(float a)
    {
        Theta += a;
    }

    private void Update()
    {
        Theta += Time.deltaTime * 120 * PlayerStatManager.instance.GetPrjSpdCoef() * _satellite.getSpdCoef() ;
        transform.localPosition = new Vector3(r * Mathf.Cos(theta * Mathf.Deg2Rad), r * Mathf.Sin(theta * Mathf.Deg2Rad) * 0.5f);
    }

    private void FixedUpdate()
    {
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
