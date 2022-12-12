using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RewardManager : MonoBehaviour
{
    public static RewardManager instance;


    [SerializeField]
    private ObjectPool _pool;


    /*
     * Choice 저장 리스트.
     * 처음엔 unObtained에 있다가, 배우면 obtained로, 마스터하면 mastered로 간다.
     * 대충 7:3 비율로 배운거:안배운거 나옴.
     */
    [SerializeField]
    private List<int> unObtained = new List<int>();
    [SerializeField]
    private List<int> obtained = new List<int>();
    [SerializeField]
    private List<int> mastered = new List<int>();

    private List<int> unObtainedStat = new List<int>();
    private List<int> obtainedStat = new List<int>();
    private List<int> masteredStat = new List<int>();

    public int GetObtainedCount(bool stat = false)
    {
        if (!stat) return obtained.Count;
        else return obtainedStat.Count;
    }
    // 획득한 순서대로 고정되는 리스트 (레벨 표시 UI전용)

    public List<int> obtainedOrdered = new List<int>();
    public List<int> obtainedStatOrdered = new List<int>();

    [SerializeField] private List<int> evolved = new List<int>();
    
    
    private void Awake()
    {
        if (!instance)
            instance = this;
        
        choicePanel.gameObject.SetActive(false);
        pointButton.gameObject.SetActive(false);

        _buttons[0] = choiceButton1;
        _buttons[1] = choiceButton2;
        _buttons[2] = choiceButton3;
    }

    private void Start()
    {
        unObtained.Add(SkillKey.SKILL_ATTACK_AUTO);
        unObtained.Add(SkillKey.SKILL_ATTACK_SATELLITE);
        unObtained.Add(SkillKey.SKILL_ATTACK_TOXIC);
        unObtained.Add(SkillKey.SKILL_ATTACK_SKULL);
        unObtained.Add(SkillKey.SKILL_ATTACK_VISION);
        unObtained.Add(SkillKey.SKILL_ATTACK_SLASH);
        unObtained.Add(SkillKey.SKILL_ATTACK_DAEDALUS);
        unObtained.Add(SkillKey.SKILL_ATTACK_QUAKE);
        
        unObtainedStat.Add(SkillKey.STAT_DMG);
        unObtainedStat.Add(SkillKey.STAT_SPD);
        unObtainedStat.Add(SkillKey.STAT_PRJ_SPD);
        unObtainedStat.Add(SkillKey.STAT_COOL);
        unObtainedStat.Add(SkillKey.STAT_SIZE);
        unObtainedStat.Add(SkillKey.STAT_EXP_COEF);
        unObtainedStat.Add(SkillKey.STAT_OBTAIN_RANGE);
        unObtainedStat.Add(SkillKey.STAT_RECOVER);

        unObtained.Remove(CharacterManager.instance.GetSkillKey());
        setSkillChoiced(CharacterManager.instance.GetSkillKey());
        
    }

    public void AddPrjNumToUnobtained()
    {
        unObtainedStat.Add(SkillKey.STAT_PRJ_NUM);
    }

    [SerializeField] private GameObject foodPrefab;

    public void OnKilledNormalSlime(Vector2 pos, float hp = 0)
    {
        if(ConstMethods.GetRandomResult(45))
        {
            GameObject exp;
            if (hp >= 15 && ConstMethods.GetRandomResult(50))
            {
                exp = _pool.Pop(ObjectType.EXP5);
                if (ConstMethods.GetRandomResult(1))
                {
                    Instantiate(foodPrefab, pos, Quaternion.identity);
                }
            }
            else
            {
                exp = _pool.Pop(ObjectType.EXP1);
            }

            exp.transform.position = pos;
        }
        
    }

    [SerializeField] private GameObject chestPrefab;
    public void OnKilledBossSlime(Vector2 pos)
    {
        if(ConstMethods.GetRandomResult(40))
        {
            GameObject go = Instantiate(chestPrefab, pos, Quaternion.identity, null);
        }
        else
        {
            Instantiate(foodPrefab, pos, Quaternion.identity);
        }
    }

    public void OnKilledGoldSlime(Vector2 pos)
    {
        GameObject food = Instantiate(foodPrefab, pos, Quaternion.identity);

        int n = Random.Range(7, 11);
        for (int i = 0; i < n; i++)
        {
            float a = Random.Range(0, 1f);
            float b = Random.Range(0, 1f);
            Vector2 pos2 = new Vector2(pos.x + a, pos.y + b);

            if (ConstMethods.GetRandomResult(10))
            {
                GameObject exp = ObjectPool.instance.Pop(ObjectType.EXP25);
                exp.transform.position = pos2;
            }
            else if (ConstMethods.GetRandomResult(30))
            {
                GameObject exp = ObjectPool.instance.Pop(ObjectType.EXP5);
                exp.transform.position = pos2;
            }
            else
            {
                GameObject exp = ObjectPool.instance.Pop(ObjectType.EXP1);
                exp.transform.position = pos2;
            }
        }
    }

    [SerializeField, Header("LevelUpChoice")] private Button pointButton;
    [SerializeField] private Image pointButtonImage;
    [SerializeField] private RectTransform choicePanel;
    [SerializeField] private ChoiceButton choiceButton1, choiceButton2, choiceButton3;
    private ChoiceButton[] _buttons = new ChoiceButton[3];
    public void EnablePointButton()
    {
        StartCoroutine(waitAndEnablePointButton());
    }

    IEnumerator waitAndEnablePointButton()
    {
        Vector2 pos = pointButton.transform.position;
        pointButton.transform.DOMoveY(pos.y - 20, 0);
        
        pointButton.gameObject.SetActive(true);
        pointButton.transform.DOMoveY(pos.y, 0.5f);
        pointButtonImage.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        
        pointButton.onClick.AddListener(ShowChoices);
    }

    public void DisablePointButton()
    {
        pointButton.gameObject.SetActive(false);
        pointButtonImage.color = new Color(0.8679245f, 0.8133519f, 0, 0);
        pointButton.onClick.RemoveAllListeners();
    }

    public void MovePointButton(bool remove)
    {
        if (remove)
            pointButton.transform.position += new Vector3(100, 0, 0);
        else pointButton.transform.position -= new Vector3(100, 0, 0);
    }
    
    //중복 방지를 위해 임시로 뽑아두는 배열.
    //선택된 후엔 선택된 index 이외에는 다시 돌려두고 초기화한다.
    private int[] tempChoicedSkills = {0,0,0};

    //업그레이드 버튼 눌렀을 때
    public void ShowChoices()
    {
        Time.timeScale = 0;
        
        /*
         * 한 게임에서 각각 최대 5종류의 공격 스킬, 5개의 스탯 증가를 찍을 수 있다.
         * 즉, obtained.Count + mastered.Count >= 5일 시 새로운 공격 스킬은 안 나오는 형식.
         */
        
        
        
        for(int i = 0; i < 3; i ++)
        {
            int choiced = -1;
            //
            choiced = getSkillChoiced();
            //
            /*
            if (unObtained.Count > 0 && obtained.Count > 0) //둘다 있을땐 50%로 obtained중 1개.
            {
                if (ConstMethods.GetRandomResult(50))
                {
                    choiced = obtained[Random.Range(0, obtained.Count)];
                    obtained.Remove(choiced);
                }
                else
                {
                    choiced = unObtained[Random.Range(0, unObtained.Count)];
                    unObtained.Remove(choiced);
                }
            }
            else if (unObtained.Count > 0 && obtained.Count <= 0) //unObtained만 있을 땐
            {
                choiced = unObtained[Random.Range(0, unObtained.Count)];
                unObtained.Remove(choiced);
            }
            else if (unObtained.Count <= 0 && obtained.Count > 0) //obtained에만 있을 때
            {
                choiced = obtained[Random.Range(0, obtained.Count)];
                obtained.Remove(choiced);
            }
            else
            {
                print("no remaining skill!");
            }

*/
            
            tempChoicedSkills[i] = choiced;

            string t_name = TextManager.instance.GetSkillName(choiced);
            string t_desc =
                TextManager.instance.GetSkillDesc(choiced, PlayerStatManager.instance.GetSkillLevel(choiced));

            if (choiced >= 100 && PlayerStatManager.instance.GetSkillLevel(choiced) == 0)
            {
                t_desc = $"New!\n{t_desc}";
            }
            _buttons[i].SetChoiceButton(t_name,t_desc);
            
        }
        
        choicePanel.gameObject.SetActive(true);
        DisablePointButton();
    }

    //보여줄 스킬을 뽑음.
    int getSkillChoiced()
    {
        int res = -1;
        /*
         * unObtained = 0, obtained = 1, unObtainedStat = 2, obtainedStat = 3;
         */
        List<int> availables = new List<int>();

        if (unObtained.Count > 0) availables.Add(0);
        if (obtained.Count > 0) availables.Add(1);
        if (unObtainedStat.Count > 0) availables.Add(2);
        if (obtainedStat.Count > 0) availables.Add(3);

        if (availables.Count > 0)
        {
            int r = availables[Random.Range(0, availables.Count)];
            switch (r)
            {
                case 0:
                    res = unObtained[Random.Range(0, unObtained.Count)];
                    unObtained.Remove(res);
                    break;
                case 1:
                    res = obtained[Random.Range(0, obtained.Count)];
                    obtained.Remove(res);
                    break;
                case 2:
                    res = unObtainedStat[Random.Range(0, unObtainedStat.Count)];
                    unObtainedStat.Remove(res);
                    break;
                case 3:
                    res = obtainedStat[Random.Range(0, obtainedStat.Count)];
                    obtainedStat.Remove(res);
                    break;
            }
        }
        
        return res;
    }

    //선택지를 골랐을 때
    public void OnChoiced(int index)
    {
        switch (index)
        {
            case 0:
                setSkillChoiced(tempChoicedSkills[0]);
                returnSkillChoiced(tempChoicedSkills[1]);
                returnSkillChoiced(tempChoicedSkills[2]);
                break;
            case 1:
                returnSkillChoiced(tempChoicedSkills[0]);
                setSkillChoiced(tempChoicedSkills[1]);
                returnSkillChoiced(tempChoicedSkills[2]);
                break;
            case 2:
                returnSkillChoiced(tempChoicedSkills[0]);
                returnSkillChoiced(tempChoicedSkills[1]);
                setSkillChoiced(tempChoicedSkills[2]);
                break;
        }
        tempChoicedSkills[0] = 0;
        tempChoicedSkills[1] = 0;
        tempChoicedSkills[2] = 0;
        
        choiceButton1.GetComponent<Button>().onClick.RemoveAllListeners();
        choiceButton2.GetComponent<Button>().onClick.RemoveAllListeners();
        choiceButton3.GetComponent<Button>().onClick.RemoveAllListeners();
        
        Time.timeScale = 1;
        choicePanel.gameObject.SetActive(false);
        //다시 PSM으로 넘겨 반복할지 그만할지 결정
        PlayerStatManager.instance.PointUsed();

        
        if (obtained.Count + mastered.Count >= 5)
        {
            unObtained.Clear();
        }

        if (obtainedStat.Count + masteredStat.Count >= 5)
        {
            unObtainedStat.Clear();
        }
    }

    //선택된 스킬을 정리함.
    private void setSkillChoiced(int skill)
    {
        if (skill == -1 || skill == 0) return;
        int lvl = PlayerStatManager.instance.GetSkillLevel(skill);
        int maxLvl = PlayerStatManager.instance.GetSkillMaxLevel(skill);
        
        //0레벨이었다면 새롭게 획득한다.
        if (lvl == 0)
        {
            
            //공격 스킬이라면 스킬매니저에 전달.
            if(skill < 100)
            {
                if(!obtained.Contains(skill))
                    obtained.Add(skill);
                SkillManager.instance.ObtainSkill(skill);
                obtainedOrdered.Add(skill);
            }
            //스탯 스킬이라면 PSM에 전달.
            else
            {
                if(!obtainedStat.Contains(skill))
                    obtainedStat.Add(skill);
                PlayerStatManager.instance.UpgradeSkill(skill);
                obtainedStatOrdered.Add(skill);
            }
        }
        else
        {
            //max-1레벨이었다면 마스터한다. (지금은 공격 스킬만 mastered에 저장)
            if (lvl == maxLvl - 1)
            {
                if (skill < 100)
                {
                    if(!mastered.Contains(skill))
                        mastered.Add(skill);
                }
                else
                {
                    if(!masteredStat.Contains(skill))
                        masteredStat.Add(skill);
                }

            }
            else
            {
                if (skill < 100)
                {
                    if(!obtained.Contains(skill))
                        obtained.Add(skill);
                }
                else
                {
                    if(!obtainedStat.Contains(skill))
                        obtainedStat.Add(skill);
                }
            }
            //공격 스킬일 경우
            if(skill < 100)
                SkillManager.instance.UpgradeSkill(skill);
            else
            {
                PlayerStatManager.instance.UpgradeSkill(skill);
            }
        }
        PlayerStatManager.instance.AddSkillLevel(skill);


    }
    
    //선택되지 않은 스킬은 제자리로.
    private void returnSkillChoiced(int skill)
    {
        if (skill == -1 || skill == 0) return;
        
        if(PlayerStatManager.instance.GetSkillLevel(skill) == 0)
        {
            if (skill < 100)
            {
                if(!unObtained.Contains(skill))
                    unObtained.Add(skill);
            }
            else
            {
                if(!unObtainedStat.Contains(skill))
                    unObtainedStat.Add(skill);
            }
        }
        else
        {
            if (skill < 100)
            {
                if(!obtained.Contains(skill))obtained.Add(skill);
            }
            else
            {
                if(!obtainedStat.Contains(skill)) obtainedStat.Add(skill);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ObtainChest()
    {
        //마스터한 스킬이 없을 땐
        if (mastered.Count == 0)
        {
            //획득한 스킬조차 0개라면 (그럴 일은 없겠지만)
            if (obtained.Count == 0)
            {
                return;
            }

            int skill = obtained[Random.Range(0, obtained.Count)];
            obtained.Remove(skill);
            setSkillChoiced(skill);
            
            UnityEventManager.instance.ShowUIText(TextManager.instance.GetSkillLevelUpText(skill));
            
        }
        //있을 땐 랜덤 진화.
        else
        {
            int skill = mastered[Random.Range(0, mastered.Count)];
            mastered.Remove(skill);
            evolved.Add(skill);
            SkillManager.instance.EvolveSkill(skill);
            
            UnityEventManager.instance.ShowUIText(TextManager.instance.GetSkillEvolveText(skill));
        }
    }
}
