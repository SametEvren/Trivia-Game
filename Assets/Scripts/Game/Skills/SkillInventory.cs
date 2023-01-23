using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Skills
{
    public class SkillInventory : MonoBehaviour
    {
        #region Private Properties
        private static Dictionary<SkillType, int> _skillInventory = new();
        private const int ItemStartAmount = 2;
        #endregion
        
        private void Awake()
        {
            GetInventoryValues();
        }
        
        public static bool CanUse(SkillType skillType)
        {
            return SkillAmount(skillType) > 0;
        }

        public static int SkillAmount(SkillType skillType)
        {
            if (!SkillExistInInventory(skillType))
                return 0;
            return _skillInventory[skillType];
        }
        
        public static void DecreaseSkill(SkillType skillType)
        {
            if (!SkillExistInInventory(skillType)) 
                return;
            
            _skillInventory[skillType]--;
            PlayerPrefs.SetInt(skillType.ToString(), _skillInventory[skillType]);
        }

        private static void AddSkill(SkillType skillType, int amount)
        {
            if (!SkillExistInInventory(skillType))
                _skillInventory.Add(skillType, amount);
            else
                _skillInventory[skillType] += amount;
            
            PlayerPrefs.SetInt(skillType.ToString(), _skillInventory[skillType]);
        }
        
        private static bool SkillExistInInventory(SkillType skillType)
        {
            return _skillInventory.ContainsKey(skillType);
        }
        
        private void GetInventoryValues()
        {
            foreach (SkillType skillType in (SkillType[]) Enum.GetValues(typeof(SkillType)))
            {
                var typeString = skillType.ToString();
                var amount = PlayerPrefs.GetInt(typeString, ItemStartAmount);
                AddSkill(skillType,amount);
            }
        }
    }
    
    public enum SkillType
    {
        DoubleChance,
        Bomb,
        Skip,
        MagicAnswer
    }
}