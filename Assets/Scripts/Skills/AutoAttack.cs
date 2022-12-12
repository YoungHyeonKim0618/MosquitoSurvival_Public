using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class AutoAttack : MonoBehaviour, AttackSkill
{
    /*
     * 기본 공격에 해당하는 스킬.
     * 짧은 쿨타임, 약한 데미지가 특징임.
     * 레벨업시 피해/공격 속도/ 관통 등이 올라감.
     */

    private int curSkillLevel = 0;


    //기본 공격 피해량.
    [SerializeField]
    private float _dmg = 4f;
    
    //기본 공격 주기.
    [SerializeField]
    private float _delay = 1;
    
    //투사체 속도.
    private float _spd = 15;
    //나가는 투사체 개수.
    private int _numOfPrj = 1;

    //관통 횟수.
    private int _penetration = 0;

    //사거리 내의 슬라임들.
    private List<Transform> slimesInRange = new List<Transform>();

    private bool _isEvolved = false;
    public void Evolve()
    {
        _isEvolved = true;
        StopCoroutine(wae);
        StartCoroutine(waitAndExecuteEvolved());
        
        tmpAmmo.text = tmpAmmo.text = $"{curAmmo} / {maxAmmo}";
    }

    private IEnumerator wae;
    
    
    
    //진화 재장전 ---
    private int maxAmmo = 30, curAmmo = 30;

    private float defaultReloadTime = 3;
    [SerializeField]
    private TextMeshProUGUI tmpAmmo;

    private void Awake()
    {
        
        wae = waitAndExecute();
    }

    IEnumerator waitAndReload()
    {
        int num = _numOfPrj + PlayerStatManager.instance.NumOfPrj;
        tmpAmmo.text = "Reloading...";
        yield return new WaitForSeconds(defaultReloadTime * PlayerStatManager.instance.GetCoolCoef());

        maxAmmo = 30 + 10 * num;
        curAmmo = maxAmmo;
        
    }
    
    // ------------
    
    
    private void Start()
    {
        //SetSkillLevel(0);
        
        StartCoroutine(wae);
        StartCoroutine(waitAndSetNearestSlime());
        StartCoroutine(waitAndRemoveErrors());


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

    public void UpgradeSkill()
    {
        
        
        SetSkillLevel(++curSkillLevel);
    }
    public void SetSkillLevel(int i)
    {
        i = Mathf.Clamp(i, 0, 10);
        curSkillLevel = i;

        switch (i)
        {
            case 0 :    //스킬을 삭제함.
                break;
            case 1 :
                _delay = 1f;
                _dmg = 4f;
                break;
            case 2 :    //딜레이 감소, 공격력 증가.
                _delay = 0.9f;
                _dmg = 5.6f;
                break;
            case 3 :    //관통 횟수 1 증가.
                _delay = 0.9f;
                _dmg = 5.6f;
                _penetration = 1;
                break;
            case 4 :    //딜레이 감소, 공격력 증가.
                _delay = 0.8f;
                _dmg = 7.2f;
                _penetration = 1;
                break;
            case 5 :    //투사체 개수 1 증가.
                _delay = 0.8f;
                _dmg = 7.2f;
                _penetration = 1;
                _numOfPrj = 2;
                break;
            case 6 :    //딜레이 감소.
                _delay = 0.7f;
                _dmg = 7.2f;
                _penetration = 1;
                _numOfPrj = 2;
                break;
            case 7 :    //투사체 개수 1 증가.
                _delay = 0.7f;
                _dmg = 7.2f;
                _penetration = 1;
                _numOfPrj = 3;
                break;
            case 8:     //관통 1 증가.
                _delay = 0.7f;
                _dmg = 7.2f;
                _penetration = 2;
                _numOfPrj = 3;
                break;
            case 9:     //딜레이감소, 공격력 증가
                _delay = 0.6f;
                _dmg = 8.8f;
                _penetration = 2;
                _numOfPrj = 3;
                break;
            case 10:    //투사체 증가
                _delay = 0.6f;
                _dmg = 8.8f;
                _penetration = 2;
                _numOfPrj = 4;
                break;
        }
    }

    [SerializeField] private AutoPrj _prjPrefab;
    [SerializeField] private AutoPrj _prjPrefabEvolved;
    private void execute()
    {
        if (slimesInRange.Count > 0)
        {
            Transform target = slimesInRange[0];
            for (int i = 0; i < _numOfPrj + PlayerStatManager.instance.NumOfPrj; i++)
            {
                StartCoroutine(waitAndRepeat(0.05f * i,target));
            }
        }
    }

    private bool isFiredOdd = false;
    void executeEvolved()
    {
        Transform target = slimesInRange[0];
        var position = transform.position;
        Vector2 dir = target.position - position;
        Vector2 r = Vector2.right * 0.175f;

        Vector2 p1 = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90) * r;
        if (isFiredOdd)
        {
            p1 = new Vector2(-p1.x, -p1.y);
            isFiredOdd = false;
        }
        else isFiredOdd = true;

        p1 += (Vector2)position;

        AutoPrj prj = ObjectPool.instance.PopAutoPrj(true);
        prj.transform.position = p1;
        prj.setPrj(_dmg * PlayerStatManager.instance.GetDmgCoef(),_spd,_penetration,target.transform.position - position);
    }

    private IEnumerator waitAndRepeat(float t,Transform target)
    {
        yield return new WaitForSeconds(t);
        if(!target) yield break;
        
        float error = Random.Range(-0.3f, 0.3f);
        var position = transform.position;
        AutoPrj prj = ObjectPool.instance.PopAutoPrj();
        prj.transform.position = position + error * Vector3.up;
        prj.setPrj(_dmg * PlayerStatManager.instance.GetDmgCoef(),_spd,_penetration,target.transform.position - position);
    }

    private IEnumerator waitAndExecute()
    {
        while (true)
        {
            float w = _delay * (100 - PlayerStatManager.instance.CooldownDecrement) / 100;
            yield return new WaitForSeconds(w);
            execute();
        }
    }

    private IEnumerator waitAndExecuteEvolved()
    {
        WaitForSeconds ws = new WaitForSeconds(0.15f);
        while (true)
        {
            yield return ws;
            if (slimesInRange.Count > 0)
            {
                if(curAmmo > 0)
                {
                    curAmmo--;
                    tmpAmmo.text = $"{curAmmo} / {maxAmmo}";
                    executeEvolved();

                    if (curAmmo == 0)
                    {
                        StartCoroutine(waitAndReload());
                    }
                }
            }
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
            if(slimesInRange[i] != null)
                if ((slimesInRange[i].position - transform.position).magnitude > 7)
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
