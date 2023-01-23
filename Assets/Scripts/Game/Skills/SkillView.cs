using TMPro;
using UnityEngine;

public class SkillView : MonoBehaviour
{
    #region Private Properties
    [SerializeField]private TextMeshProUGUI skillAmount;
    #endregion

    public void RenderText(int amount)
    {
        skillAmount.text = "X" + amount;
    }
}
