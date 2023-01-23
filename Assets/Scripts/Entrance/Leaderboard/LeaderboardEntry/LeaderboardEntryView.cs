using TMPro;
using UnityEngine;

namespace Entrance.Leaderboard.LeaderboardEntry
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        #region Public Properties
        public TextMeshProUGUI rankText, nicknameText, scoreText;
        #endregion

        public void Render(LeaderboardEntryModel model)
        {
            rankText.text = model.rank.ToString();
            nicknameText.text = model.nickname;
            scoreText.text = model.score.ToString();
        }
    }
}