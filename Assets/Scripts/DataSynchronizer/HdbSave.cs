using System;
using System.Collections.Generic;

[Serializable]
public class HdbSave
{
    public bool fullVersion;
    public Dictionary<string, int> numbers;
    public bool musicEnabled;
    public Dictionary<string, int> stageToDay;
    public int[] stageStars;
    public string createdInVersion;
    public Dictionary<string, int> ints;
    public Dictionary<string, int> rateShownForLevel;
    public int numHappyCustomers;
    public string savedInVersion;
    public Dictionary<string, string> strings;
    public int currentStage;
    public int totalHotDogsSold;
    public bool gamingServiceEnabled;
    public int speedModeBestScore;
    public bool playedAlready;
    public Dictionary<string, bool> booleans;
    public Dictionary<string, bool> disabledHints;
    public int maxOpenStage;
    public int[] levelScore;
    public Dictionary<string, bool> unlockedAchievements;
    public bool sfxEnabled;
    public bool tapToCookPromoWatched;
    public string guid;
}