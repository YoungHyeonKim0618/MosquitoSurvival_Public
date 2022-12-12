using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.UIElements;


interface AttackSkill
{
    void UpgradeSkill();
}

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public AutoAttack AutoAttack;
    public Satellite Satellite;
    public ToxicFloor ToxicFloor;
    public SkullBomb SkullBomb;
    public FlashVision FlashVision;
    public BlinkSlash BlinkSlash;
    public Daedalus Daedalus;
    public InnerQuake InnerQuake;

    private List<bool> isEvolved = new List<bool>();

    public bool isSkillEvolved(int skill)
    {
        switch (skill)
        {
            case SkillKey.SKILL_ATTACK_AUTO:
                return isEvolved[0];
                break;
            case SkillKey.SKILL_ATTACK_SATELLITE:
                return isEvolved[1];
                break;
            case SkillKey.SKILL_ATTACK_TOXIC:
                break;
            case SkillKey.SKILL_ATTACK_SKULL:
                return isEvolved[3];
                break;
            case SkillKey.SKILL_ATTACK_VISION:
                return isEvolved[4];
                break;
            case SkillKey.SKILL_ATTACK_SLASH:
                return isEvolved[5];
                break;
            case SkillKey.SKILL_ATTACK_GATE:
                break;
            case SkillKey.SKILL_ATTACK_DAEDALUS:
                return isEvolved[7];
                break;
            case SkillKey.SKILL_ATTACK_QUAKE:
                return isEvolved[8];
                break;
        }

        return false;
    }

    private void Awake()
    {
        if (!instance)
            instance = this;
        
        AutoAttack.gameObject.SetActive(false);
        Satellite.gameObject.SetActive(false);

        for (int i = 0; i < 9; i++)
        {
            isEvolved.Add(false);
        }
    }

    private void Start()
    {
    }

    public void ObtainSkill(int skill)
    {
        switch (skill)
        {
            case SkillKey.SKILL_ATTACK_AUTO:
                AutoAttack.transform.SetParent(GameObject.FindWithTag("Player").transform);
                AutoAttack.transform.localPosition = Vector3.zero;
                AutoAttack.gameObject.SetActive(true);
                AutoAttack.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_SATELLITE:
                Satellite.transform.SetParent(GameObject.FindWithTag("Player").transform);
                Satellite.transform.localPosition = Vector3.zero;
                Satellite.gameObject.SetActive(true);
                Satellite.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_TOXIC:
                ToxicFloor.transform.SetParent(GameObject.FindWithTag("Player").transform);
                ToxicFloor.transform.localPosition = Vector3.zero;
                ToxicFloor.gameObject.SetActive(true);
                ToxicFloor.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_SKULL:
                SkullBomb.transform.SetParent(GameObject.FindWithTag("Player").transform);
                SkullBomb.transform.localPosition = Vector3.zero;
                SkullBomb.gameObject.SetActive(true);
                SkullBomb.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_VISION:
                FlashVision.transform.SetParent(GameObject.FindWithTag("Player").transform);
                FlashVision.transform.localPosition = Vector3.zero;
                FlashVision.gameObject.SetActive(true);
                FlashVision.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_SLASH:
                BlinkSlash.transform.SetParent(GameObject.FindWithTag("Player").transform);
                BlinkSlash.transform.localPosition = Vector3.zero;
                BlinkSlash.gameObject.SetActive(true);
                BlinkSlash.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_GATE:
                break;
            case SkillKey.SKILL_ATTACK_DAEDALUS:
                Daedalus.transform.SetParent(GameObject.FindWithTag("Player").transform);
                Daedalus.transform.localPosition = Vector3.zero;
                Daedalus.gameObject.SetActive(true);
                Daedalus.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_QUAKE:
                InnerQuake.transform.SetParent(GameObject.FindWithTag("Player").transform);
                InnerQuake.transform.localPosition = Vector3.zero;
                InnerQuake.gameObject.SetActive(true);
                InnerQuake.UpgradeSkill();
                break;
        }
    }
    public void UpgradeSkill(int skill){
        switch (skill)
        {
            case SkillKey.SKILL_ATTACK_AUTO:
                AutoAttack.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_SATELLITE:
                Satellite.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_TOXIC:
                ToxicFloor.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_SKULL:
                SkullBomb.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_VISION:
                FlashVision.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_SLASH:
                BlinkSlash.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_DAEDALUS:
                Daedalus.UpgradeSkill();
                break;
            case SkillKey.SKILL_ATTACK_QUAKE:
                InnerQuake.UpgradeSkill();
                break;
                
        }
        
    }

    public void EvolveSkill(int skill)
    {
        //최대 레벨이 아니라면 리턴.
        if (PlayerStatManager.instance.GetSkillLevel(skill) !=
            PlayerStatManager.instance.GetSkillMaxLevel(skill)) return;

        switch (skill)
        {
            case SkillKey.SKILL_ATTACK_AUTO:
                AutoAttack.Evolve();
                isEvolved[0] = true;
                break;
            case SkillKey.SKILL_ATTACK_SATELLITE:
                Satellite.Evolve();
                isEvolved[1] = true;
                break;
            case  SkillKey.SKILL_ATTACK_SKULL:
                SkullBomb.Evolve();
                isEvolved[3] = true;
                break;
            case SkillKey.SKILL_ATTACK_VISION:
                FlashVision.Evolve();
                isEvolved[4] = true;
                break;
            case SkillKey.SKILL_ATTACK_SLASH:
                BlinkSlash.Evolve();
                isEvolved[5] = true;
                break;
            case SkillKey.SKILL_ATTACK_DAEDALUS:
                Daedalus.Evolve();
                isEvolved[7] = true;
                break;
            case SkillKey.SKILL_ATTACK_QUAKE:
                InnerQuake.Evolve();
                isEvolved[8] = true;
                break;
        }
    }
}
public static class SkillKey
{
    public const int SKILL_ATTACK_AUTO = 1;
    public const int SKILL_ATTACK_SATELLITE = 2;
    public const int SKILL_ATTACK_TOXIC = 3;
    public const int SKILL_ATTACK_SKULL = 4;
    public const int SKILL_ATTACK_VISION = 5;
    public const int SKILL_ATTACK_SLASH = 6;
    public const int SKILL_ATTACK_GATE = 7;
    public const int SKILL_ATTACK_DAEDALUS = 8;
    public const int SKILL_ATTACK_QUAKE = 9;

    public const int STAT_DMG = 101;
    public const int STAT_SPD = 102;
    public const int STAT_PRJ_NUM = 103;
    public const int STAT_PRJ_SPD = 104;
    public const int STAT_COOL = 105;
    public const int STAT_SIZE = 106;
    public const int STAT_EXP_COEF = 107;
    public const int STAT_OBTAIN_RANGE = 108;
    public const int STAT_RECOVER = 109;
}
