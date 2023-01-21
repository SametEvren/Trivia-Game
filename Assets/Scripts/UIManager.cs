using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Leaderboard.LeaderboardEntry;

public class UIManager : Instancable<UIManager>
{
    [SerializeField] private GameObject leaderboardPanel;
    public RectTransform leaderboardEntryRectTransform;
    public GameObject content;
    [SerializeField] LeaderboardAPI leaderboardAPI;
    public List<string> entryNicknames;
    public int scrollOrder;
    public List<LeaderboardEntryModel> leaderboardEntryModels;


    public void PopUpShow()
    {
        leaderboardPanel.SetActive(true);
        leaderboardPanel.transform.DOScale(1.2f, 0.5f).OnComplete(() =>
        {
            leaderboardPanel.transform.DOScale(1f, 0.5f);
        });
    }

    public void PopUpClose()
    {
        leaderboardPanel.transform.DOScale(0f, 0.5f).OnComplete(() =>
        {
            leaderboardPanel.SetActive(false);
        });
    }

    public void InstantiateLeaderboardEntries()
    {
        
    }
    
}
