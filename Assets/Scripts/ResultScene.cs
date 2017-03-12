using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{

    public Text scoreText;
    public Text comboText;

	void Start ()
    {
        var data = SystemManager.Instance.ScoreData;

        scoreText.text = data.score.ToString();
        comboText.text = data.maxCombo.ToString();

        SystemManager.Instance.ClearScore();
	}

    /// <summary>
    /// 曲選択画面に移動
    /// </summary>
    public void ReturnSelectMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MusicSelect");
    }
}
