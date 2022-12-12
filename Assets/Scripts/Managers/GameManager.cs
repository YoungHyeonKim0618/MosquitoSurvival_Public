using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


public static class ConstMethods
{
    public static void Swap(Object o1, Object o2)
    {
        if (o1 != o2)
        {
            var t = o1;
            o1 = o2;
            o2 = t;
        }
    }

    public static bool GetRandomResult(int i)
    {
        int n = Random.Range(0, 100);
        if (n < i) return true;
        return false;
    }

    public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
    {
            return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
    }

    public static string GetColoredString(string text, bool isGreen = true)
    {
        return isGreen ? $"<color=#66EA2E>{text}</color>" : $"<color=#CF2020>{text}</color>";
    }
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private int _timeMinute;

    public int GetTimeMinute()
    {
        return _timeMinute;
        
    }
    private int _timeSeconds;
    

    public int _TimeSeconds
    {
        get { return _timeSeconds;}
        set
        {
            if (value >= 60)
            {
                _timeMinute++;
                if(_timeMinute >= 20) ClearGame();
                _timeSeconds = 0;
            }
            else
            {
                _timeSeconds++;
            }

            tmpTimer.text = $"{_timeMinute} : {$"{_timeSeconds:0#}"}";
        }
    }
    [SerializeField] private TextMeshProUGUI tmpTimer;
    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    private void Start()
    {
        _pausePanel.gameObject.SetActive(false);
        StartCoroutine(waitAndTimer());
        
        CharacterManager.instance.SetCharacterStat();
    }


    IEnumerator waitAndTimer()
    {
        WaitForSeconds ws = new WaitForSeconds(1f);
        while (true)
        {
            yield return ws;
            _TimeSeconds++;
        }
    }

    [SerializeField] private RectTransform _pausePanel;

    public void Pause()
    {
        Time.timeScale = 0;
        _pausePanel.gameObject.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        _pausePanel.gameObject.SetActive(false);
    }

    [SerializeField] private Button _pauseButton;
    public void SetPauseButton(bool disabled)
    {
        if(disabled) _pauseButton.gameObject.SetActive(false);
        else _pauseButton.gameObject.SetActive(true);
    }

    void ClearGame()
    {
        
        DataManager.instance.AddPoint(1500);
        
        DataManager.instance.Save();
        SceneManager.LoadScene("Main");
    }

}
