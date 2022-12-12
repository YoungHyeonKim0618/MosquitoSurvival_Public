using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public string GetStringByKey(int key)
    {
        string t = "";
        switch (key)
        {
            case -1:
                t = "ERROR";
                break;
            
            case TextKeys.UI_MAIN_START_GAME:
                t = "Start Game";
                break;
            case TextKeys.UI_MAIN_UPGRADE:
                t = "Upgrades";
                break;
            
            case TextKeys.UI_MAX_LEVEL:
                t = "Max Level.";
                break;
            case TextKeys.UI_UPGRADE:
                t = "Upgrades";
                break;
            case TextKeys.UI_NOT_SELECTED:
                t = "No skill is selected.";
                break;
            

            case TextKeys.CHOICE_AUTO_NAME:
                t = "Magical arrow";
                break;
            case TextKeys.CHOICE_AUTO_OBTAIN:
                t = "New!\nFires an arrow to nearby enemy rapidly.";
                break;
            case TextKeys.CHOICE_AUTO_LVL2_DESC:
                t = "Cooldown - 10%.\nDamage + 40%.";
                break;
            case TextKeys.CHOICE_AUTO_LVL3_DESC:
                t = "Penetration + 1.";
                break;
            case TextKeys.CHOICE_AUTO_LVL4_DESC:
                t = "Cooldown - 10%.\nDamage + 40%.";
                break;
            case TextKeys.CHOICE_AUTO_LVL5_DESC:
                t = "Number of projectiles + 1.";
                break;
            case TextKeys.CHOICE_AUTO_LVL6_DESC:
                t = "Cooldown - 10%.";
                break;
            case TextKeys.CHOICE_AUTO_LVL7_DESC:
                t = "Number of projectiles + 1.";
                break;
            case TextKeys.CHOICE_AUTO_LVL8_DESC:
                t = "Penetration + 1.";
                break;
            case TextKeys.CHOICE_AUTO_LVL9_DESC:
                t = "Cooldown - 10%.\nDamage + 40%.";
                break;
            case TextKeys.CHOICE_AUTO_LVL10_DESC:
                t = "Number of projectiles + 1.";
                break;

            case TextKeys.CHOICE_SATELLITE_NAME:
                t = "Satellite";
                break;
            case TextKeys.CHOICE_SATELLITE_OBTAIN:
                t = "New!\nSummons satellites that orbit around you.";
                break;
            case TextKeys.CHOICE_SATELLITE_LVL2_DESC:
                t = "Satellite + 1.";
                break;
            case TextKeys.CHOICE_SATELLITE_LVL3_DESC:
                t = "Orbital speed + 25%.";
                break;
            case TextKeys.CHOICE_SATELLITE_LVL4_DESC:
                t = "Satellite size + 30%.";
                break;
            case TextKeys.CHOICE_SATELLITE_LVL5_DESC:
                t = "Satellite damage + 20%.";
                break;
            case TextKeys.CHOICE_SATELLITE_LVL6_DESC:
                t = "Satellite + 1.";
                break;
            case TextKeys.CHOICE_SATELLITE_LVL7_DESC:
                t = "Satellite damage + 20%.\nOrbital speed + 25%.";
                break;

            case TextKeys.CHOICE_TOXIC_NAME:
                t = "Spray";
                break;
            case TextKeys.CHOICE_TOXIC_OBTAIN:
                t = "New!\nDeals damage to nearby enemies continuously.";
                break;
            case TextKeys.CHOICE_TOXIC_LVL2_DESC:
                t = "Range + 25%.";
                break;
            case TextKeys.CHOICE_TOXIC_LVL3_DESC:
                t = "Attack delay - 20%.";
                break;
            case TextKeys.CHOICE_TOXIC_LVL4_DESC:
                t = "Damage + 20%.";
                break;
            case TextKeys.CHOICE_TOXIC_LVL5_DESC:
                t = "Range + 25%.";
                break;
            case TextKeys.CHOICE_TOXIC_LVL6_DESC:
                t = "Damage + 20%.";
                break;
            case TextKeys.CHOICE_TOXIC_LVL7_DESC:
                t = "Range + 25%.";
                break;

            case TextKeys.CHOICE_SKULL_NAME:
                break;
            case TextKeys.CHOICE_SKULL_OBTAIN:
                t = "New!\nThrow a bomb that explodes when hit enemy.";
                break;
            case TextKeys.CHOICE_SKULL_LVL2_DESC:
                t = "Cooldown - 10%";
                break;
            case TextKeys.CHOICE_SKULL_LVL3_DESC:
                t = "Explosion Range + 20%.\nDamage + 25%.";
                break;
            case TextKeys.CHOICE_SKULL_LVL4_DESC:
                t = "Cooldown - 10%";
                break;
            case TextKeys.CHOICE_SKULL_LVL5_DESC:
                t = "Explosion Range + 20%.\nDamage + 25%.";
                break;
            case TextKeys.CHOICE_SKULL_LVL6_DESC:
                t = "Number of bomb + 1.";
                break;
            case TextKeys.CHOICE_SKULL_LVL7_DESC:
                t = "Explosion Range + 20%.\nDamage + 25%.";
                break;

            case TextKeys.CHOICE_VISION_NAME:
                t = "";
                break;
            case TextKeys.CHOICE_VISION_OBTAIN:
                t = "New!\nShoot a laser that damages all the enemies through.";
                break;
            case TextKeys.CHOICE_VISION_LVL2_DESC:
                t = "Laser Size + 20%.\nDamage + 20%.";
                break;
            case TextKeys.CHOICE_VISION_LVL3_DESC:
                t = "Cooldown - 10%.";
                break;
            case TextKeys.CHOICE_VISION_LVL4_DESC:
                t = "Laser Size + 20%.\nDamage + 20%.";
                break;
            case TextKeys.CHOICE_VISION_LVL5_DESC:
                t = "Cooldown - 10%.";
                break;
            case TextKeys.CHOICE_VISION_LVL6_DESC:
                t = "Number of laser + 1.";
                break;
            case TextKeys.CHOICE_VISION_LVL7_DESC:
                t = "Laser Size + 20%.\nDamage + 20%.";
                break;
            
            
            case TextKeys.CHOICE_SLASH_NAME:
                t = "";
                break;
            case TextKeys.CHOICE_SLASH_OBTAIN:
                t = "New!\nBlink and slash nearby enemies.";
                break;
            case TextKeys.CHOICE_SLASH_LVL2_DESC:
                t = "Cooldown - 10%.";
                break;
            case TextKeys.CHOICE_SLASH_LVL3_DESC:
                t = "Max number blinking + 2.";
                break;
            case TextKeys.CHOICE_SLASH_LVL4_DESC:
                t = "nDamage + 20%.";
                break;
            case TextKeys.CHOICE_SLASH_LVL5_DESC:
                t = "Cooldown - 10%.";
                break;
            case TextKeys.CHOICE_SLASH_LVL6_DESC:
                t = "nDamage + 20%.";
                break;
            case TextKeys.CHOICE_SLASH_LVL7_DESC:
                t = "Max number blinking + 2.";
                break;


            case TextKeys.CHOICE_DAEDALUS_NAME:
                t = "";
                break;
            case TextKeys.CHOICE_DAEDALUS_OBTAIN:
                t = "New!\nSummon a spear that bounds when hit enemy.";
                break;
            case TextKeys.CHOICE_DAEDALUS_LVL2_DESC:
                t = "Number of spear + 1.";
                break;
            case TextKeys.CHOICE_DAEDALUS_LVL3_DESC:
                t = "Spear speed + 20%.\nDuration + 10%.";
                break;
            case TextKeys.CHOICE_DAEDALUS_LVL4_DESC:
                t = "Damage + 20%.\nCooldown - 10%.";
                break;
            case TextKeys.CHOICE_DAEDALUS_LVL5_DESC:
                t = "Spear speed + 20%.\nnDuration + 10%.";
                break;
            case TextKeys.CHOICE_DAEDALUS_LVL6_DESC:
                t = "Number of spear + 1.";
                break;
            case TextKeys.CHOICE_DAEDALUS_LVL7_DESC:
                t = "Spear speed + 20%.\nDamage + 20%.";
                break;
            
            case TextKeys.CHOICE_QUAKE_NAME:
                t = "";
                break;
            case TextKeys.CHOICE_QUAKE_OBTAIN:
                t = "New!\nHit random nearby enemy rapidly.";
                break;
            case TextKeys.CHOICE_QUAKE_LVL2_DESC:
                t = "Number of quake + 4.";
                break;
            case TextKeys.CHOICE_QUAKE_LVL3_DESC:
                t = "Cooldown - 10%.";
                break;
            case TextKeys.CHOICE_QUAKE_LVL4_DESC:
                t = "Damage + 20%.";
                break;
            case TextKeys.CHOICE_QUAKE_LVL5_DESC:
                t = "Number of quake + 4.";
                break;
            case TextKeys.CHOICE_QUAKE_LVL6_DESC:
                t = "Cooldown - 10%.";
                break;
            case TextKeys.CHOICE_QUAKE_LVL7_DESC:
                t = "Number of quake + 8.";
                break;
            
            case TextKeys.CHOICE_STAT_DMG_DESC:
                t = "Damage + 6%.";
                break;
            case TextKeys.CHOICE_STAT_SPD_DESC:
                t = "Speed + 5%.";
                break;
            case TextKeys.CHOICE_STAT_PRJ_NUM_DESC:
                t = "Number of projectile + 1.";
                break;
            case TextKeys.CHOICE_STAT_PRJ_SPD_DESC:
                t = "Projectile speed + 8%.";
                break;
            case TextKeys.CHOICE_STAT_COOL_DESC:
                t = "Cooldown - 4%.";
                break;
            case TextKeys.CHOICE_STAT_SIZE_DESC:
                t = "Skill size + 10%.";
                break;
            case TextKeys.CHOICE_STAT_EXP_DESC:
                t = "Experience obtain + 15%.";
                break;
            case TextKeys.CHOICE_STAT_OBTAIN_DESC:
                t = "Obtain range + 15%.";
                break;
            case TextKeys.CHOICE_STAT_RECOVERY:
                t = "Recovery + 0.1.";
                break;
        }

        return t;
    }

    public string GetSkillName(int skill)
    {
        string t = "";
        switch (skill)
        {
            case SkillKey.SKILL_ATTACK_AUTO:
                t = "Magical arrow";
                break;
            case SkillKey.SKILL_ATTACK_SATELLITE:
                t = "Satellite";
                break;
            case SkillKey.SKILL_ATTACK_TOXIC:
                t = "Spray";
                break;
            case SkillKey.SKILL_ATTACK_SKULL:
                t = "Pesticide Bomb";
                break;
            case SkillKey.SKILL_ATTACK_VISION:
                t = "Flash Vision";
                break;
            case SkillKey.SKILL_ATTACK_SLASH:
                t = "Blink Slash";
                break;
            case SkillKey.SKILL_ATTACK_DAEDALUS:
                t = "Daedalus";
                break;
            case SkillKey.SKILL_ATTACK_QUAKE:
                t = "Rage Quake";
                break;

            case SkillKey.STAT_DMG:
                t = "Ring of Strength";
                break;
            case SkillKey.STAT_SPD:
                t = "Boots of agility";
                break;
            case SkillKey.STAT_PRJ_NUM:
                t = "Crossbow";
                break;
            case SkillKey.STAT_PRJ_SPD:
                t = "Powder";
                break;
            case SkillKey.STAT_COOL:
                t = "Hourglass";
                break;
            case SkillKey.STAT_SIZE:
                t = "Scale";
                break;
            case SkillKey.STAT_EXP_COEF:
                t = "Experience book";
                break;
            case SkillKey.STAT_OBTAIN_RANGE:
                t = "Magnetism";
                break;
            case SkillKey.STAT_RECOVER:
                t = "Recovery";
                break;
        }

        return t;
    }

    public string GetSkillDesc(int skill, int level)
    {
        int k = 0;
        switch (skill)
        {
            case SkillKey.SKILL_ATTACK_AUTO:
                k = 5021;
                break;
            case SkillKey.SKILL_ATTACK_SATELLITE:
                k = 5041;
                break;
            case SkillKey.SKILL_ATTACK_TOXIC:
                k = 5061;
                break;
            case SkillKey.SKILL_ATTACK_SKULL:
                k = 5081;
                break;
            case SkillKey.SKILL_ATTACK_VISION:
                k = 5101;
                break;
            case SkillKey.SKILL_ATTACK_SLASH:
                k = 5121;
                break;
            case SkillKey.SKILL_ATTACK_GATE:
                k = 5141;
                break;
            case SkillKey.SKILL_ATTACK_DAEDALUS:
                k = 5161;
                break;
            case SkillKey.SKILL_ATTACK_QUAKE:
                k = 5181;
                break;

            case SkillKey.STAT_DMG:
                k = TextKeys.CHOICE_STAT_DMG_DESC;
                break;
            case SkillKey.STAT_SPD:
                k = TextKeys.CHOICE_STAT_SPD_DESC;
                break;
            case SkillKey.STAT_PRJ_NUM:
                k = TextKeys.CHOICE_STAT_PRJ_NUM_DESC;
                break;
            case SkillKey.STAT_PRJ_SPD:
                k = TextKeys.CHOICE_STAT_PRJ_SPD_DESC;
                break;
            case SkillKey.STAT_COOL:
                k = TextKeys.CHOICE_STAT_COOL_DESC;
                break;
            case SkillKey.STAT_SIZE:
                k = TextKeys.CHOICE_STAT_SIZE_DESC;
                break;
            case SkillKey.STAT_EXP_COEF:
                k = TextKeys.CHOICE_STAT_EXP_DESC;
                break;
            case SkillKey.STAT_OBTAIN_RANGE:
                k = TextKeys.CHOICE_STAT_OBTAIN_DESC;
                break;
            case SkillKey.STAT_RECOVER:
                k = TextKeys.CHOICE_STAT_RECOVERY;
                break;
        }

        if (skill < 100)
            k += level;

        return GetStringByKey(k);
    }

    public string GetCharacterName(int character)
    {
        string t = "";
        switch (character)
        {
            case CharacterKey.CITIZEN:
                t = "Citizen";
                break;
            case CharacterKey.PEST_CONTROLLER:
                t = "Pest Controller";
                break;
        }

        return t;
    }

    public string GetCharacterDesc(int character)
    {
        string t = "";
        switch (character)
        {
            case CharacterKey.CITIZEN:
                t = $"Start Skill : Magical arrow\n{ConstMethods.GetColoredString("Experience obtain + 20%")}.";
                break;
            case CharacterKey.PEST_CONTROLLER:
                t = $"Start Skill : Satellite\n{ConstMethods.GetColoredString("Projectile speed + 15%.")}";
                break;
        }

        return t;
    }

    public string GetAttributeName(int attribute)
    {
        string t = "";
        switch (attribute)
        {
            case AttributeKey.EXPERIENCE:
                t = "Veteran";
                break;
            case AttributeKey.ACCELERATION:
                t = "Acceleration";
                break;
        }

        return t;
    }

    public string GetAttributeDesc(int attribute)
    {
        string t = "";
        switch (attribute)
        {
            case AttributeKey.EXPERIENCE:
                t = "On use, you get experience proportionally to your current survival time.";
                break;
            case AttributeKey.ACCELERATION:
                t = "On use, your speed and projectiles speed increase for 3 seconds.";
                break;
        }

        return t;
    }

    public string GetUpgradeName(int key)
    {
        string t = "Error";
        switch (key)
        {
            case SkillKey.STAT_DMG:
                t = "Damage";
                break;
            case SkillKey.STAT_SPD:
                t = "Speed";
                break;
            case SkillKey.STAT_PRJ_NUM:
                t = "Number of Projectiles";
                break;
            case SkillKey.STAT_PRJ_SPD:
                t = "Projectile Speed";
                break;
            case SkillKey.STAT_COOL:
                t = "Cooldown";
                break;
            case SkillKey.STAT_SIZE:
                t = "Skill Size";
                break;
            case SkillKey.STAT_EXP_COEF:
                t = "Experience Obtain";
                break;
            case SkillKey.STAT_OBTAIN_RANGE:
                t = "Obtain Range";
                break;
            case SkillKey.STAT_RECOVER:
                t = "Recovery";
                break;

        }

        return t + " Upgrade";
    }

    public string GetSkillLevelUpText(int skill)
    {
        return $"{GetSkillName(skill)} Level + 1.";
    }

    public string GetSkillEvolveText(int skill)
    {
        return $"{GetSkillName(skill)} evolved!";
    }

    public string GetCurPenanceText(int i)
    {
        return $"{i} level.";
    }
}

