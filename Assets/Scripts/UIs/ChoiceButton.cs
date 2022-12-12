using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpName, tmpDesc;

    public void SetChoiceButton(string name, string desc)
    {
        tmpName.text = name;
        tmpDesc.text = desc;
    }
}
