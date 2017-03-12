using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.UI;

public class GameScene : MonoBehaviour {

    public static GameScene Instance { get; private set; }

    public string m_criCueName = "RoadToHeaven";
    public CriAtomSource m_gameMusicSource;

    public List<GameObject> m_notesPrefabs = new List<GameObject>();
    private List<float> m_timingList;
    private List<int> m_lineNumList;

    public string filePass;

    private float m_startTime = 0;

    private bool m_isPlaying = false;
    public Text m_musicTitle;
    public Image m_musicTitleBG;

    public Text scoreText;
    public Text comboText;
    private int _score = 0;
    private int _combo = 0;
    private int _maxCombo = 0;

    List<NotesScript> notesList = new List<NotesScript>();

    public void AddNote(NotesScript note)
    {
        notesList.Add(note);
    }

    void Awake()
    {
        if(Instance != null)
        {
            enabled = false;
            DestroyImmediate(this);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        m_timingList = new List<float>();
        m_lineNumList = new List<int>();
        m_criCueName = MusicData.musicCueList[(int)SystemManager.musicName];
        m_musicTitle.text = MusicData.musicTitles[m_criCueName];
        LoadCSV();
        CreateNotes();
        StartCoroutine(StartEvent());
    }

    IEnumerator StartEvent()
    {
        yield return new WaitForSeconds(2.0f);
        float elapsedTime = 0f;
        float endTime = 1f;
        float alpha = 1;
        Color mt = m_musicTitle.color;
        Color bg = m_musicTitleBG.color;
        while (elapsedTime < endTime)
        {
            elapsedTime += Time.deltaTime;
            alpha = 1 - Mathf.Clamp01(elapsedTime / endTime);
            mt.a = alpha;
            m_musicTitle.color = mt;
            bg.a = alpha;
            m_musicTitleBG.color = bg;
            yield return 0;
        }

        StartGame();
    }

    public void StartGame()
    {
        m_startTime = Time.time;
        SoundManager.PlayGameMusic(m_criCueName);
        m_isPlaying = true;
    }

    /// <summary>
    /// 終了イベント。スコアの保存とリザルト画面に遷移
    /// </summary>
    void FinishEvent()
    {
        var scoreData = new ScoreData(_score, _maxCombo);
        SystemManager.Instance.ScoreData = scoreData;
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }

    void Update()
    {
        if (m_isPlaying)
        {
            // 登録されているノーツを更新
            for(int i = 0, max = notesList.Count; i < max; ++i)
            {
                if (!notesList[i].enabled) { continue; }
                notesList[i].NoteUpdate();
            }
            scoreText.text = _score.ToString();
            comboText.text = _combo.ToString();
            // 最大コンボの更新
            _maxCombo = _maxCombo < _combo ? _combo : _maxCombo;

            // 曲が終了したら終了処理へ
            if (m_gameMusicSource.status == CriAtomSource.Status.PlayEnd)
            {
                m_isPlaying = false;
                // 終了処理
                FinishEvent();
            }
        }
    }

    void CreateNotes()
    {
        for(int i = 0, max = m_lineNumList.Count; i < max; ++i)
        {
            SpawnNotes(m_lineNumList[i], m_timingList[i]);
        }
    }

    void SpawnNotes(int num, float timing)
    {
        Instantiate(m_notesPrefabs[num],
            new Vector3(-4.5f + (3.0f * num), 10.0f * timing, 0),
            Quaternion.identity);
    }

    void LoadCSV()
    {
        TextAsset csv = Resources.Load(filePass + m_criCueName) as TextAsset;
        StringReader reader = new StringReader(csv.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');

            m_timingList.Add(float.Parse(values[0]));
            m_lineNumList.Add(int.Parse(values[1]));
        }
    }

    float GetMusicTime()
    {
        return Time.time - m_startTime;
    }

    public void PerfectTimingFunc(int num)
    {
        _score += 5;
    }

    public void GoodTimingFunc(int num)
    {
        _score += 2;
    }

    public void AddCombo()
    {
        _combo++;
    }

    public void ResetCombo()
    {
        _combo = 0;
    }
}
