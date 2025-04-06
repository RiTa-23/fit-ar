using UnityEngine;

public enum QuestStatus { InProgress, Completed }

[System.Serializable]
public class Quest
{
    public string questID;
    public string title;
    public string description;
    public int goalAmount;
    public int currentAmount;
    public QuestStatus status = QuestStatus.InProgress;

    public bool IsComplete()
    {
        return currentAmount >= goalAmount;
    }
}
