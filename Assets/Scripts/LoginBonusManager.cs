using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginBonusManager : MonoBehaviour
{
    public int consecutiveDays;
    public DateTime lastLoginDate;
    public string[] rewards;
    public Text tmpro;

    void Start()
    {
        consecutiveDays = PlayerPrefs.GetInt("ConsecutiveDays", 0);
        string lastLoginString = PlayerPrefs.GetString("LastLoginDate", DateTime.MinValue.ToString());
        lastLoginDate = DateTime.Parse(lastLoginString);

        CheckLogin();
        test();
    }

    void CheckLogin()
    {
        DateTime currentDate = DateTime.Now.Date;

        if (lastLoginDate.Date < currentDate)
        {
            if ((currentDate - lastLoginDate.Date).Days == 1)
            {
                consecutiveDays++;
            }
            else
            {
                consecutiveDays = 1;
            }

            GiveReward(consecutiveDays);
            lastLoginDate = currentDate;

            PlayerPrefs.SetInt("ConsecutiveDays", consecutiveDays);
            PlayerPrefs.SetString("LastLoginDate", currentDate.ToString());
            PlayerPrefs.Save();
        }
    }

    void GiveReward(int day)
    {
        string reward = rewards[Mathf.Min(day - 1, rewards.Length - 1)];
        Debug.Log($"ログインボーナス: {reward} を獲得しました！");
        tmpro.text = $"ログインボーナス: {reward} を獲得しました！";

        QuestManager questManager = FindObjectOfType<QuestManager>();
        if (questManager != null)
        {
            questManager.UpdateQuestProgress("LoginBonusQuest", 1);
        }
    }

    void test()
    {
        tmpro.text = $"ログインボーナス: {rewards[0]}を獲得しました！";
    }
}
