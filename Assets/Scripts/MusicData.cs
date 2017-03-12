using System.Collections.Generic;

public enum MusicName
{
    RoadToHeaven,
    AFickleBufferflyInTheHangingGarden
};

public struct MusicData {

    public readonly static List<string> musicCueList = new List<string>()
    {
        "RoadToHeaven",
        "AFickleBufferflyInTheHangingGarden"
    };

    public readonly static Dictionary<string, string> musicTitles = new Dictionary<string, string>()
    {
        {musicCueList[0], "Road to heaven"},
        {musicCueList[1], "空中庭園と気まぐれな蝶"}
    };
}
