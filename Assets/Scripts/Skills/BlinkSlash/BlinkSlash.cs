using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlinkSlash : MonoBehaviour, AttackSkill
{
    /*
     * 게임을 멈추고 플레이어가 주변의 슬라임들을 연속해서 벤다.
     * 횟수가 모두 소진되거나 근처에 적이 없을때까지,
     * 이동 -> 검기 생성 -> 반복.
     * 시간을 멈추므로 코루틴은 WaitForSecondsRealTime을 사용한다.
     *
     * 레벨업 보상시 timescale을 조정하므로
     * 이 스킬 사용중엔 레벨업 보상 선택 불가능하게 한다.
     */
    
    //검기 피해량.
    private float _dmg = 8; 
    
    //검기 크기.
    private float _size = 1;
    
    //슬라임을 베는 횟수.
    private int _numSlash = 2;
    
    //쿨타임
    private float _delay = 8;

    [SerializeField] private List<Transform> slimesInRange = new List<Transform>();

    [SerializeField] private GameObject _trailObject;
    
    
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
        //StartCoroutine(waitAndExecute());
        StartCoroutine(waitAndCompareCondition());
        StartCoroutine(waitAndRemoveErrors());
        _trailObject.SetActive(false);
        
        _dark.color = Color.clear;
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
                _dmg = 5;
                _size = 1;
                _numSlash = 2;
                _delay = 10;
                break;
            case 2: //쿨타임 감소
                _dmg = 5;
                _size = 1;
                _numSlash = 2;
                _delay = 9f;
                break;
            case 3: //베는 횟수 증가
                _dmg = 5;
                _size = 1;
                _numSlash = 3;
                _delay = 9f;
                break;
            case 4: //데미지 증가
                _dmg = 6;
                _size = 1;
                _numSlash = 3;
                _delay = 9f;
                break;
            case 5: //쿨타임 감소
                _dmg = 6;
                _size = 1;
                _numSlash = 3;
                _delay = 8f;
                break;
            case 6: //데미지 증가
                _dmg = 7;
                _size = 1;
                _numSlash = 3;
                _delay = 8f;
                break;
            case 7: //횟수 증가
                _dmg = 7;
                _size = 1;
                _numSlash = 4;
                _delay = 8f;
                break;
        }
    }

    void execute()
    {
        //시간 멈추고 레벨업, 어트리뷰트 비활성화
        if(slimesInRange.Count > 0 )
            StartCoroutine(waitAndSlash());
    }

    [SerializeField] private SpriteRenderer _dark;
    IEnumerator waitAndSlash()
    {
        Vector3 originPos = transform.position;
        _trailObject.SetActive(true);

        if(!_isEvolved)
        {
            for (int i = 0; i < 2 * _numSlash + PlayerStatManager.instance.NumOfPrj; i++)
            {
                PlayerController.instance.AddGhostTime(0.5f);
                if (slimesInRange.Count == 0) break;
                yield return new WaitForSecondsRealtime(0.3f);
                slash();
            }
            PlayerController.instance.AddGhostTime(0.3f);
            PlayerController.instance.GetPlayerRb().MovePosition(originPos);
        }
        else
        {
            AttributeManager.instance.SetActiveButton(false);
            RewardManager.instance.MovePointButton(true);
            GameManager.instance.SetPauseButton(true);
        
            _dark.DOFade(0.5f, 0.5f);
            yield return new WaitForSeconds(0.45f);
            Time.timeScale = 0.3f;
            for (int i = 0; i < (3 * _numSlash + PlayerStatManager.instance.NumOfPrj); i++)
            {
                PlayerController.instance.AddGhostTime(0.2f);
                if (slimesInRange.Count == 0) break;
                yield return new WaitForSecondsRealtime(0.15f);
                slash();
            }
            
            PlayerController.instance.AddGhostTime(0.875f);
            PlayerController.instance.GetPlayerRb().MovePosition(originPos);
            GameManager.instance.SetPauseButton(false);
            _dark.DOFade(0, 0.5f).SetUpdate(true);
            yield return new WaitForSecondsRealtime(0.5f);
            Time.timeScale = 1;
            AttributeManager.instance.SetActiveButton(true);
            RewardManager.instance.MovePointButton(false);
        }

        _trailObject.SetActive(false);
        
    }

    [SerializeField] private BlinkSlashPrj _prjPrefab;

    void slash()
    {
        if (slimesInRange.Count == 0) return;
        
        int r = Random.Range(0, slimesInRange.Count);
        Transform t = slimesInRange[r];
        int angle = Random.Range(0, 360);
        
        PlayerController.instance.GetPlayerRb().MovePosition(t.position);

        if(!_isEvolved)
        {
            BlinkSlashPrj prj = Instantiate(_prjPrefab, t.position, Quaternion.identity, null);
            prj.transform.Rotate(Vector3.forward, angle);
            prj.transform.localScale *= (_size * PlayerStatManager.instance.GetSizeCoef() * 0.9f);
            prj.SetDamage(_dmg * PlayerStatManager.instance.GetDmgCoef());
        }
        else
        {
            int tempAngle = angle - 90;
            
            BlinkSlashPrj prj = Instantiate(_prjPrefab, t.position, Quaternion.identity, null);
            prj.SetColor(new Color(1,0,0.5707547f,1));
            prj.transform.Rotate(Vector3.forward, angle);
            prj.transform.localScale *= (_size * PlayerStatManager.instance.GetSizeCoef() * 2f);
            prj.SetDamage(_dmg * PlayerStatManager.instance.GetDmgCoef());
            
        }
        
    }

    /*
    IEnumerator waitAndExecute()
    {
        while (true)
        {
            float w = _delay * PlayerStatManager.instance.GetCoolCoef();
            yield return new WaitForSeconds(w);
            execute();
        }
    }
    */

    void removeErrors()
    {
        for (int i = 0; i < slimesInRange.Count; i++)
        {
            if ((slimesInRange[i].transform.position - transform.position).magnitude > 4)
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
