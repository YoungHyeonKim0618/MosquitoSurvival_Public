using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public static class ObjectTypeUI
{
    public const int DMG_TEXT = 51;
}
public class UIObjectPool : MonoBehaviour
{
    public static UIObjectPool instance;
    
    [SerializeField] private TextMeshProUGUI _dmgTextPrefab;
    private List<TextMeshProUGUI> _dmgTexts = new List<TextMeshProUGUI>();

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    public TextMeshProUGUI Pop(int type)
    {
        TextMeshProUGUI t = null;
        switch (type)
        {
            case ObjectTypeUI.DMG_TEXT:
                if(_dmgTexts.Count == 0) Create(type);
                t = _dmgTexts[0];
                _dmgTexts.Remove(t);
                break;
        }

        t.gameObject.SetActive(true);
        return t;
    }

    public void Push(TextMeshProUGUI t, int type)
    {
        t.DOFade(1, 0);
        t.gameObject.SetActive(false);

        switch (type)
        {
            case ObjectTypeUI.DMG_TEXT:
                _dmgTexts.Add(t);
                break;
        }
    }

    void Create(int type)
    {
        switch (type)
        {
            case ObjectTypeUI.DMG_TEXT:
                for (int i = 0; i < 20; i++)
                {
                    TextMeshProUGUI go = Instantiate(_dmgTextPrefab, null);
                    go.gameObject.SetActive(false);
                    _dmgTexts.Add(go);
                }

                break;
        }
    }
}