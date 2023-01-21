using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Leaderboard.LeaderboardEntry;

public class UIManager : Instancable<UIManager>
{
    public static event Action OnPopUpRequest;
    public static event Action OnPopUpClosed;
    [SerializeField] private GameObject leaderboardPanel;
    public RectTransform leaderboardEntryRectTransform;
    public GameObject content;
    [SerializeField] LeaderboardAPI leaderboardAPI;
    public List<string> entryNicknames;
    public int scrollOrder;
    public List<LeaderboardEntryModel> leaderboardEntryModels;
    public CanvasGroup canvasGroup;
    


    public void PopUpShow()
    {
        //OnPopUpRequest?.Invoke();
        //leaderboardPanel.SetActive(true);
        float alpha = 0;
        DOTween.To(() => alpha, x => alpha = x, 1, 0.5f)
            .OnUpdate(() =>
            {
                canvasGroup.alpha = alpha;
            }).OnComplete(() =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        leaderboardPanel.transform.DOScale(1.2f, 0.5f).OnComplete(() =>
        {
            leaderboardPanel.transform.DOScale(1f, 0.5f);
            // canvasGroup.interactable = true;
            // canvasGroup.blocksRaycasts = true;
        });
    }

    public void PopUpClose()
    {
        float alpha = 1;
        DOTween.To(() => alpha, x => alpha = x, 0, 0.5f)
            .OnUpdate(() =>
            {
                canvasGroup.alpha = alpha;
            }).OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        OnPopUpClosed?.Invoke();
        //leaderboardPanel.SetActive(false);
        
        leaderboardPanel.transform.DOScale(0f, 0.5f).OnComplete(() =>
        {
            
        });
    }
}
