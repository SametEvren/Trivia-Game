using System;
using Leaderboard;
using Leaderboard.LeaderboardEntry;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScroll : Instancable<InfiniteScroll>, IBeginDragHandler, IDragHandler
{
    public event Action<LeaderboardEntryView,bool> OnNewItem;
    #region Private Members

    [SerializeField]
    private ScrollContent scrollContent;

    private ScrollRect scrollRect;
    
    private Vector2 lastDragPosition;
    
    private bool positiveDrag;

    private const float MinimumScrollValue = -250f;
    private const float ExtraMargin = 250f;

    public bool completelyOpen;

    [SerializeField]private Transform upLimit, downLimit;

    private float _upThreshold = 70f;
    private float _downThreshold = -50f;
    #endregion

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.vertical = true;
        scrollRect.horizontal = false;
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPosition = eventData.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        positiveDrag = eventData.position.y > lastDragPosition.y;
        lastDragPosition = eventData.position;
    }

    public void OnViewScroll()
    {
        HandleVerticalScroll();
    }
    
    private void HandleVerticalScroll()
    {
        int currItemIndex = positiveDrag ?  0 : scrollRect.content.childCount - 1;
        
        var currItem = scrollRect.content.GetChild(currItemIndex);
        
        var maxScrollValue =
            UIManager.Instance.leaderboardEntryRectTransform.rect.height * LeaderboardAPI.leaderboardData.data.Count + ExtraMargin;
        
        scrollRect.content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,
            Mathf.Clamp(scrollRect.content.GetComponent<RectTransform>().anchoredPosition.y, MinimumScrollValue, maxScrollValue));
        
        var leaderboardEntryView = currItem.GetComponent<LeaderboardEntryView>();
        
        if (positiveDrag)
        {
            if (leaderboardEntryView.rankText.text == LeaderboardAPI.leaderboardData.data[^LeaderboardController.ScrollViewItemAmount].rank.ToString())
                return;
        }
        else
        {
            if (leaderboardEntryView.rankText.text == LeaderboardAPI.leaderboardData.data[LeaderboardController.ScrollViewItemAmount-1].rank.ToString())
                return;
        }
        
        if (!ReachedThreshold(currItem) || !completelyOpen)
        {
            return;
        }
        
        int endItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (positiveDrag) 
        {
            newPos.y = endItem.position.y - scrollContent.ChildHeight * scrollContent.AdjustMultiplier * 1f - scrollContent.ItemSpacing;
        }
        else 
        {
            newPos.y = endItem.position.y + scrollContent.ChildHeight * scrollContent.AdjustMultiplier * 1f + scrollContent.ItemSpacing;
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
        OnNewItem?.Invoke(leaderboardEntryView,positiveDrag);
    }
    
    private bool ReachedThreshold(Transform item)
    {
        if (positiveDrag)
            return Mathf.Abs(item.position.y - upLimit.position.y) < _upThreshold;
        
        return item.position.y - downLimit.position.y < _downThreshold;
        
    }
}
