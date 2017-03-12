using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LoadGame : MonoBehaviour {

    public List<Sprite> spriteList = new List<Sprite>();
    public List<Image> imageList = new List<Image>();
    public List<Text> titleList = new List<Text>();

    void Start()
    {
        for (int i = 0; i < titleList.Count; ++i)
        {
            if (titleList.Count == MusicData.musicCueList.Count)
            {
                titleList[i].text = MusicData.musicTitles[MusicData.musicCueList[i]];
            }
        }

        for(int i = 0; i < imageList.Count; ++i)
        {
            if (spriteList.Count == imageList.Count)
            {
                imageList[i].sprite = spriteList[i];
            }
        }
    }

#if UNITY_EDITOR
    void Update()
    {
        Start();
    }
#endif

    public void LoadScene(int musicName)
    {
        musicName = musicName % ((int)MusicName.AFickleBufferflyInTheHangingGarden + 1);
        SystemManager.Instance.LoadGame((MusicName)musicName);
    }
}
