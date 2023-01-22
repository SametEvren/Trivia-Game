using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Leaderboard.LeaderboardEntry
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        public TextMeshProUGUI rankText, nicknameText, scoreText;
        public Image backgroundImage;

        public void Render(LeaderboardEntryModel model)
        {
            rankText.text = model.rank.ToString();
            nicknameText.text = model.nickname;
            scoreText.text = model.score.ToString();
        }
    }
}