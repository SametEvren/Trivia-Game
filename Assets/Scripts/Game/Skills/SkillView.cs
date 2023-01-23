using TMPro;
using UnityEngine;

public class SkillView : MonoBehaviour
{
    public TextMeshProUGUI skillAmount;

    public void RenderText(int amount)
    {
        skillAmount.text = "X" + amount;
    }
}
