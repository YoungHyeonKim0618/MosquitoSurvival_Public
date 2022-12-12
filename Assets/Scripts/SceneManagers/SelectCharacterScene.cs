using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharacterScene : MonoBehaviour
{

    public void OnConfirmButtonClicked()
    {
        
        SceneManager.LoadScene("Game");

    }

    private void Start()
    {
        OnSelectCharacter(0);
        
    }


    [SerializeField] private TextMeshProUGUI tmpCharName;
    [SerializeField]
    private TextMeshProUGUI tmpCharDesc;
    [SerializeField]
    private TextMeshProUGUI tmpAttName;
    [SerializeField]
    private TextMeshProUGUI tmpAttDesc;

    [SerializeField] private Image attributeImage;
    public void OnSelectCharacter(int index)
    {
        CharacterManager.instance.CurCharacter = index;
        int attKey = CharacterManager.instance.GetAttributeKey();

        tmpCharName.text = TextManager.instance.GetCharacterName(index);
        tmpCharDesc.text = TextManager.instance.GetCharacterDesc(index);
        tmpAttName.text = TextManager.instance.GetAttributeName(attKey);
        tmpAttDesc.text = TextManager.instance.GetAttributeDesc(attKey);
        attributeImage.sprite = SpriteManager.instance.GetSpriteByAttKey(CharacterManager.instance.GetAttributeKey());
    }


    void setTmpPenanceDesc(int level)
    {
        string t = "";
        switch (level)
        {
            case 1:
                t = TextManager.instance.GetStringByKey(TextKeys.UI_PENANCE_LVL1_DESC);
                break;
            case 2:
                t = TextManager.instance.GetStringByKey(TextKeys.UI_PENANCE_LVL2_DESC);
                break;
            case 3:
                t = TextManager.instance.GetStringByKey(TextKeys.UI_PENANCE_LVL3_DESC);
                break;
            case 4:
                t = TextManager.instance.GetStringByKey(TextKeys.UI_PENANCE_LVL4_DESC);
                break;
            case 5:
                t = TextManager.instance.GetStringByKey(TextKeys.UI_PENANCE_LVL5_DESC);
                break;
        }

    }

}

