using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlashVision : MonoBehaviour, AttackSkill 
{
    /*
     * 일정 시간마다 짧은 딜레이 후 긴 광선을 발사한다.
     * 방향은 랜덤이며, 개수만큼 다른 방향으로 발사함.
     * 공격력/ 쿨타임/ 투사체 개수/ 크기 영향 받음.
     *
     *
     * 진화시 레이저가 하나로 통합되고, 크기가 매우 커지며, 피해량도 엄청나진다.
     */

    [SerializeField] private float _dmg = 0;
    [SerializeField]
    private float _delay = 0;
    [SerializeField]
    private float _size  = 0;
    [SerializeField]
    private float _numOfPrj = 0;

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

    [SerializeField] private GameObject delaySprite;
    [SerializeField] private GameObject visionSprite;

    void execute()
    {
        if(!_isEvolved)
        {
            for (int i = 0; i < _numOfPrj + PlayerStatManager.instance.NumOfPrj; i++)
            {
                float r = Random.Range(0, 360f);

                GameObject go = Instantiate(delaySprite, transform.position, Quaternion.identity,
                    PlayerStatManager.instance.transform);
                go.transform.Rotate(Vector3.forward, r);
                go.transform.DOScaleY(0, 0.8f);
                Destroy(go, 0.875f);

                StartCoroutine(waitAndVision(r));
            }
        }
        else
        {
            float r = Random.Range(0, 360f);

            GameObject go = Instantiate(delaySprite, transform.position, Quaternion.identity,
                PlayerStatManager.instance.transform);
            go.transform.Rotate(Vector3.forward, r);
            go.transform.DOScaleY(0, 1.5f);
            Destroy(go, 1.625f);

            StartCoroutine(waitAndVisionEvolved(r));
        }
    }

    IEnumerator waitAndVision(float angle)
    {
        yield return new WaitForSeconds(0.8f);
        GameObject g = Instantiate(visionSprite, transform.position, Quaternion.identity, PlayerStatManager.instance.transform);
        g.transform.Rotate(Vector3.forward,angle);
        FlashVisionPrj go = g.GetComponentInChildren<FlashVisionPrj>();
        go.SetDmg(_dmg * PlayerStatManager.instance.GetDmgCoef());
        g.transform.localScale *= (_size * PlayerStatManager.instance.GetSizeCoef() * 0.8f);
        g.GetComponentInChildren<SpriteRenderer>().DOFade(0, 0.3f);
        Destroy(g.gameObject,0.625f);
    }
    IEnumerator waitAndVisionEvolved(float angle)
    {
        yield return new WaitForSeconds(1.5f);
        GameObject g = Instantiate(visionSprite, transform.position, Quaternion.identity, PlayerStatManager.instance.transform);
        g.transform.Rotate(Vector3.forward,angle);
        FlashVisionPrj go = g.GetComponentInChildren<FlashVisionPrj>();
        SpriteRenderer sr = g.transform.GetChild(1).GetComponent<SpriteRenderer>();
        go.SetDmg(_dmg * PlayerStatManager.instance.GetDmgCoef() * _numOfPrj);
        g.transform.localScale *= (_size * PlayerStatManager.instance.GetSizeCoef() * 1.5f * (1+ 0.4f * _numOfPrj));

        Destroy(g.gameObject,1.125f);
        
        sr.color = new Color(1,0,0.5707547f,1);
        yield return new WaitForSeconds(0.7f);
        sr.DOFade(0, 0.3f);
    }

    IEnumerator waitAndExecute()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if(!_isEvolved)
                yield return new WaitForSeconds(_delay * PlayerStatManager.instance.GetCoolCoef());
            else
            {
                yield return new WaitForSeconds(_delay * PlayerStatManager.instance.GetCoolCoef() * 1.5f);
            }
            execute();
        }
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
                _dmg = 20;
                _delay = 6;
                _size = 1;
                _numOfPrj = 1;
                break;
            case 2://범위, 피해 증가
                _dmg = 24;
                _delay = 6;
                _size = 1.2f;
                _numOfPrj = 1;
                break;
            case 3://쿨타임 감소
                _dmg = 24;
                _delay = 5.4f;
                _size = 1.2f;
                _numOfPrj = 1;
                break;
            case 4://범위, 피해 증가
                _dmg = 28;
                _delay = 5.4f;
                _size = 1.4f;
                _numOfPrj = 1;
                break;
            case 5://쿨타임 감소
                _dmg = 28;
                _delay = 4.8f;
                _size = 1.4f;
                _numOfPrj = 1;
                break;
            case 6://개수 증가
                _dmg = 28;
                _delay = 4.8f;
                _size = 1.4f;
                _numOfPrj = 2;
                break;
            case 7://범위, 피해 증가
                _dmg = 32;
                _delay = 4.8f;
                _size = 1.6f;
                _numOfPrj = 2;
                break;
        }
    }
}
