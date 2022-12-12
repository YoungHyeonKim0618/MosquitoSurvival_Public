using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBombExploded : MonoBehaviour
{
    private float _dmg;

    private List<Slime> slimesInRange = new List<Slime>();


    [SerializeField] private GameObject particlePrefab;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Slime")) return;
        
        slimesInRange.Add(other.GetComponent<Slime>());
    }

    //데미지를 정해줌. 
    public void SetDmg(float t)
    {
        _dmg = t;
        StartCoroutine(waitAndDoDmg());
        Destroy(this.gameObject,0.1f);
    }

    IEnumerator waitAndDoDmg()
    {
        yield return null;
        yield return null;

        for (int i = 0; i < slimesInRange.Count; i++)
        {
            if (slimesInRange[i].gameObject.activeInHierarchy)
            {
                slimesInRange[i].setDamage(_dmg);
            }
        }
    }
}
