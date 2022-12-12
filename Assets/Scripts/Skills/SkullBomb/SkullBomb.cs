using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
public class SkullBomb : MonoBehaviour, AttackSkill
{
    /*
     * 일정 시간마다 적에게 닿을 시 폭발하는 해골 폭탄을 소환한다.
     * 적이 충분하다면 개수만큼 소환하고, 아니라면 적의 수만큼만 소환함.
     * 투사체 속도/ 공격력/ 쿨타임/ 투사체 개수 영향 받음.
     *
     */

    //폭발 피해량.
    [SerializeField]
    private float _dmg;

    [SerializeField]
    private float _delay = 4;
    //폭발 반경.
    [SerializeField]
    private float _range;
    //발사체 속도.
    [SerializeField]
    private float _spd;
    //발사 개수.
    [SerializeField]
    private float _numOfPrj;


    [SerializeField]
    private List<Transform> slimesInRange = new List<Transform>();


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

    private bool _isEvolved = false;
    public void Evolve()
    {
        _isEvolved = true;
    }
    private void Start()
    {
        StartCoroutine(waitAndSetNearestSlime());
        //StartCoroutine(waitAndExecute());
        StartCoroutine(waitAndCompareCondition());
        StartCoroutine(waitAndRemoveErrors());
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
        if(other.CompareTag("Slime"))
        {
            slimesInRange.Add(other.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    { 
        if(other.CompareTag("Slime"))
            slimesInRange.Remove(other.transform);
    }

    private int curSkillLevel = 0;
    public void UpgradeSkill()
    {
        setSkillLevel(++curSkillLevel);
    }

    private void setSkillLevel(int i)
    {
        i = Mathf.Clamp(i, 0, 7);
        curSkillLevel = i;


        switch (i)
        {
            case 1: //
                _dmg = 20;
                _delay = 6;
                _range = 2;
                _spd = 3.5f;
                _numOfPrj = 1;
                break;
            case 2: //쿨타임 감소.
                _dmg = 20;
                _delay = 5.4f;
                _range = 2;
                _spd = 3.5f;
                _numOfPrj = 1;
                break;
            case 3: //반경, 피해 증가.
                _dmg = 25;
                _delay = 5.4f;
                _range = 2.4f;
                _spd = 3.5f;
                _numOfPrj = 1;
                break;
            case 4: //쿨타임 감소.
                _dmg = 25;
                _delay = 4.8f;
                _range = 2.4f;
                _spd = 3.5f;
                _numOfPrj = 1;
                break;
            case 5: //반경, 피해 증가.
                _dmg = 30;
                _delay = 4.8f;
                _range = 2.8f;
                _spd = 3.5f;
                _numOfPrj = 1;
                break;
            case 6: //투사체 개수 증가.
                _dmg = 30;
                _delay = 4.8f;
                _range = 2.8f;
                _spd = 3.5f;
                _numOfPrj = 2;
                break;
            case 7: //반경, 피해 증가.
                _dmg = 35;
                _delay = 4.8f;
                _range = 3.2f;
                _spd = 3.5f;
                _numOfPrj = 2;
                break;
        }
    }

    [SerializeField] private SkullBombPrj _prjPrefab;
    void execute()
    {

        //NumofPrj와 SlimsinRange.Count 중 작은 값만큼 반복.
        for (int i = 0; i < Mathf.Min(slimesInRange.Count, _numOfPrj + PlayerStatManager.instance.NumOfPrj); i++)
        {
            var target = slimesInRange[i];
            SkullBombPrj prj = Instantiate(_prjPrefab, transform.position, Quaternion.identity, null);
            if(!_isEvolved)
                prj.setPrj(_spd * PlayerStatManager.instance.GetPrjSpdCoef(), target.transform.position - transform.position,this);
            else
            {
                prj.setPrj(_spd * PlayerStatManager.instance.GetPrjSpdCoef(), target.transform.position - transform.position,this,2);
            }
        }
    }

    /*
    IEnumerator waitAndExecute()
    {
        while (true)
        {
            float w = _delay * (100 - PlayerStatManager.instance.CooldownDecrement) / 100;
            yield return new WaitForSeconds(w);
            execute();
        }
    }
    */

    [SerializeField] private SkullBombExploded explodedPrefab;
    //폭발을 담당함.
    //위치에 폭발 프리팹을 생성함. 
    public void Explode(Vector2 pos)
    {
        if(!_isEvolved)
        {
            SkullBombExploded exploded = Instantiate(explodedPrefab, pos, quaternion.identity, null);
            exploded.transform.localScale =
                new Vector2(1, 1) * _range * PlayerStatManager.instance.GetSizeCoef() * 0.5f;
            exploded.SetDmg(_dmg * PlayerStatManager.instance.GetDmgCoef());
        }
        else
        {
            SkullBombExploded exploded = Instantiate(explodedPrefab, pos, quaternion.identity, null);
            exploded.GetComponent<SpriteRenderer>().color = Color.magenta;
            exploded.transform.localScale =
                new Vector2(1, 1) * _range * PlayerStatManager.instance.GetSizeCoef() * 0.5f * 0.875f;
            exploded.SetDmg(_dmg * PlayerStatManager.instance.GetDmgCoef());
        }
    }
    
    
    
    //가장 가까운 슬라임을 slimesInRange[0]으로 지정해줌.
    private void setNearestSlime()
    {
        int index = 0;
        if (slimesInRange.Count > 1)
        {
            Transform nearest = slimesInRange[0];
            for (int i = 0; i < Mathf.Clamp(slimesInRange.Count, 0, 20); i++)
            {
                if ((slimesInRange[i].position - transform.position).magnitude <
                    (nearest.position - transform.position).magnitude)
                {
                    nearest = slimesInRange[i];
                    index = i;
                }
            }
            if (nearest != slimesInRange[0])
            {
                Transform t = slimesInRange[0];
                slimesInRange[0] = nearest;
                slimesInRange[index] = t;
            }
        }
    }

    private IEnumerator waitAndSetNearestSlime()
    {
        WaitForSeconds ws = new WaitForSeconds(0.5f);
        while (true)
        {
            setNearestSlime();
            yield return ws;
        }
    }
    void removeErrors()
    {
        for (int i = 0; i < slimesInRange.Count; i++)
        {
            if ((slimesInRange[i].transform.position - transform.position).magnitude > 6)
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
