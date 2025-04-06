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
                Debug.Log($"クエスト '{quest.title}' が完了しました！");
                GiveCompletionReward(questID);
            }
        }
    }

    private void GiveCompletionReward(string questID)
    {
        if (questID == "LoginBonusQuest")
        {
            Debug.Log("ログインボーナスクエスト完了！報酬として500ゴールドを獲得しました！");
        }
    }
}

