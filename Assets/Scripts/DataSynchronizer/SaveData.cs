using System;
using System.Collections.Generic;

namespace HotDogBush.Infrastructure.Service
{
    [Serializable]
    public class SaveData
    {
        public int maxOpenStage = 0;
        public int[] levelScore = new int[0];
        public int[] stageStars = new int[0];
        public Dictionary<string, bool> disabledHints = new Dictionary<string, bool>();
        public Dictionary<string, bool> unlockedAchievements = new Dictionary<string, bool>();
        public Dictionary<string, bool> rateShownForLevel = new Dictionary<string, bool>();
        public int numHappyCustomers = 0;
        public int totalHotDogsSold = 0;
        public int speedModeBestScore = 0;
        public int currentStage = -1;
        public bool playedAlready = false;
        public bool gamingServiceEnabled = true;
        public bool tapToCookPromoWatched = false;
        public Dictionary<string, int> stageToDay = new Dictionary<string, int>();
        public Dictionary<string, bool> booleans = new Dictionary<string, bool>();
        public Dictionary<string, int> ints = new Dictionary<string, int>();
        public Dictionary<string, double> numbers = new Dictionary<string, double>();
        public Dictionary<string, string> strings = new Dictionary<string, string>();
    }
}