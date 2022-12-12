using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public static class SlimeKey
{
    
    public const int MOSQUITO = 100;
    public const int BLUE_MOSQUITO = 101;
    public const int BOMB_MOSQUITO = 110;
}
public class SlimeSpawner : MonoBehaviour
{

    [SerializeField] private float minDistance, maxDistance;
    [SerializeField] private float spawnDelay;
    [SerializeField] private int spawnNumber;


    private float number_coef = 0, hp_coef = 0;

    private int _chanceMiniSlime = 0;
    private int _chanceBlueSlime = 0;

    [SerializeField] private ObjectPool _pool;


    [SerializeField] private Slime bombSlimePrefab;
    [SerializeField] private Slime goldSlimePrefab;


    [SerializeField] private List<SlimePattern> _slimePatterns = new List<SlimePattern>();

    private void Start()
    {
        StartCoroutine(waitAndSpawn());
    }

    private void spawnSlimes()
    {
        //현재 진행 시간에 따라 다름.
        int time = GameManager.instance.GetTimeMinute();

        //_chanceMiniSlime = Mathf.Clamp(40 - time * 2,20,40);
        _chanceMiniSlime = 0;
        _chanceBlueSlime = Mathf.Clamp((time - 1) * 5, 0, 50);
        if (time < 1)
        {
            number_coef = 1;
            hp_coef = 1;
        }
        else if (time < 3)
        {
            number_coef = 1.5f;
            hp_coef = 1.15f;
        }
        else
        {
            number_coef = 1.5f + 0.1f * time;
            //hp_coef = 1.15f * Mathf.Pow(1.15f, ((float)time - 3)/2);
            hp_coef = 1.2f + 0.025f * (((float) time - 3) * 1f);
        }

        for (int i = 0; i < spawnNumber * number_coef; i++)
        {
            float distance = Random.Range(minDistance, maxDistance);
            Vector2 dir = Vector2.right * distance;
            dir = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * dir;

            int r = Random.Range(0, 100);
            //미니 슬라임은 2마리씩 생성.
            if (r < _chanceMiniSlime)
            {
                Slime s1 = _pool.PopSlime(ObjectType.MINI_SLIME);
                Slime s2 = _pool.PopSlime(ObjectType.MINI_SLIME);
                
                s1.transform.position = PlayerStatManager.instance.transform.position + (Vector3)dir;
                s1.setHpCoef(hp_coef);
                s1.OnSpawn();
                
                s2.transform.position = PlayerStatManager.instance.transform.position + -(Vector3)dir;
                s2.setHpCoef(hp_coef);
                s2.OnSpawn();
            }
            else if (r < _chanceMiniSlime + _chanceBlueSlime)
            {
                Slime s1 = _pool.PopSlime(ObjectType.BLUE_SLIME);
                
                s1.transform.position = PlayerStatManager.instance.transform.position + (Vector3)dir;
                s1.setHpCoef(hp_coef);
                s1.OnSpawn();
            }
            else
            {
                Slime s1 = null;
                if (GameManager.instance.GetTimeMinute() > 3 && ConstMethods.GetRandomResult(1))
                {
                    s1 = Instantiate(goldSlimePrefab);
                }
                else
                {
                    s1 = _pool.PopSlime(ObjectType.SLIME);
                }
                
                s1.transform.position = PlayerStatManager.instance.transform.position + (Vector3)dir;
                s1.setHpCoef(hp_coef);
                s1.OnSpawn();
            }
            
        }

        if (time > 4 && ConstMethods.GetRandomResult(15 + (time - 4) * 5))
        {
            float distance = Random.Range(minDistance, maxDistance);
            Vector2 dir = Vector2.right * distance;
            dir = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * dir;

            Slime go = Instantiate(bombSlimePrefab, dir, Quaternion.identity, null);
            go.setHpCoef(hp_coef);
            go.OnSpawn();
        }
        
        
        if (time > 5 && ConstMethods.GetRandomResult(10))
        {
            spawnFromPattern(_slimePatterns[Random.Range(0,_slimePatterns.Count)]);
        }
    }

    public Slime GetSlimePrefabByKey(int key)
    {
        Slime slime = null;
        switch (key)
        {
            case SlimeKey.MOSQUITO:
                break;
            case SlimeKey.BLUE_MOSQUITO:
                break;
            case SlimeKey.BOMB_MOSQUITO:
                break;
        }

        return slime;
    }

    void spawnFromPattern(SlimePattern pattern)
    {
        for (int i = 0; i < pattern.Positions.Count; i++)
        {
            Vector2 pos = pattern.SpawnPoint + pattern.Positions[i];

            Slime go = Instantiate(GetSlimePrefabByKey(pattern.SlimeKeys[i]), pos, Quaternion.identity, null);
            go.setHpCoef(hp_coef);
            go.OnSpawn();
        }
    }

    IEnumerator waitAndSpawn()
    {
        for (int i = 0; i < 100000; i++)
        {
            if (Slime.numOfSlimes < 200)
            {
                spawnSlimes();
            }
            yield return new WaitForSeconds(spawnDelay );
        }
    }
}