/// <summary>
/// 키에 따라 string을 반환하는 메서드.
/// 언어의 경우 항상 맨 마지막에 _KR 아니면 _EN 과 같은 식으로 쓴다.
/// 그 외엔 앞에 UI_ , REWARD_ , SKILL_ 등으로 분류한다.
/// 0 ~ 9999까진 한국어, 1 ~ 19999까진 영어, ... (같은 내용은 항상 n * 10000 차이가 나도록) 해서 설정에 따라 반환.
/// </summary>
public static class TextKeys
{
    public const int UI_MAIN_START_GAME = 101;
    public const int UI_MAIN_UPGRADE = 102;
    
    public const int UI_MAX_LEVEL = 1001;
    public const int UI_UPGRADE = 1002;
    public const int UI_NOT_SELECTED = 1003;

    public const int UI_PENANCE = 2001;

    public const int UI_PENANCE_LVL1_DESC = 2011;
    public const int UI_PENANCE_LVL2_DESC = 2012;
    public const int UI_PENANCE_LVL3_DESC = 2013;
    public const int UI_PENANCE_LVL4_DESC = 2014;
    public const int UI_PENANCE_LVL5_DESC = 2015;
    public const int UI_PENANCE_LVL6_DESC = 2016;
    public const int UI_PENANCE_LVL7_DESC = 2017;
    public const int UI_PENANCE_LVL8_DESC = 2018;
    public const int UI_PENANCE_LVL9_DESC = 2019;
    public const int UI_PENANCE_LVL10_DESC = 2020;
    
