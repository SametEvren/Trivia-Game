using System;
using System.Collections.Generic;
using Utility;

namespace Game.Skills
{
    public class SkillManager : Instancable<SkillManager>
    {
        #region Public Properties
        public static event Action<SkillType> OnSkillUsed;
        public List<SkillView> skillViews;
        #endregion
        

        private void Start()
        {
            for (int i = 0; i < skillViews.Count; i++)
            {
                skillViews[i].RenderText(SkillInventory.SkillAmount((SkillType)i));
            }
        }

        public void HandlePowerUpButtonPressed(int skillIndex)
        {
            var skill = (SkillType)skillIndex;
            if (SkillInventory.CanUse(skill)) UseSkill(skill);
        }
        
        private void UseSkill(SkillType skill)
        {
            SkillInventory.DecreaseSkill(skill);
            skillViews[(int)skill].RenderText(SkillInventory.SkillAmount(skill));
            OnSkillUsed?.Invoke(skill);
        }
    }
}
