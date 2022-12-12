using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeSceneManager : MonoBehaviour
{
    public static UpgradeSceneManager instance;

    private int selectedSkill = -1;

    [SerializeField] private TextMeshProUGUI tmpStat;
    [SerializeField] private TextMeshProUGUI tmpCost;
    
    

    [SerializeField] private RectTransform buttonUpgrade;
    [SerializeField] private TextMeshProUGUI tmpButtonUpgrade;

    [SerializeField, Space(10f)] private Image levelDmgBar;


    private void Awake()
    {
        if (!instance)
            instance = this;
        
        buttonUpgrade.gameObject.SetActive(false);
        tmpButtonUpgrade.text = TextManager.instance.GetStringByKey(TextKeys.UI_UPGRADE);
        
        setPointText();
        resetSelected();

        for (int i = 0; i < 9; i++)
        {
            setLevelBar(101+i);
        }
    }

    public void OnSelectStatToUpgrade(int index)
    {
        index = Mathf.Clamp(index,0, 9);

        selectedSkill = 101 + index;
        int cost = getUpgradeCost(selectedSkill);

        tmpStat.text = $"{TextManager.instance.GetUpgradeName(selectedSkill)}";
        
        if(DataManager.instance.GetUpgradeLevel(selectedSkill) < getMaxLevel(selectedSkill))
            tmpCost.text = $"{cost}";
        else
        {
            tmpCost.text = TextManager.instance.GetStringByKey(TextKeys.UI_MAX_LEVEL);
        }

        if (DataManager.instance.GetUpgradeLevel(selectedSkill) < getMaxLevel(selectedSkill)
            && DataManager.instance.GetCurPoint() >= cost)
        {
            buttonUpgrade.gameObject.SetActive(true);
        }
        else buttonUpgrade.gameObject.SetActive(false);
    }
    
    public void Upgrade()
    {
        if (selectedSkill == -1) return;
        
        DataManager.instance.AddPoint(-getUpgradeCost(selectedSkill));
        DataManager.instance.LevelUpgrade(selectedSkill);
        setPointText();
        DataManager.instance.Save();
        setLevelBar(selectedSkill);
        resetSelected();
    }
    
    [SerializeField] private Image levelSpdBar,
        levelPrjNumBar,
        levelPrjSpdBar,
        levelCoolBar,
        levelSizeBar,
        levelExpBar,
        levelObtainBar,
        levelRecoveryBar;

    void setLevelBar(int skill)
    {
        Image bar = null;

        switch (skill)
        {
            case SkillKey.STAT_DMG:
                bar = levelDmgBar;
                break;
            case SkillKey.STAT_SPD:
                bar = levelSpdBar;
                break;
            case SkillKey.STAT_PRJ_NUM:
                bar = levelPrjNumBar;
                break;
            case SkillKey.STAT_PRJ_SPD:
                bar = levelPrjSpdBar;
                break;
            case SkillKey.STAT_COOL:
                bar = levelCoolBar;
                break;
            case SkillKey.STAT_SIZE:
                bar = levelSizeBar;
                break;
            case SkillKey.STAT_EXP_COEF:
                bar = levelExpBar;
                break;
            case SkillKey.STAT_OBTAIN_RANGE:
                bar = levelObtainBar;
                break;
            case SkillKey.STAT_RECOVER:
                bar = levelRecoveryBar;
                break;
        }
        if(bar)
            bar.fillAmount = (float)DataManager.instance.GetUpgradeLevel(skill)/ getMaxLevel(skill);
    }   

    void resetSelected()
    {
        buttonUpgrade.gameObject.SetActive(false);
        selectedSkill = -1; tmpStat.text = TextManager.instance.GetStringByKey(TextKeys.UI_NOT_SELECTED);
        tmpCost.text = $"";
    }

    int getUpgradeCost(int key)
    {
        switch (key)
        {
            
            case SkillKey.STAT_DMG:
                return 300 + DataManager.instance.LevelDmgUpgrade * 100;
                break;
            case SkillKey.STAT_SPD:
                return 300 + DataManager.instance.LevelSpdUpgrade * 100;
                break;
            case SkillKey.STAT_PRJ_NUM:
                return 2500 + DataManager.instance.LevelPrjNumUpgrade * 1000;
                break;
            case SkillKey.STAT_PRJ_SPD:
                return 300 + DataManager.instance.LevelPrjSpdUpgrade * 100;
                break;
            case SkillKey.STAT_COOL:
                return 500 + DataManager.instance.LevelCoolUpgrade * 150;
                break;
            case SkillKey.STAT_SIZE:
                return 300 + DataManager.instance.LevelSizeUpgrade * 100;
                break;
            case SkillKey.STAT_EXP_COEF:
                return 500 + DataManager.instance.LevelExpUpgrade * 150;
                break;
            case SkillKey.STAT_OBTAIN_RANGE:
                return 300 + DataManager.instance.LevelObtainUpgrade * 100;
                break;
            case SkillKey.STAT_RECOVER:
                return 300 + DataManager.instance.LevelRecoverUpgrade * 100;
                break;
        }

        return 99999;
    }

    int getMaxLevel(int key)
    {
        switch (key)
        {
            case SkillKey.STAT_DMG:
                return 4;
            case SkillKey.STAT_SPD:
                return 4;
            case SkillKey.STAT_PRJ_NUM:
                return 1;
            case SkillKey.STAT_PRJ_SPD:
                return 4;
            case SkillKey.STAT_COOL:
                return 3;
            case SkillKey.STAT_SIZE:
                return 4;
            case SkillKey.STAT_EXP_COEF:
                return 4;
            case SkillKey.STAT_OBTAIN_RANGE:
                return 4;
            case SkillKey.STAT_RECOVER:
                return 4;
        }

        return -1;
    }

    public void ReturnToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    [SerializeField, Space(10f)] private TextMeshProUGUI tmpPoints;
    void setPointText()
    {
        tmpPoints.text = $"{DataManager.instance.GetCurPoint()}";
    }
}
