using UnityEngine;
using System.Collections;

/// <summary>
/// 譜面生成用クラス
/// </summary>
public class NotesTimingMaker : MonoBehaviour
{
    public string m_criCueName = "RoadToHeaven";
    public CSVWriter m_CSVWriter;

    private float _startTime = 0;
    
    private bool _isPlaying = false;
    public GameObject startButton;

    void Update()
    {
        if (_isPlaying)
        {
            DetectKeys();
        }
    }

    public void StartMusic()
    {
        startButton.SetActive(false);
        SoundManager.PlayGameMusic(m_criCueName);
        _startTime = Time.time;
        _isPlaying = true;
    }

    void DetectKeys()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            WriteNotesTiming(0);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            WriteNotesTiming(1);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            WriteNotesTiming(2);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            WriteNotesTiming(3);
        }
    }

    void WriteNotesTiming(int num)
    {
        Debug.Log(GetTiming());
        SoundManager.PlaySE("Tap");
        m_CSVWriter.WriteCSV(GetTiming().ToString() + "," + num.ToString());
    }

    float GetTiming()
    {
        return Time.time - _startTime;
    }
}