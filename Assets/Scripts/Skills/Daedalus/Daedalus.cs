using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daedalus : MonoBehaviour, AttackSkill
{
    /*
     * 지속시간동안 적을 만나면 튕기는 강한 투사체를 날린다.
     * 적을 튕기면 (우상단/우하단/좌상단/좌하단)중 1개 방향으로 날아간다.
     * 다만 현재 진행 방향/ 그 반대 방향은 안됨.
     * 화면 바로 밖에 투명한 벽에 부딪혀도 튕김.
     */
    
    //피해량
    private float _dmg = 10.5f;
    //투사체 속도
    private float _spd = 5;
    //투사체 개수
    private int _numOfPrj = 1;
    //쿨타임
    private float _delay = 9f;
    //지속시간
    private float _life = 5f;
    
    
    private int curSkillLevel = 0;

    private bool _isEvolved = false;
    public void Evolve()
    {
        _isEvolved = true;
    }
    private void Start()
    {
        StartCoroutine(waitAndExecute());
    }

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
            case 1:
                _dmg = 10f;
                _spd = 4;
                _numOfPrj = 1;
                _delay = 9;
                _life = 5;
                break;
            case 2: //개수 증가
                _dmg = 10f;
                _spd = 4;
                _numOfPrj = 2;
                _delay = 9;
                _life = 5;
                break;
            case 3: //속도, 지속시간 증가
                _dmg = 10;
                _spd = 4.8f;
                _numOfPrj = 2;
                _delay = 9;
                _life = 5.5f;
                break;
            case 4: //공격력 증가, 쿨타임 감소
                _dmg = 12f;
                _spd = 4.8f;
                _numOfPrj = 2;
                _delay = 8.1f;
                _life = 5.5f;
                break;
            case 5: //속도, 지속시간 증가
                _dmg = 12f;
                _spd = 5.6f;
                _numOfPrj = 2;
                _delay = 8.1f;
                _life = 6f;
                break;
            case 6: //개수 증가
                _dmg = 12f;
                _spd = 5.6f;
                _numOfPrj = 3;
                _delay = 8.1f;
                _life = 6f;
                break;
            case 7 : //속도, 공격력 증가
                _dmg = 14f;
                _spd = 6.4f;
                _numOfPrj = 3;
                _delay = 8.1f;
                _life = 6f;
                break;
        }
        
        
    }
    [SerializeField] DaedalusPrj _prjPrefab;
    [SerializeField] private DaedalusPrj _prjEvolvedPrefab;

    void execute()
    {
        StartCoroutine(waitAndRepeat());
    }

    IEnumerator waitAndRepeat()
    {
        if (!_isEvolved)
        {
            for (int i = 0; i < _numOfPrj + PlayerStatManager.instance.NumOfPrj; i++)
            {
                DaedalusPrj prj = Instantiate(_prjPrefab, transform.position, Quaternion.identity, null);
                prj.InitPrj(_dmg * PlayerStatManager.instance.GetDmgCoef(),
                    _spd * 1.5f * PlayerStatManager.instance.GetPrjSpdCoef(), _life);
                prj.transform.localScale *= PlayerStatManager.instance.GetSizeCoef();

                yield return new WaitForSeconds(0.25f);
            }
        }
        else
        {
            DaedalusPrj prj = Instantiate(_prjEvolvedPrefab, transform.position, Quaternion.identity, null);
            prj.InitPrj(_dmg * PlayerStatManager.instance.GetDmgCoef(),_spd * 1.5f * PlayerStatManager.instance.GetPrjSpdCoef(),_life,true);
            prj.transform.localScale *= PlayerStatManager.instance.GetSizeCoef();
        }
    }

    IEnumerator waitAndExecute()
    {
        while (true)
        {
            float w = _delay * PlayerStatManager.instance.GetCoolCoef();
            yield return new WaitForSeconds(w);
            execute();
        }
    }
}
