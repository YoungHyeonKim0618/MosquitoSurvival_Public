using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI tmpStartGame;
    [SerializeField] private TextMeshProUGUI tmpUpgrade;

    private void Awake()
    {
        tmpStartGame.text = TextManager.instance.GetStringByKey(TextKeys.UI_MAIN_START_GAME);
        tmpUpgrade.text = TextManager.instance.GetStringByKey(TextKeys.UI_MAIN_UPGRADE);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SelectCharacter");
    }

    public void GotoUpgradeScene()
    {
        SceneManager.LoadScene("Upgrade");
    }

    public void ResetData()
    {
        DataManager.instance.ResetData();
    }
}
