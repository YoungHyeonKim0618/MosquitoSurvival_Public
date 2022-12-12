using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BlinkSlashPrj : MonoBehaviour
{
    private float _dmg = 0;
    
    private List<Slime> slimesInRange = new List<Slime>();
    [SerializeField] private SpriteRenderer _sr;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Slime")) return;
        
        slimesInRange.Add(other.GetComponent<Slime>());
    }

    
    public void SetDamage(float dmg)
    {
        _dmg = dmg;
        StartCoroutine(waitAndSlash());
        _sr.DOFade(0, 0.2f).SetUpdate(true);
    }

    public void SetColor(Color c)
    {
        _sr.color = c;
    }

    IEnumerator waitAndSlash()
    {
        yield return new WaitForSecondsRealtime(0.05f);
        for (int i = 0; i < slimesInRange.Count; i++)
        {
            if (slimesInRange[i].gameObject.activeInHierarchy)
            {
                slimesInRange[i].setDamage(_dmg);
            }
        }

        yield return new WaitForSecondsRealtime(0.23f);
        Destroy(gameObject);
    }
}