    public const int CHOICE_AUTO_NAME = 5020;
    public const int CHOICE_AUTO_OBTAIN = 5021;
    public const int CHOICE_AUTO_LVL2_DESC = 5022;
    public const int CHOICE_AUTO_LVL3_DESC = 5023;
    public const int CHOICE_AUTO_LVL4_DESC = 5024;
    public const int CHOICE_AUTO_LVL5_DESC = 5025;
    public const int CHOICE_AUTO_LVL6_DESC = 5026;
    public const int CHOICE_AUTO_LVL7_DESC = 5027;
    public const int CHOICE_AUTO_LVL8_DESC = 5028;
    public const int CHOICE_AUTO_LVL9_DESC = 5029;
    public const int CHOICE_AUTO_LVL10_DESC = 5030;

    public const int CHOICE_SATELLITE_NAME = 5040;
    public const int CHOICE_SATELLITE_OBTAIN = 5041;
    public const int CHOICE_SATELLITE_LVL2_DESC = 5042;
    public const int CHOICE_SATELLITE_LVL3_DESC = 5043;
    public const int CHOICE_SATELLITE_LVL4_DESC = 5044;
    public const int CHOICE_SATELLITE_LVL5_DESC = 5045;
    public const int CHOICE_SATELLITE_LVL6_DESC = 5046;
    public const int CHOICE_SATELLITE_LVL7_DESC = 5047;

