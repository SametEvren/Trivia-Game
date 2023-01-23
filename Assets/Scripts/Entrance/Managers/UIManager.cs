using UnityEngine;
using DG.Tweening;
using Entrance.InfiniteScroll;
using Utility;

public class UIManager : Instancable<UIManager>
{
    #region Public Properties
    public CanvasGroup canvasGroup;
    public RectTransform leaderboardEntryRectTransform;
    #endregion
    
    #region Private Properties
    [SerializeField] private GameObject leaderboardPanel;
    #endregion
    
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
            });
    }
}
