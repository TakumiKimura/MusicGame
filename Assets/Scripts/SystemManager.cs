using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour {

    public static SystemManager _instance;
    public static SystemManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<SystemManager>();
                if(_instance == null)
                {
                    GameObject obj = new GameObject(typeof(SystemManager).Name);
                    _instance = obj.AddComponent<SystemManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }
    
    void Awake()
    {
        if(!CheckInstance())
        {
            enabled = false;
            DestroyImmediate(this);
        }
    }

    bool CheckInstance()
    {
        if(_instance == this)
        {
            return true;
        }
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return true;
        }
        return false;
    }

    ScoreData scoreData;

    public ScoreData ScoreData { get { return scoreData; } set { scoreData = value; } }
    
    public void ClearScore()
    {
        scoreData = new ScoreData();
    }

    public static MusicName musicName { get; private set; }

    public void LoadGame(MusicName _musicName)
    {
        musicName = _musicName;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}

public struct ScoreData
{
    public ScoreData(int _score, int _maxCombo)
    {
        score = _score;
        maxCombo = _maxCombo;
    }
    
    public int score;
    public int maxCombo;
}