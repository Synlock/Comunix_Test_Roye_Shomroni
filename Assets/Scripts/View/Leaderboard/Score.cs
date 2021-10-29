namespace ComunixTest.Model
{
    [System.Serializable]
    public class Score
    {
        public string name = "";
        public int score = 0;

        public Score(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

    }
}