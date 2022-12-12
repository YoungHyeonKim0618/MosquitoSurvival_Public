using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Satellite : MonoBehaviour, AttackSkill
{
    /*
     * 위성이 돌며 공격하는 스킬.
     * 위성이 도는 속도가 곧 데미지로 직결됨.
     * 레벨업시 피해/ 개수/ 도는 속도/ 크기 등이 결정됨.
     */

    [SerializeField] private GameObject satellitePrefab;
    [SerializeField] private SatellitePrjEvolved satelliteEvolvedPrefab;
    private List<Transform> satellitePrjs = new List<Transform>();

    private int curSkillLevel = 0;

    //위성과 플레이어의 거리
    [SerializeField] private float _distance = 1;
    //위성 크기
    [SerializeField] private float _size = 0.2f;
    //위성 피해량
    [SerializeField] private float _dmg = 5;
    //위성 공전 속도
    [SerializeField] private float _spd;

    public float getSpdCoef()
    {
        return _spd * 0.5f;
    }
    
    //위성 개수
    private int _numofPrj = 1;

    private Rigidbody2D _rigidbody;

    private bool _isEvolved = false;
    public void Evolve()
    {
        _isEvolved = true;
        arrangePrj();
    }
    
    private void Start()
    {
        arrangePrj();
        
        UnityEventManager.instance.OnNumOfPrjChanged.AddListener(arrangePrj);
        UnityEventManager.instance.OnSkillSizeChanged.AddListener(arrangePrj);
    }

    private void FixedUpdate()
    {
        float k = _isEvolved ? 0.5f : 1f;
        transform.eulerAngles += Vector3.back * (_spd * PlayerStatManager.instance.GetPrjSpdCoef() * 0.5f * k);
    }

    public void SetSkillLevel(int i)
    {
        i = Mathf.Clamp(i, 0, 7);
        curSkillLevel = i;

        switch (i)
        {
            case 1:
                _size = 0.2f;
                _dmg = 8;
                _spd = 2f;
                _numofPrj = 1;
                break;
            case 2: //위성 개수 증가.
                _size = 0.2f;
                _dmg = 8;
                _spd = 2f;
                _numofPrj = 2;
                break;
            case 3: //위성 회전 속도 증가..
                _size = 0.2f;
                _dmg = 8;
                _spd = 2.5f;
                _numofPrj = 2;
                break;
            case 4: //위성 크기 증가.
                _size = 0.26f;
                _dmg = 8;
                _spd = 2.5f;
                _numofPrj = 2;
                break;
            case 5: //공격력 증가.
                _size = 0.26f;
                _dmg = 9.6f;
                _spd = 2.5f;
                _numofPrj = 2;
                break;
            case 6: //위성 개수 증가.
                _size = 0.26f;
                _dmg = 9.6f;
                _spd = 2.5f;
                _numofPrj = 3;
                break;
            case 7: //위성 회전 속도 증가.
                _size = 0.26f;
                _dmg = 11.2f;
                _spd = 3f;
                _numofPrj = 3;
                break;
        }
        arrangePrj();
    }

    void arrangePrj()
    {
        //이전 위성들 삭제.
        int k = satellitePrjs.Count;
        for (int j = 0; j < k; j++)
        {
            var t = satellitePrjs[0];
            satellitePrjs.Remove(t);
            Destroy(t.gameObject);
        }

        if(!_isEvolved)
        {
            int n = _numofPrj + PlayerStatManager.instance.NumOfPrj;
            for (int i = 0; i < n; i++)
            {
                Vector2 v = Vector2.right * _distance;
                v = Quaternion.Euler(new Vector3(0, 0, (float) i / n * 360)) * v;
                GameObject s = Instantiate(satellitePrefab, transform.position + (Vector3) v, quaternion.identity,
                    transform);
                
                s.transform.localScale = new Vector2(1, 1) * _size * PlayerStatManager.instance.GetSizeCoef();
                satellitePrjs.Add(s.transform);
            }
        }
        else
        {
            int n =  (_numofPrj + PlayerStatManager.instance.NumOfPrj) + 3;
            for (int i = 0; i < n ; i++)
            {
                Vector2 v = Vector2.right * _distance;
                v = Quaternion.Euler(new Vector3(0, 0, (float) i / n * 360)) * v;
                SatellitePrjEvolved s = Instantiate(satelliteEvolvedPrefab, transform.position + (Vector3)v, quaternion.identity,
                    transform);

                s.InitRotate((float) i / n * 360);
                
                s.transform.localScale = new Vector2(1, 1) * _size * 0.625f;
                satellitePrjs.Add(s.transform);
            }
        }
    }

    //위성이 슬라임에 부딪혔을때 
    public void OnHitSlime(Slime target)
    {
        float k = _isEvolved ? 1.5f : 1f;
        target.setDamage(_dmg * k  * PlayerStatManager.instance.GetDmgCoef());
    }

    public void UpgradeSkill()
    {
        SetSkillLevel(++curSkillLevel);
    }

}