    public const int CHOICE_TOXIC_NAME = 5060;
    public const int CHOICE_TOXIC_OBTAIN = 5061;
    public const int CHOICE_TOXIC_LVL2_DESC = 5062;
    public const int CHOICE_TOXIC_LVL3_DESC = 5063;
    public const int CHOICE_TOXIC_LVL4_DESC = 5064;
    public const int CHOICE_TOXIC_LVL5_DESC = 5065;
    public const int CHOICE_TOXIC_LVL6_DESC = 5066;
    public const int CHOICE_TOXIC_LVL7_DESC = 5067;

    public const int CHOICE_SKULL_NAME = 5080;
    public const int CHOICE_SKULL_OBTAIN = 5081;
    public const int CHOICE_SKULL_LVL2_DESC = 5082;
    public const int CHOICE_SKULL_LVL3_DESC = 5083;
    public const int CHOICE_SKULL_LVL4_DESC = 5084;
    public const int CHOICE_SKULL_LVL5_DESC = 5085;
    public const int CHOICE_SKULL_LVL6_DESC = 5086;
    public const int CHOICE_SKULL_LVL7_DESC = 5087;
    
    public const int CHOICE_VISION_NAME = 5100;
    public const int CHOICE_VISION_OBTAIN = 5101;
    public const int CHOICE_VISION_LVL2_DESC = 5102;
    public const int CHOICE_VISION_LVL3_DESC = 5103;
    public const int CHOICE_VISION_LVL4_DESC = 5104;
    public const int CHOICE_VISION_LVL5_DESC = 5105;
    public const int CHOICE_VISION_LVL6_DESC = 5106;
    public const int CHOICE_VISION_LVL7_DESC = 5107;
    
