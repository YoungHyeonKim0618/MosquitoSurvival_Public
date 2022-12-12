using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InnerQuake : MonoBehaviour, AttackSkill
{
    /*
     * 레슈락 w. 발동시 주변의 적들 중 랜덤한 1명을 타격하기를 빠르게 반복한다.
     * 피해를 줌과 동시에 Particle을 그곳에 생성한다.
     */

    private float _dmg = 2;
    private int _numOfPrj = 1;
    private int _numOfQuake = 40;
    private float _delay = 12;

    private List<Slime> slimesInRange = new List<Slime>();

    //쿨타임 관리 ---
    
    //fixedUpdate에선 skillTimer를 감소한다.
    
    private float skillTimer = 0;

    private bool skillCondition()
    {
        return slimesInRange.Count > 0;
    }

    IEnumerator waitAndCompareCondition()
    {
        //스킬 타이머 초기화
        skillTimer = _delay * PlayerStatManager.instance.GetCoolCoef();
        WaitForSeconds ws = new WaitForSeconds(0.5f);
        while (true)
        {
            yield return ws;
            if (skillCondition() && skillTimer <= 0)
            {
                skillTimer = _delay * PlayerStatManager.instance.GetCoolCoef();
                execute();
            }
        }
    }
    //쿨타임 관리 ---

    private void Start()
    {
        StartCoroutine(waitAndRemoveErrors());
        //StartCoroutine(waitAndExecute());
        StartCoroutine(waitAndCompareCondition());
    }

    private void FixedUpdate()
    {
        if (skillTimer > 0)
        {
            skillTimer -= Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Slime"))
        {
            slimesInRange.Add(other.GetComponent<Slime>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Slime"))
        {
            slimesInRange.Remove(other.GetComponent<Slime>());
        }
    }

    private bool _isEvolved = false;
    public void Evolve()
    {
        _isEvolved = true;
    }
    private int curSkillLevel = 0;
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
                _dmg = 3f;
                _numOfQuake = 18;
                _delay = 12f;
                break;
            case 2: //진동 횟수 증가
                _dmg = 3f;
                _numOfQuake = 18;
                _delay = 12f;
                break;
            case 3: //쿨타임 감소
                _dmg = 3f;
                _numOfQuake = 18;
                _delay = 10.8f;
                break;
            case 4: //공격력 증가
                _dmg = 3.6f;
                _numOfQuake = 18;
                _delay = 10.8f;
                break;
            case 5: //진동 횟수 증가
                _dmg = 3.6f;
                _numOfQuake = 22;
                _delay = 10.8f;
                break;
            case 6: //쿨타임 감소
                _dmg = 3.6f;
                _numOfQuake = 22;
                _delay = 9.6f;
                break;
            case 7: //진동 횟수 증가
                _dmg = 3.6f;
                _numOfQuake = 26;
                _delay = 9.6f;
                break;
        }
    }

    void execute()
    {
        if(!_isEvolved)
        {
            StartCoroutine(waitAndQuake(_numOfQuake + 6 * PlayerStatManager.instance.NumOfPrj));
        }
        else
        {
            StartCoroutine(waitAndQuake(2 * _numOfQuake + 12 * PlayerStatManager.instance.NumOfPrj));
        }
    }

    /*
    IEnumerator waitAndExecute()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay * PlayerStatManager.instance.GetCoolCoef());
            execute();
        }
    }
    */

    IEnumerator quake(Slime target)
    {
        target.setDamage(_dmg * PlayerStatManager.instance.GetDmgCoef());
        ParticleSystem ps = ObjectPool.instance.PopInnerParticle();
        ps.transform.position = target.transform.position;
        yield return new WaitForSeconds(0.3f);
        ObjectPool.instance.PushInnerParticle(ps);
    }

    IEnumerator waitAndQuake(int num)
    {
        WaitForSeconds ws = _isEvolved ? new WaitForSeconds(0.125f) :  new WaitForSeconds(0.25f);
        for (int i = 0; i < num; i++)
        {
            /*
            for (int j = 0; j < _numOfPrj + PlayerStatManager.instance.NumOfPrj; j++)
            {
                if (slimesInRange.Count == 0) break;
                
                Slime s = slimesInRange[Random.Range(0, slimesInRange.Count)];
                StartCoroutine(quake(s));
            }
            */
            if (slimesInRange.Count > 0)
            {
                Slime s = slimesInRange[Random.Range(0, slimesInRange.Count)];
                StartCoroutine(quake(s));
            }

            yield return ws;
        }
    }
    
    
    void removeErrors()
    {
        for (int i = 0; i < slimesInRange.Count; i++)
        {
            if ((slimesInRange[i].transform.position - transform.position).magnitude > 7)
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
