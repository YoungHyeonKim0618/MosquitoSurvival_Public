using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField]
    private List<SkillLevelUI> _skillUIs = new List<SkillLevelUI>();
    [SerializeField]
    private List<SkillLevelUI> _statUIs = new List<SkillLevelUI>();
    private void OnEnable()
    {
        for (int i = 0; i < RewardManager.instance.obtainedOrdered.Count; i++)
        {
            _skillUIs[i].SetSkillUI(RewardManager.instance.obtainedOrdered[i]);
        }

        for (int i = 0; i < RewardManager.instance.obtainedStatOrdered.Count; i++)
        {
            _statUIs[i].SetSkillUI(RewardManager.instance.obtainedStatOrdered[i]);
        }
    }
}
