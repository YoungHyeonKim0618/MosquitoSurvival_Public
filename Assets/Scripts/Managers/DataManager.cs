using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private void Awake()
    {
        if (!instance) instance = this;
        DontDestroyOnLoad(gameObject);
        
        
        Load();

        Application.targetFrameRate = 30;
    }

    private int _curPoint = 0;

    public int GetCurPoint()
    {
        return _curPoint;
        
    }

    public void AddPoint(int n)
    {
        _curPoint += n;
    }


    
    //업그레이드 정보.
    public int LevelDmgUpgrade { get; private set; } = 0;
    public int LevelSpdUpgrade { get; private set; } = 0;
    public int LevelPrjNumUpgrade { get; private set; } = 0;
    public int LevelPrjSpdUpgrade { get; private set; } = 0;
    public int LevelCoolUpgrade { get; private set; } = 0;
    public int LevelSizeUpgrade { get; private set; } = 0;
    public int LevelExpUpgrade { get; private set; } = 0;
    public int LevelObtainUpgrade { get; private set; } = 0;
    public int LevelRecoverUpgrade { get; private set; } = 0;

    /// <summary>
    /// 업그레이드의 레벨을 1 올림.
    /// </summary>
    /// <param name="key"></param>
    public void LevelUpgrade(int key)
    {
        switch (key)
        {
            case SkillKey.STAT_DMG:
                LevelDmgUpgrade++;
                break;
            case SkillKey.STAT_SPD:
                LevelSpdUpgrade++;
                break;
            case SkillKey.STAT_PRJ_NUM:
                LevelPrjNumUpgrade++;
                break;
            case SkillKey.STAT_PRJ_SPD:
                LevelPrjSpdUpgrade++;
                break;
            case SkillKey.STAT_COOL:
                LevelCoolUpgrade++;
                break;
            case SkillKey.STAT_SIZE:
                LevelSizeUpgrade++;
                break;
            case SkillKey.STAT_EXP_COEF:
                LevelExpUpgrade++;
                break;
            case SkillKey.STAT_OBTAIN_RANGE:
                LevelObtainUpgrade++;
                break;
            case SkillKey.STAT_RECOVER:
                LevelRecoverUpgrade++;
                break;
        }
    }
    public int GetUpgradeLevel(int key)
    {
        switch (key)
        {
            case SkillKey.STAT_DMG:
                return LevelDmgUpgrade;
            case SkillKey.STAT_SPD:
                return LevelSpdUpgrade;
            case SkillKey.STAT_PRJ_NUM:
                return LevelPrjNumUpgrade;
            case SkillKey.STAT_PRJ_SPD:
                return LevelPrjSpdUpgrade;
            case SkillKey.STAT_COOL:
                return LevelCoolUpgrade;
            case SkillKey.STAT_SIZE:
                return LevelSizeUpgrade;
            case SkillKey.STAT_EXP_COEF:
                return LevelExpUpgrade;
            case SkillKey.STAT_OBTAIN_RANGE:
                return LevelObtainUpgrade;
            case SkillKey.STAT_RECOVER:
                return LevelRecoverUpgrade;
        }

        return -1;
    }



    public void Save()
    {
        PlayerPrefs.SetInt("Point",_curPoint);
        
        PlayerPrefs.SetInt("DmgLevel",LevelDmgUpgrade);
        PlayerPrefs.SetInt("SpdLevel",LevelSpdUpgrade);
        PlayerPrefs.SetInt("PrjNumLevel",LevelPrjNumUpgrade);
        PlayerPrefs.SetInt("PrjSpdLevel",LevelPrjSpdUpgrade);
        PlayerPrefs.SetInt("CoolLevel",LevelCoolUpgrade);
        PlayerPrefs.SetInt("SizeLevel",LevelSizeUpgrade);
        PlayerPrefs.SetInt("ExpLevel",LevelExpUpgrade);
        PlayerPrefs.SetInt("ObtainLevel",LevelObtainUpgrade);
        PlayerPrefs.SetInt("RecoverLevel",LevelRecoverUpgrade);
        
    }

    public void Load()
    {
        if(PlayerPrefs.HasKey("Point"))
            _curPoint = PlayerPrefs.GetInt("Point");

        if (PlayerPrefs.HasKey("DmgLevel"))
            LevelDmgUpgrade = PlayerPrefs.GetInt("DmgLevel");
        if (PlayerPrefs.HasKey("SpdLevel"))
            LevelSpdUpgrade = PlayerPrefs.GetInt("SpdLevel");
        if (PlayerPrefs.HasKey("PrjNumLevel"))
            LevelPrjNumUpgrade = PlayerPrefs.GetInt("PrjNumLevel");
        if (PlayerPrefs.HasKey("PrjSpdLevel"))
            LevelPrjSpdUpgrade = PlayerPrefs.GetInt("PrjSpdLevel");
        if (PlayerPrefs.HasKey("CoolLevel"))
            LevelCoolUpgrade = PlayerPrefs.GetInt("CoolLevel");
        if (PlayerPrefs.HasKey("SizeLevel"))
            LevelSizeUpgrade = PlayerPrefs.GetInt("SizeLevel");
        if (PlayerPrefs.HasKey("ExpLevel"))
            LevelExpUpgrade = PlayerPrefs.GetInt("ExpLevel");
        if (PlayerPrefs.HasKey("ObtainLevel"))
            LevelObtainUpgrade = PlayerPrefs.GetInt("ObtainLevel");
        if (PlayerPrefs.HasKey("RecoverLevel"))
            LevelRecoverUpgrade = PlayerPrefs.GetInt("RecoverLevel");

    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}

public static class LanguageKey
{
    public const int KOREAN = 0;
    public const int ENGLISH = 1;
}