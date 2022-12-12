using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    //선택된 캐릭터 정보.
    public int CurCharacter = 0;
    
    
    private void Awake()
    {
        if (!instance)
            instance = this;
        
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// 캐릭터별로 스탯을 적용시킴.
    /// </summary>
    public void SetCharacterStat()
    {
        switch (CurCharacter)
        {
            case CharacterKey.CITIZEN:
                PlayerStatManager.instance.ExpAmount += 20;
                break;
            case CharacterKey.PEST_CONTROLLER:
                PlayerStatManager.instance.PrjSpeed += 15;
                break;
        }
    }

    /// <summary>
    /// 캐릭터 키를 받아 그 캐릭터의 고유 어트리뷰트 키를 반환함.
    /// 게임 씬에서 시작할 때 받아서 적용시킴.
    /// </summary>
    public int GetAttributeKey()
    {
        int k = 0;

        switch (CurCharacter)
        {
            case CharacterKey.CITIZEN:
                k = AttributeKey.EXPERIENCE;
                break;
            case CharacterKey.PEST_CONTROLLER:
                k = AttributeKey.ACCELERATION;
                break;
        }

        return k;
    }

    /// <summary>
    /// 캐릭터의 시작 스킬 키를 반환.
    /// </summary>
    public int GetSkillKey()
    {
        int k = 0;
        switch (CurCharacter)
        {
            case CharacterKey.CITIZEN:
                k = SkillKey.SKILL_ATTACK_AUTO;
                break;
            case CharacterKey.PEST_CONTROLLER:
                k = SkillKey.SKILL_ATTACK_SATELLITE;
                break;
        }

        return k;
    }
}

public static class CharacterKey
{
    public const int CITIZEN = 0;
    public const int PEST_CONTROLLER = 1;
}