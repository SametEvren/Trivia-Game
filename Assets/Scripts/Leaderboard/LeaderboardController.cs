using System;
using System.Collections;
using Leaderboard.LeaderboardEntry;
using UnityEngine;

namespace Leaderboard
{
    public class LeaderboardController : MonoBehaviour
    {
        public const int ScrollViewItemAmount = 5;
        private int topIndex = 1;

        private int TopIndex
        {
            get => topIndex;
            set => topIndex = Mathf.Clamp(value,1,LeaderboardAPI.leaderboardData.data.Count);
        }

        private int BottomIndex => topIndex + ScrollViewItemAmount;

        public void OnClickedLeaderboardButton()
        {
            StartCoroutine(GetLeaderboard());
        }
        private IEnumerator GetLeaderboard()
        {
            LeaderboardAPI.Instance.UpdateLeaderboardData();
            
            yield return new WaitUntil(() => LeaderboardAPI.Status != LeaderboardAPI.APIStatus.Empty);
            
            if (LeaderboardAPI.Status == LeaderboardAPI.APIStatus.Error)
                yield break;
            
            InfiniteScroll.Instance.OnNewItem -= HandleScrollItemChange;
            InfiniteScroll.Instance.OnNewItem += HandleScrollItemChange;
            yield return new WaitForSeconds(1f);
            ShowLeaderboard();
            UIManager.Instance.PopUpShow();
        }

        public void ShowLeaderboard()
        {
            if (LeaderboardAPI.Status != LeaderboardAPI.APIStatus.Ready)
                return;

            ScrollContent.Instance.ResetEntryViews();
            for (var index = 0; index < ScrollContent.Instance.leaderboardEntryViews.Count; index++)
            {
                var entryView = ScrollContent.Instance.leaderboardEntryViews[index];
                HandleInitializeScrollItem(TopIndex + index,entryView);
            }
        }

        private void HandleScrollItemChange(LeaderboardEntryView entryView, bool positiveDrag)
        {
            
            var dataIndex = positiveDrag ? BottomIndex - 1 : TopIndex - 2;
            
            if (positiveDrag)
                TopIndex++;
            else
                TopIndex--;
            
            PopulateLeaderboardEntry(entryView,LeaderboardAPI.leaderboardData.data[dataIndex]);
        }

        void HandleInitializeScrollItem(int index, LeaderboardEntryView entryView)
        {
            var entryData = LeaderboardAPI.leaderboardData.data[index-1];
            PopulateLeaderboardEntry(entryView,entryData);
            
        }
        
        private void PopulateLeaderboardEntry(LeaderboardEntryView entryView,LeaderboardEntryData entryData)
        {
            var model = new LeaderboardEntryModel()
            {
                rank = entryData.rank,
                nickname = entryData.nickname,
                score = entryData.score
            };
            entryView.Render(model);

        }
        
    }
}