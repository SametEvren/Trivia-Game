using System.Collections.Generic;

namespace Game
{ 
    [System.Serializable]
    public class Question
    {
        public string category { get; set; }
        public string question { get; set; }
        public List<string> choices { get; set; }
        public string answer { get; set; }
    }

    [System.Serializable]
    public class QuestionData
    {
        public List<Question> questions { get; set; }
    }

}