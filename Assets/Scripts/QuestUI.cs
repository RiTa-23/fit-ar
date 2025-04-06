using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public Text questTitleText;
    public Text questDescriptionText;
    public Text questProgressText;
    public QuestManager questManager;

    void Update()
    {
        Quest loginQuest = questManager.GetQuest("LoginBonusQuest");
        if (loginQuest != null)
        {
            questTitleText.text = loginQuest.title;
            questDescriptionText.text = loginQuest.description;
            questProgressText.text = $"êiçsèÛãµ: {loginQuest.currentAmount}/{loginQuest.goalAmount}";
        }
    }
}

