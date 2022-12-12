using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeManager : MonoBehaviour
{
    public static AttributeManager instance;


    private float maxCool;
    private float curCool;
    [SerializeField] private Image coolImage;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    [SerializeField] private Image attButtonImage;
    private void Start()
    {
        attButtonImage.sprite = SpriteManager.instance.GetSpriteByAttKey(CharacterManager.instance.GetAttributeKey());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            OnActive();
    }

    private void FixedUpdate()
    {
        if (curCool > 0)
        {
            curCool -= Time.fixedUnscaledDeltaTime;
            coolImage.fillAmount = curCool / maxCool;
        }
    }


    /// <summary>
    /// 시작 시 어트리뷰트 키를 받아 효과를 실행하는 함수.
    /// </summary>
    public void ApplyAttributePassive(int attribute)
    {
        
    }

    /// <summary>
    /// 버튼 클릭 시 어트리뷰트 키를 받아 액티브를 실행하는 함수.
    /// </summary>
     void OnAttributeActive(int attribute)
    {
        if (curCool > 0) return;
        
        switch (attribute)
        {
            case AttributeKey.EXPERIENCE:
                maxCool = 15;
                curCool = 15;
                PlayerStatManager.instance.ObtainEXP(3 * GameManager.instance.GetTimeMinute() + 2.5f);
                break;
            case AttributeKey.ACCELERATION:
                maxCool = 9;
                curCool = 9;
                StartCoroutine(waitAndAcceleration());
                break;
            case AttributeKey.RAMPAGE:
                maxCool = 12;
                curCool = 12;
                StartCoroutine(waitAndRampage());
                break;
            case AttributeKey.HASTE:
                maxCool = 6;
                curCool = 6;
                StartCoroutine(waitAndHaste());
                break;
                
            default:
                break;
        }
    }

    IEnumerator waitAndAcceleration()
    {
        PlayerStatManager.instance.Speed += 30;
        PlayerStatManager.instance.PrjSpeed += 50;
        yield return new WaitForSeconds(3f);
        PlayerStatManager.instance.Speed -= 30;
        PlayerStatManager.instance.PrjSpeed -= 50;
    }

    IEnumerator waitAndRampage()
    {
        PlayerStatManager.instance.CooldownDecrement += 30;
        yield return new WaitForSeconds(5f); 
        PlayerStatManager.instance.CooldownDecrement -= 30;
    }

    IEnumerator waitAndHaste()
    {
        PlayerStatManager.instance.Speed += 110;
        PlayerController.instance.AddGhostTime(0.75f);
        yield return new WaitForSeconds(0.625f);
        
        PlayerStatManager.instance.Speed -= 110;
    }

    public void OnActive()
    {
        OnAttributeActive(CharacterManager.instance.GetAttributeKey());
    }

    [SerializeField] private Button attributeButton;

    public void SetActiveButton(bool t)
    {
        attributeButton.gameObject.SetActive(t);
    }

}

public static class AttributeKey
{
    public const int EXPERIENCE = 0;
    public const int ACCELERATION = 1;
    public const int RAMPAGE = 2;
    public const int HASTE = 3;
}