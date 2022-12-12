using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NeutralExplosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatManager.instance.SetDamaged(55);
        }
        else if (other.CompareTag("Slime"))
        {
            other.GetComponent<Slime>().setDamage(55);
        }
    }

    private void Start()
    {
        Transform particle = transform.GetChild(0);
        particle.SetParent(null);
        Destroy(particle.gameObject, 0.5f);
        Destroy(gameObject, 0.05f);
    }
}
