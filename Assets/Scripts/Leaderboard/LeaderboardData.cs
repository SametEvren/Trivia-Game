using System.Collections.Generic;

[System.Serializable]
public class LeaderboardData
{
    public int page { get; set; }
    public bool is_last { get; set; }
    public List<LeaderboardEntryData> data { get; set; }
}
[System.Serializable]
public class LeaderboardEntryData
{
    public int rank { get; set; }
    public string nickname { get; set; }
    public int score { get; set; }
}