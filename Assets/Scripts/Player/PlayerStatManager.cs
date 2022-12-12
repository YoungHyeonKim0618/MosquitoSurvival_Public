using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager instance;


    private int curLevel = 1;

    //보상 선택할 수 있는 횟수.
    private int levelPoint = 0;

    [SerializeField] private TextMeshProUGUI tmpDmgDisplay,
        tmpSpdDisplay,
        tmpPrjNumDisplay,
        tmpPrjSpdDisplay,
        tmpCoolDisplay,
        tmpSizeDisplay;


    private void Awake()
    {
        if (!instance)
            instance = this;

        MaxHp = 100;
        CurHp = 100;
    }

    private void FixedUpdate()
    {
        if (IsBeingHit)
        {
            SetDamaged(Time.fixedDeltaTime * 60);
        }
    }

    private void Start()
    {
        ObtainEXP(0);
        StartCoroutine(waitAndRecover());
        
        applyUpgrades();

        tmpLevel.text = $"Level {curLevel}";
    }
    
    [SerializeField]
    private TextMeshProUGUI tmpPoint;

    [SerializeField]
    private TextMeshProUGUI tmpLevel;
    public void increaseLevel()
    {
        curLevel++;
        
        //15레벨부터 투사체 개수 증가 가능
        if (curLevel == 15)
        {
            RewardManager.instance.AddPrjNumToUnobtained();
        }
        
        if (curLevel <= 10)
            maxExp = 5 + 4 * (curLevel-1);
        else
        {
            maxExp = 1.11f * maxExp;
        }

        tmpLevel.text = $"Level {curLevel}";
        //보상 선택 창 리스트에 더함.
        levelPoint++;
        if(levelPoint == 1)
            RewardManager.instance.EnablePointButton();
        tmpPoint.text = $" + {levelPoint}";
    }

    private float maxHp;

    public float MaxHp
    {
        get { return maxHp;}
        set
        {
            if (value > maxHp)
            {
                CurHp += value - maxHp;
            }
            else if (value < curHp)
            {
                CurHp = value;
            }
            
            
            maxHp = value;
            
            setHpBars();
        }
    }
    
    private float curHp;
    public float CurHp
    {
        get { return curHp;}
        set
        {
            if(value <= 0)
                Die();
            value = Mathf.Clamp(value,0, maxHp);
            curHp = value; 
            setHpBars();
        }
    }

    [SerializeField, Header("Hp Bars")] private Image hpBar;
    [SerializeField] private Image hpBarBackground;
    void setHpBars()
    {
        hpBar.fillAmount = curHp / maxHp;
    }


    public bool IsBeingHit = false;

    public void SetDamaged(float dmg)
    {
        CurHp -= dmg;
    }

    private float maxExp = 5;
    private float curExp = 0;


    public void ObtainEXP(float amount)
    {
        amount *= GetExpCoef();
        float t = maxExp - curExp;
        if (amount >= maxExp - curExp)
        {
            increaseLevel();
            //그러고도 경험치가 많이 남았다면
            if (amount - (maxExp - curExp) > maxExp)
            {
                curExp = maxExp - 1;
            }
            else
            {
                curExp = amount - t;
            }
        }
        else curExp += amount;
        
        setExpBar();
    }

    [SerializeField] private Image _expBar;
    void setExpBar()
    {
        _expBar.fillAmount = curExp / maxExp;
    }
    
    
    
    //추가적인 스탯.
    
    //이동속도 ( + n%)
    private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
            tmpSpdDisplay.text = $"{(100 + speed)/100:0.00}";
        }
    }
    
    //피해 ( + n%)
    private float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; 
            tmpDmgDisplay.text = $"{(100 + damage)/100:0.00}";}
    }

    public float GetDmgCoef()
    {
        return (100 + damage) / 100;
    }
    
    //투사체 개수 ( + n개)
    private int numOfPrj;
    public int NumOfPrj
    {
        get { return numOfPrj; }
        set
        {
            numOfPrj = value;
            UnityEventManager.instance.OnNumOfPrjChanged.Invoke();
            tmpPrjNumDisplay.text = $"+ {numOfPrj}";
        }
    }
    
    //투사체 이동 속도 ( + n%)
    private float prjSpeed;
    public float PrjSpeed
    {
        get { return prjSpeed; }
        set { prjSpeed = value; 
            tmpPrjSpdDisplay.text = $"{(100 + prjSpeed)/100:0.00}";}
    }

    public float GetPrjSpdCoef()
    {
        return (100 + prjSpeed) / 100;
    }
    
    //스킬 쿨타임 ( - n%)
    private float cooldownDecrement = 0;
    public float CooldownDecrement
    {
        get { return cooldownDecrement; }
        set { cooldownDecrement = value;
            tmpCoolDisplay.text = $"- {(int)cooldownDecrement}%"; }
    }

    public float GetCoolCoef()
    {
        return Mathf.Clamp((100 - cooldownDecrement) / 100, 0.5f, 1f);
    }
    
    //스킬 범위 ( + n%)
    private float skillSize;
    public float SkillSize
    {
        get { return skillSize; }
        set
        {
            skillSize = value; 
            UnityEventManager.instance.OnSkillSizeChanged.Invoke();
            tmpSizeDisplay.text = $"{(100 + skillSize)/100:0.00}";
        }
    }

    public float GetSizeCoef()
    {
        return (100 + skillSize) / 100;
    }
    
    //경험치 획득량 ( + n%)
    private float expAmount;
    public float ExpAmount
    {
        get { return expAmount; }
        set { expAmount = value; }
    }

    public float GetExpCoef()
    {
        return (100 + expAmount) / 100;
    }


    [SerializeField] private Transform _obtainRange;
    //획득 반경 증가 ( + n%)
    private float obtainRange;
    public float ObtainRange
    {
        get { return obtainRange; }
        set
        {
            obtainRange = value;

            _obtainRange.localScale = new Vector2(1,1) *  (100 + obtainRange) / 100;
        }
    }
    
    //체력 회복(초당)
    private float hpRecovery;

    public float HpRecovery
    {
        get { return hpRecovery; }
        set { hpRecovery = value; }
    }
    
    //열쇠 드랍확률 증가 ( + n%)
    private float luck;
    public float Luck
    {
        get { return luck; }
        set { luck = value; }
    }


    //스킬들의 레벨.
    [SerializeField]
    private int levelAuto = 0;
    [SerializeField]
    private int levelSatellite = 0;

    private int levelToxic;
    private int levelSkull;
    private int levelVision;
    private int levelSlash;
    private int levelGate;
    private int levelDaedalus;
    private int levelQuake;

    private int levelDamage, levelSpeed, levelNumOfPrj, levelPrjSpeed, levelCooltime,
        levelSize, levelExp, levelObtain, levelRecovery;

    //키를 받아 스킬의 레벨을 반환함.
    public int GetSkillLevel(int skill)
    {
        int i = 0;

        switch (skill)
        {
            case SkillKey.SKILL_ATTACK_AUTO:
                i = levelAuto;
                break;
            case SkillKey.SKILL_ATTACK_SATELLITE:
                i = levelSatellite;
                break;
            case SkillKey.SKILL_ATTACK_TOXIC:
                i = levelToxic;
                break;
            case SkillKey.SKILL_ATTACK_SKULL:
                i = levelSkull;
                break;
            case SkillKey.SKILL_ATTACK_VISION:
                i = levelVision;
                break;
            case SkillKey.SKILL_ATTACK_SLASH:
                i = levelSlash;
                break;
            case SkillKey.SKILL_ATTACK_GATE:
                i = levelGate;
                break;
            case SkillKey.SKILL_ATTACK_DAEDALUS:
                i = levelDaedalus;
                break;
            case SkillKey.SKILL_ATTACK_QUAKE:
                i = levelQuake;
                break;

            case SkillKey.STAT_DMG:
                i = levelDamage;
                break;
            case SkillKey.STAT_SPD:
                i = levelSpeed;
                break;
            case SkillKey.STAT_PRJ_NUM:
                i = levelNumOfPrj;
                break;
            case SkillKey.STAT_PRJ_SPD:
                i = levelPrjSpeed;
                break;
            case SkillKey.STAT_COOL:
                i = levelCooltime;
                break;
            case SkillKey.STAT_SIZE:
                i = levelSize;
                break;
            case SkillKey.STAT_EXP_COEF:
                i = levelExp;
                break;
            case SkillKey.STAT_OBTAIN_RANGE:
                i = levelObtain;
                break;
            case SkillKey.STAT_RECOVER:
                i = levelRecovery;
                break;
        }

        return i;
    }

    public int GetSkillMaxLevel(int skill)
    {
        int i = 0;

        switch (skill)
        {
            case SkillKey.SKILL_ATTACK_AUTO:
                i = 7;
                break;
            case SkillKey.SKILL_ATTACK_SATELLITE:
                i = 7;
                break;
            case SkillKey.SKILL_ATTACK_TOXIC:
                i = 7;
                break;
            case SkillKey.SKILL_ATTACK_SKULL:
                i = 7;
                break;
            case SkillKey.SKILL_ATTACK_VISION:
                i = 7;
                break;
            case SkillKey.SKILL_ATTACK_SLASH:
                i = 7;
                break;
            case SkillKey.SKILL_ATTACK_GATE:
                i = 7;
                break;
            case SkillKey.SKILL_ATTACK_DAEDALUS:
                i = 7;
                break;
            case SkillKey.SKILL_ATTACK_QUAKE:
                i = 7;
                break;
            
            case SkillKey.STAT_DMG:
                i = 7;
                break;
            case SkillKey.STAT_SPD:
                i = 7;
                break;
            case SkillKey.STAT_PRJ_NUM:
                i = 2;
                break;
            case SkillKey.STAT_PRJ_SPD:
                i = 7;
                break;
            case SkillKey.STAT_COOL:
                i = 5;
                break;
            case SkillKey.STAT_SIZE:
                i = 7;
                break;
            case SkillKey.STAT_EXP_COEF:
                i = 7;
                break;
            case SkillKey.STAT_OBTAIN_RANGE:
                i = 7;
                break;
            case SkillKey.STAT_RECOVER:
                i = 5;
                break;
        }
        return i;
    }

    //키를 받아 스킬의 레벨을 올림.
    //실제로 스킬이 강화되는 건 RewardManager에서 처리.
    public void AddSkillLevel(int skill)
    {
        switch (skill)
        {
            case SkillKey.SKILL_ATTACK_AUTO:
                levelAuto++;
                break;
            case SkillKey.SKILL_ATTACK_SATELLITE:
                levelSatellite++;
                break;
            case SkillKey.SKILL_ATTACK_TOXIC:
                levelToxic++;
                break;
            case SkillKey.SKILL_ATTACK_SKULL:
                levelSkull++;
                break;
            case SkillKey.SKILL_ATTACK_VISION:
                levelVision++;
                break;
            case SkillKey.SKILL_ATTACK_SLASH:
                levelSlash++;
                break;
            case SkillKey.SKILL_ATTACK_GATE:
                levelGate++;
                break;
            case SkillKey.SKILL_ATTACK_DAEDALUS:
                levelDaedalus++;
                break;
            case SkillKey.SKILL_ATTACK_QUAKE:
                levelQuake++;
                break;

            case SkillKey.STAT_DMG:
                levelDamage++;
                break;
            case SkillKey.STAT_SPD:
                levelSpeed++;
                break;
            case SkillKey.STAT_PRJ_NUM:
                levelNumOfPrj++;
                break;
            case SkillKey.STAT_PRJ_SPD:
                levelPrjSpeed++;
                break;
            case SkillKey.STAT_COOL:
                levelCooltime++;
                break;
            case SkillKey.STAT_SIZE:
                levelSize++;
                break;
            case SkillKey.STAT_EXP_COEF:
                levelExp++;
                break;
            case SkillKey.STAT_OBTAIN_RANGE:
                levelObtain++;
                break;
        }
    }
    public void UpgradeSkill(int skill)
    {
        switch (skill)
        {
            case SkillKey.STAT_DMG:
                Damage += 6;
                break;
            case SkillKey.STAT_SPD:
                Speed += 5;
                break;
            case SkillKey.STAT_PRJ_NUM:
                NumOfPrj++;
                break;
            case SkillKey.STAT_PRJ_SPD:
                PrjSpeed += 8;
                break;
            case SkillKey.STAT_COOL:
                CooldownDecrement += 4;
                break;
            case SkillKey.STAT_SIZE:
                SkillSize += 10;
                break;
            case SkillKey.STAT_EXP_COEF:
                ExpAmount += 15;
                break;
            case SkillKey.STAT_OBTAIN_RANGE:
                ObtainRange += 15;
                break;
            case SkillKey.STAT_RECOVER:
                HpRecovery += 0.1f;
                break;
        }
    }

    public void PointUsed()
    {
        levelPoint--;
        
        if(levelPoint > 0)
            RewardManager.instance.ShowChoices();
    }

    IEnumerator waitAndRecover()
    {
        WaitForSeconds ws = new WaitForSeconds(1f);
        while (true)
        {
            yield return ws;
            CurHp += HpRecovery;
        }
    }

    void applyUpgrades()
    {
        Damage += DataManager.instance.LevelDmgUpgrade * 5;
        Speed += DataManager.instance.LevelSpdUpgrade * 5;
        NumOfPrj += DataManager.instance.LevelPrjNumUpgrade;
        PrjSpeed += DataManager.instance.LevelPrjSpdUpgrade * 5;
        CooldownDecrement += DataManager.instance.LevelCoolUpgrade * 3;
        SkillSize += DataManager.instance.LevelSizeUpgrade * 5;
        ExpAmount += DataManager.instance.LevelExpUpgrade * 5;
        ObtainRange += DataManager.instance.LevelObtainUpgrade * 5;
        HpRecovery += DataManager.instance.LevelRecoverUpgrade * 0.05f;
    }

    void Die()
    {
        DataManager.instance.AddPoint(GameManager.instance.GetTimeMinute() * 40);
        Slime.numOfSlimes = 0;
        DataManager.instance.Save();
        SceneManager.LoadScene("Start");
    }
}
