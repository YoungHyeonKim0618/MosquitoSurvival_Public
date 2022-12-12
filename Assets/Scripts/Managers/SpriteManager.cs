using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;


    [SerializeField]
    private List<Sprite> attributeSprites = new List<Sprite>();

    [SerializeField] private List<Sprite> attSkillSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> statSprites = new List<Sprite>();
    private void Awake()
    {
        if (!instance)
            instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Sprite GetSpriteByKey(int key)
    {
        Sprite sprite = null;
        switch (key)
        {
            
            case SpriteKey.ATTRIBUTE_EXPERIENCE:
                sprite = attributeSprites[0];
                break;
            case SpriteKey.ATTRIBUTE_ACCELERATION:
                sprite = attributeSprites[1];
                break;
            case SpriteKey.ATTRIBUTE_RAMPAGE:
                sprite = attributeSprites[2];
                break;
            case SpriteKey.ATTRIBUTE_HASTE:
                sprite = attributeSprites[3];
                break;
        }
        return sprite;
    }
    
    public Sprite GetSpriteByAttKey(int att)
    {
        Sprite k = null;
        switch (att)
        {
            case AttributeKey.EXPERIENCE:
                k = GetSpriteByKey(SpriteKey.ATTRIBUTE_EXPERIENCE);
                break;
            case AttributeKey.ACCELERATION:
                k = GetSpriteByKey(SpriteKey.ATTRIBUTE_ACCELERATION);
                break;
            case AttributeKey.RAMPAGE:
                k = GetSpriteByKey(SpriteKey.ATTRIBUTE_RAMPAGE);
                break;
            case AttributeKey.HASTE:
                k = GetSpriteByKey(SpriteKey.ATTRIBUTE_HASTE);
                break;
        }

        return k;
    }

    public Sprite GetSpriteBySkill(int skill)
    {
        Sprite k = null;

        switch (skill)
        {
            case SkillKey.SKILL_ATTACK_AUTO:
                k = attSkillSprites[0];
                break;
            case SkillKey.SKILL_ATTACK_SATELLITE:
                k = attSkillSprites[1];
                break;
            case SkillKey.SKILL_ATTACK_TOXIC:
                k = attSkillSprites[2];
                break;
            case SkillKey.SKILL_ATTACK_SKULL:
                k = attSkillSprites[3];
                break;
            case SkillKey.SKILL_ATTACK_VISION:
                k = attSkillSprites[4];
                break;
            case SkillKey.SKILL_ATTACK_SLASH:
                k = attSkillSprites[5];
                break;
            case SkillKey.SKILL_ATTACK_GATE:
                k = attSkillSprites[6];
                break;
            case SkillKey.SKILL_ATTACK_DAEDALUS:
                k = attSkillSprites[7];
                break;
            case SkillKey.SKILL_ATTACK_QUAKE:
                k = attSkillSprites[8];
                break;
            
            
            case SkillKey.STAT_DMG:
                k = statSprites[0];
                break;
            case SkillKey.STAT_SPD:
                k = statSprites[1];
                break;
            case SkillKey.STAT_PRJ_NUM:
                k = statSprites[2];
                break;
            case SkillKey.STAT_PRJ_SPD:
                k = statSprites[3];
                break;
            case SkillKey.STAT_COOL:
                k = statSprites[4];
                break;
            case SkillKey.STAT_SIZE:
                k = statSprites[5];
                break;
            case SkillKey.STAT_EXP_COEF:
                k = statSprites[6];
                break;
            case SkillKey.STAT_OBTAIN_RANGE:
                k = statSprites[7];
                break;
            case SkillKey.STAT_RECOVER:
                k = statSprites[8];
                break;
            
        }

        return k;
    }
}

public static class SpriteKey
{
    public const int ATTRIBUTE_EXPERIENCE = 101;
    public const int ATTRIBUTE_ACCELERATION = 102;
    public const int ATTRIBUTE_RAMPAGE = 103;
    public const int ATTRIBUTE_HASTE = 104;
}