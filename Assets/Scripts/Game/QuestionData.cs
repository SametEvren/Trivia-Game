using System.Collections.Generic;

namespace Game
{ 
    public class Question
    {
        public string category { get; set; }
        public string question { get; set; }
        public List<string> choices { get; set; }
        public string answer { get; set; }
    }

    public class QuestionData
    {
        public List<Question> questions { get; set; }
    }

}