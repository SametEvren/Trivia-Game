using System;
using System.Collections.Generic;
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
    
    [SerializeField]
    private float outOfBoundsThreshold;
    
    private InfiniteScrollRect scrollRect;
    
    private Vector2 lastDragPosition;
    
    private bool positiveDrag;

    private const float MinimumScrollValue = -250f;
    
    #endregion

    private void Start()
    {
        scrollRect = GetComponent<InfiniteScrollRect>();
        scrollRect.vertical = true;
        scrollRect.horizontal = false;
        scrollRect.movementType = InfiniteScrollRect.MovementType.Unrestricted;
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
            UIManager.Instance.leaderboardEntryRectTransform.rect.height * LeaderboardAPI.leaderboardData.data.Count;
        
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
        
        
        if (!ReachedThreshold(currItem))
        {
            return;
        }
        
        

        int endItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (positiveDrag) //mouse upwards
        {
            newPos.y = endItem.position.y - scrollContent.ChildHeight * 1.5f + scrollContent.ItemSpacing;
        }
        else //mouse downwards
        {
            newPos.y = endItem.position.y + scrollContent.ChildHeight * 1.5f - scrollContent.ItemSpacing;
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
        OnNewItem?.Invoke(leaderboardEntryView,positiveDrag);
    }
    
    
    private bool ReachedThreshold(Transform item)
    {
        float posYThreshold = transform.position.y + scrollContent.Height * 0.5f + outOfBoundsThreshold;
        float negYThreshold = transform.position.y - scrollContent.Height * 0.5f - outOfBoundsThreshold;
        return positiveDrag ? item.position.y - scrollContent.ChildWidth * 0.5f > posYThreshold :
            item.position.y + scrollContent.ChildWidth * 0.5f < negYThreshold;
    }

}
