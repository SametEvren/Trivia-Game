using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class HelperMethods
    {
        public static List<string> ReturnWrongAnswers(string correctAnswer)
        {
            List<string> choices = new() { "A", "B", "C", "D" };
            choices.Remove(correctAnswer);
            return choices;
        }

        public static List<string> SelectRandomWrongChoices(string correctAnswer)
        {
            var wrongAnswers = ReturnWrongAnswers(correctAnswer);
            wrongAnswers.RemoveAt(Random.Range(0, wrongAnswers.Count));
            return wrongAnswers;
        }
    }
}