using System;
using UnityEngine;

namespace Leaderboard.LeaderboardEntry
{
    public class LeaderboardEntryController : MonoBehaviour
    {
        [SerializeField] private LeaderboardEntryView view;
        private LeaderboardEntryModel _model;

        public void SetModel(LeaderboardEntryModel newModel)
        {
            _model = newModel;
            view.Render(_model);
        }
    }
}