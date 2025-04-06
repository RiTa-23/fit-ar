using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();

    public Quest GetQuest(string questID)
    {
        return quests.Find(q => q.questID == questID);
    }

    public void UpdateQuestProgress(string questID, int amount)
    {
        Quest quest = GetQuest(questID);
        if (quest != null && quest.status == QuestStatus.InProgress)
        {
            quest.currentAmount += amount;
            if (quest.IsComplete())
            {
                quest.status = QuestStatus.Completed;
                Debug.Log($"�N�G�X�g '{quest.title}' ���������܂����I");
                GiveCompletionReward(questID);
            }
        }
    }

    private void GiveCompletionReward(string questID)
    {
        if (questID == "LoginBonusQuest")
        {
            Debug.Log("���O�C���{�[�i�X�N�G�X�g�����I��V�Ƃ���500�S�[���h���l�����܂����I");
        }
    }
}