    public const int CHOICE_SLASH_NAME = 5120;
    public const int CHOICE_SLASH_OBTAIN = 5121;
    public const int CHOICE_SLASH_LVL2_DESC = 5122;
    public const int CHOICE_SLASH_LVL3_DESC = 5123;
    public const int CHOICE_SLASH_LVL4_DESC = 5124;
    public const int CHOICE_SLASH_LVL5_DESC = 5125;
    public const int CHOICE_SLASH_LVL6_DESC = 5126;
    public const int CHOICE_SLASH_LVL7_DESC = 5127;
    
    public const int CHOICE_GATE_NAME = 5140;
    public const int CHOICE_GATE_OBTAIN = 5141;
    public const int CHOICE_GATE_LVL2_DESC = 5142;
    public const int CHOICE_GATE_LVL3_DESC = 5143;
    public const int CHOICE_GATE_LVL4_DESC = 5144;
    public const int CHOICE_GATE_LVL5_DESC = 5145;
    public const int CHOICE_GATE_LVL6_DESC = 5146;
    public const int CHOICE_GATE_LVL7_DESC = 5147;
    
    public const int CHOICE_DAEDALUS_NAME = 5160;
    public const int CHOICE_DAEDALUS_OBTAIN = 5161;
    public const int CHOICE_DAEDALUS_LVL2_DESC = 5162;
    public const int CHOICE_DAEDALUS_LVL3_DESC = 5163;
    public const int CHOICE_DAEDALUS_LVL4_DESC = 5164;
    public const int CHOICE_DAEDALUS_LVL5_DESC = 5165;
    public const int CHOICE_DAEDALUS_LVL6_DESC = 5166;
    public const int CHOICE_DAEDALUS_LVL7_DESC = 5167;
    
    public const int CHOICE_QUAKE_NAME = 5180;
    public const int CHOICE_QUAKE_OBTAIN = 5181;
    public const int CHOICE_QUAKE_LVL2_DESC = 5182;
    public const int CHOICE_QUAKE_LVL3_DESC = 5183;
    public const int CHOICE_QUAKE_LVL4_DESC = 5184;
    public const int CHOICE_QUAKE_LVL5_DESC = 5185;
    public const int CHOICE_QUAKE_LVL6_DESC = 5186;
    public const int CHOICE_QUAKE_LVL7_DESC = 5187;

    public const int CHOICE_STAT_DMG_DESC = 6001;
    public const int CHOICE_STAT_SPD_DESC = 6011;
    public const int CHOICE_STAT_PRJ_NUM_DESC = 6021;
    public const int CHOICE_STAT_PRJ_SPD_DESC = 6031;
    public const int CHOICE_STAT_COOL_DESC = 6041;
    public const int CHOICE_STAT_SIZE_DESC = 6051;
    public const int CHOICE_STAT_EXP_DESC = 6061;
    public const int CHOICE_STAT_OBTAIN_DESC = 6071;
    public const int CHOICE_STAT_RECOVERY = 6081;


}