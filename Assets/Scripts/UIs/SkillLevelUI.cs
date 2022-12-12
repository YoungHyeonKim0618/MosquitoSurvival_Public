using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillLevelUI : MonoBehaviour
{
    [SerializeField] private Image _skillImage;
    [SerializeField] private Image _filledPrefab, _emptyPrefab, _evolvedPrefab;
    [SerializeField] private RectTransform _horizontalLayoutGroup;

    private List<Image> _prefabs = new List<Image>();

    public void SetSkillUI(int skill)
    {
        ResetSkillUI();
        int maxLevel = PlayerStatManager.instance.GetSkillMaxLevel(skill);
        int curLevel = PlayerStatManager.instance.GetSkillLevel(skill);

        _skillImage.sprite = SpriteManager.instance.GetSpriteBySkill(skill);

        if (SkillManager.instance.isSkillEvolved(skill))
        {
            for (int i = 0; i < maxLevel; i++)
            {
                Image img = Instantiate(_evolvedPrefab, _horizontalLayoutGroup);
                _prefabs.Add(img);
            }
            return;
        }
        for (int i = 0; i < curLevel; i++)
        {
            Image img = Instantiate(_filledPrefab, _horizontalLayoutGroup);
            _prefabs.Add(img);
        }

        for (int j = 0; j < (maxLevel - curLevel); j++)
        {
            Image img = Instantiate(_emptyPrefab, _horizontalLayoutGroup);
            _prefabs.Add(img);
        }
    }

    void ResetSkillUI()
    {
        int n = _prefabs.Count;
        for (int i = 0; i < n; i++)
        {
            Destroy(_prefabs[i].gameObject);
        }
        _prefabs.Clear();
    }
}
