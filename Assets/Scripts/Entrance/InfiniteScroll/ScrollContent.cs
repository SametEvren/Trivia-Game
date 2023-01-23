using System.Collections.Generic;
using System.Linq;
using Entrance.Leaderboard.LeaderboardEntry;
using UnityEngine;
using Utility;

namespace Entrance.InfiniteScroll
{
    public class ScrollContent : Instancable<ScrollContent>
    {
        #region Public Properties
        public List<LeaderboardEntryView> leaderboardEntryViews;
        public float ItemSpacing => itemSpacing;
        public float ChildHeight => childHeight;
        public float AdjustMultiplier => Screen.height / referenceHeight;
        #endregion

        #region Private Properties
        private RectTransform rectTransform;
        private RectTransform[] rtChildren;
        private float  height;
        private float childHeight;
        [SerializeField]
        private float itemSpacing;
        [SerializeField]
        private float verticalMargin;
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
        
            for (int i = 0; i < rectTransform.childCount; i++)
            {
                rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
            }

            height = rectTransform.rect.height - (2 * verticalMargin);
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
}
