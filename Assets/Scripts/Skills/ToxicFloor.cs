using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicFloor : MonoBehaviour, AttackSkill
{
    /*
     * 범위 내의 적을 지속적으로 공격하는 스킬.
     * 적을 처음으로 탐지할 때에도 피해를 줌.
     */

    private int curSkillLevel = 0;
    //장판 범위
    [SerializeField] private float _range = 1;
    //장판 피해량
    [SerializeField] private float _dmg = 5;
    //장판 공격 딜레이
    [SerializeField] private float _delay = 0.5f;
    
    
    //사거리 내의 슬라임들.
    private List<Slime> slimesInRange = new List<Slime>();

    private void Start()
    {
        setRange();
        StartCoroutine(waitAndHitSlime());
        StartCoroutine(waitAndRemoveErrors());
        UnityEventManager.instance.OnSkillSizeChanged.AddListener(setRange);
    }

    private bool _isEvolved = true;
    public void Evolve()
    {
        _isEvolved = true;
    }
    
    public void UpgradeSkill()
    {
        setSkillLevel(++curSkillLevel);
    }

    void setSkillLevel(int i)
    {
        i = Mathf.Clamp(i, 0, 7);
        curSkillLevel = i;

        switch (i)
        {
            case 1:
                _range = 1;
                _dmg = 5;
                _delay = 0.5f;
                break;
            case 2: //범위 증가.
                _range = 1.25f;
                _dmg = 5;
                _delay = 0.5f;
                break;
            case 3: //딜레이 감소.
                _range = 1.25f;
                _dmg = 5;
                _delay = 0.4f;
                break;
            case 4: //데미지 증가.
                _range = 1.25f;
                _dmg = 6;
                _delay = 0.4f;
                break;
            case 5: //범위 증가.
                _range = 1.5f;
                _dmg = 6;
                _delay = 0.4f;
                break;
            case 6: //데미지 증가.
                _range = 1.5f;
                _dmg = 7;
                _delay = 0.4f;
                break;
            case 7: //범위 증가.
                _range = 1.75f;
                _dmg = 7;
                _delay = 0.4f;
                break;
        }
        setRange();
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Slime"))
        {
            Slime t = other.GetComponent<Slime>();
            slimesInRange.Add(t);
            hitSlime(t);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    { 
        if(other.CompareTag("Slime"))
            slimesInRange.Remove(other.GetComponent<Slime>());
    }

    private void hitSlime(Slime slime)
    {
        slime.setDamage(_dmg * PlayerStatManager.instance.GetDmgCoef());
    }

    IEnumerator waitAndHitSlime()
    {
        while (true)
        {
            for (int i = 0; i < slimesInRange.Count; i++)
            {
                if (slimesInRange[i])
                {
                    hitSlime(slimesInRange[i]);
                }
            }

            yield return new WaitForSeconds(_delay * PlayerStatManager.instance.GetCoolCoef());
        }
    }

    void setRange()
    {
        transform.localScale = new Vector2(1, 1) * _range * PlayerStatManager.instance.GetSizeCoef();
    }
    void removeErrors()
    {
        for (int i = 0; i < slimesInRange.Count; i++)
        {
            if ((slimesInRange[i].transform.position - transform.position).magnitude > 3)
                slimesInRange.Remove(slimesInRange[i]);
        }
    }

    IEnumerator waitAndRemoveErrors()
    {
        WaitForSeconds ws = new WaitForSeconds(1f);
        while (true)
        {
            yield return ws;
            removeErrors();
        }
    }
}
