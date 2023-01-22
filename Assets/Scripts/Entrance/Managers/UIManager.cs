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
        canvasGroup.alpha = 0;

        var popUpSequence = DOTween.Sequence()
            .Append(canvasGroup.DOFade(1, 0.5f))
            .Join(leaderboardPanel.transform.DOScale(1.2f, 0.5f))
            .Append(leaderboardPanel.transform.DOScale(1f, 0.5f))
            .OnComplete(() =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                InfiniteScroll.Instance.completelyOpen = true;
            });

    }

    public void PopUpClose()
    {
        canvasGroup.alpha = 1;
        InfiniteScroll.Instance.completelyOpen = false;
        var popUpSequence = DOTween.Sequence()
            .Append(canvasGroup.DOFade(0, 0.5f))
            .Join(leaderboardPanel.transform.DOScale(0f, 0.5f))
            .OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                OnPopUpClosed?.Invoke();
            });
    }
}
