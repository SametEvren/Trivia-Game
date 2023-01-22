using System;
using System.Collections.Generic;
using System.Linq;
using Leaderboard.LeaderboardEntry;
using UnityEngine;

public class ScrollContent : Instancable<ScrollContent>
{
    #region Public Properties
    public float ItemSpacing => itemSpacing;
    
    public float VerticalMargin => verticalMargin;

    public float ReferenceHeight => referenceHeight;
    
    public float Width => width;
    
    public float Height => height;
    
    public float ChildWidth => childWidth;
    
    public float ChildHeight => childHeight;
    
    public float AdjustMultiplier => Screen.height / referenceHeight;
    #endregion

    #region Private Members
    private RectTransform rectTransform;
    
    private RectTransform[] rtChildren;
    
    private float width, height;
    
    private float childWidth, childHeight;
    
    [SerializeField]
    private float itemSpacing;
    
    [SerializeField]
    private float verticalMargin;
    
    public List<LeaderboardEntryView> leaderboardEntryViews;
    public Vector3 topEntryPos, bottomEntryPos;
    [SerializeField] private float referenceHeight = 2160;
    
    #endregion

    private void Start()
    {
        itemSpacing *= AdjustMultiplier;
        verticalMargin *= AdjustMultiplier;
        InitializeEntryViews();
    }

    private void InitializeEntryViews()
    {
        leaderboardEntryViews = GetComponentsInChildren<LeaderboardEntryView>(true).ToList();
        rectTransform = GetComponent<RectTransform>();
        rtChildren = new RectTransform[rectTransform.childCount];

        topEntryPos = leaderboardEntryViews[0].transform.position;
        bottomEntryPos = leaderboardEntryViews[^1].transform.position;
        

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
        }
        
        width = rectTransform.rect.width;
        
        height = rectTransform.rect.height - (2 * verticalMargin);

        childWidth = rtChildren[0].rect.width;
        childHeight = rtChildren[0].rect.height;
        
        InitializeContent();
        
    }
    
    private void InitializeContent()
    {
        float originY = 0 - (height * 0.5f);
        float posOffset = childHeight * 0.5f;
        for (int i = 0; i < rtChildren.Length; i++)
        {
            Vector2 childPos = rtChildren[i].localPosition;
            childPos.y = originY - (posOffset + i * (childHeight + itemSpacing));
            rtChildren[i].localPosition = childPos;
        }
    }

    public void ResetEntryViews()
    {
        leaderboardEntryViews = GetComponentsInChildren<LeaderboardEntryView>(true).ToList();
    }
}
