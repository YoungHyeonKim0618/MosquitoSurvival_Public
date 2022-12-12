using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class NoParamsEvent : UnityEvent{}
public class SlimeEvent : UnityEvent<Slime>{}
public class FloatVectorEvent : UnityEvent<float,Vector2>{}
public class TransformEvent : UnityEvent<Transform>{}

public class UnityEventManager : MonoBehaviour
{
    
    public static UnityEventManager instance;


    [SerializeField] private Camera mainCam;

    //플레이어의 투사체 개수 스탯이 바뀔 때
    public NoParamsEvent OnNumOfPrjChanged = new NoParamsEvent();

    public NoParamsEvent OnSkillSizeChanged = new NoParamsEvent();
    
    //적에게 투사체가 닿았을 때
    public TransformEvent OnPrjHit = new TransformEvent();
    
    //피해 표시.
    public FloatVectorEvent onDamageSlime = new FloatVectorEvent();
    
    private void Awake()
    {
        if (!instance)
            instance = this;
        
        onDamageSlime.AddListener(showDmgText);
        onDamageSlime.AddListener(showHitParticle);
        
        UIText.gameObject.SetActive(false);
        tNumer = waitAndPushUI(UIText,1f);
        defaultUITextPos = UIText.transform.position;
    }

    [SerializeField] private Canvas _canvas;
    private void showDmgText(float dmg, Vector2 pos )
    {
        Vector2 screenPos = mainCam.WorldToScreenPoint(pos + Vector2.up * 1.1f);
        TextMeshProUGUI t = UIObjectPool.instance.Pop(ObjectTypeUI.DMG_TEXT);
        t.transform.SetParent(_canvas.transform);
        t.text = $"{(int) dmg}";
        t.transform.position = screenPos;
        
        t.DOFade(0, 0.5f).SetUpdate(true);
        t.transform.DOMoveY(t.transform.position.y + 10, 0.5f).SetUpdate(true);
        StartCoroutine(waitAndPush(t));
    }

    [SerializeField] private TextMeshProUGUI UIText;

    private Sequence uiTextSeq;
    private IEnumerator tNumer;
    private Vector3 defaultUITextPos;
    public void ShowUIText(string text)
    {
        StopCoroutine(waitAndPushUI(UIText,1f));
        resetUIText();
        
        UIText.text = text;
        UIText.gameObject.SetActive(true);
        uiTextSeq.Append(UIText.DOFade(0, 1f).SetUpdate(true));

        StartCoroutine(waitAndPushUI(UIText,1f));
    }

    IEnumerator waitAndPush(TextMeshProUGUI t, float delay = 0.625f)
    {
        yield return new WaitForSecondsRealtime(delay);
        UIObjectPool.instance.Push(t,ObjectTypeUI.DMG_TEXT);
    }

    IEnumerator waitAndPushUI(TextMeshProUGUI t, float delay = 0.5f)
    {
        print("start coroutine");
        UIText.transform.DOLocalMoveY(UIText.transform.position.y + 1, 1f).SetUpdate(true);
        yield return new WaitForSeconds(delay);
        resetUIText();
    }

    void resetUIText()      
    {
        print(UIText.DOKill());
        UIText.gameObject.SetActive(false);
        uiTextSeq.Append(UIText.DOFade(1, 0));
        UIText.transform.position = defaultUITextPos;
    }

    void showHitParticle(float dmg, Vector2 pos)
    {
        ParticleSystem ps = ObjectPool.instance.PopParticle(pos);
        StartCoroutine(waitAndShowHitParticle(ps));
    }

    IEnumerator waitAndShowHitParticle(ParticleSystem ps)
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPool.instance.PushParticle(ps);
    }
    [SerializeField] private GameObject _neutralExplosion;

    public void NeutralExplode(Vector2 pos)
    {
        Instantiate(_neutralExplosion, pos, Quaternion.identity, null);
    }
}
