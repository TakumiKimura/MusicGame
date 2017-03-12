using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesScript : MonoBehaviour
{
    static List<KeyCode> inputs = new List<KeyCode>()
    {
        KeyCode.D, //0
        KeyCode.F, //1
        KeyCode.J, //2
        KeyCode.K  //3
    };

    public int m_lineNum;
    Transform m_transform;

    void OnEnable()
    {
        GameScene.Instance.AddNote(this);
        m_transform = transform;
    }

    public void NoteUpdate()
    {
        CheckInput(inputs[m_lineNum]);

        m_transform.position += Vector3.down * 10.0f * Time.deltaTime;
        if (m_transform.position.y < -5.0f)
        {
            GameScene.Instance.ResetCombo();
            enabled = false;
            gameObject.SetActive(false);
        }
    }
    
    void CheckInput(KeyCode key)
    {
        float pos_y = m_transform.position.y;

        if(pos_y > 1.5f) { return; }
        if(pos_y < -1.5f) { return; }

        if (Input.GetKeyDown(key))
        {
            SoundManager.PlaySE("Tap");

            if (pos_y < 0.25f && pos_y > -0.25f)
            {
                GameScene.Instance.PerfectTimingFunc(m_lineNum);
            }
            else
            {
                GameScene.Instance.GoodTimingFunc(m_lineNum);
            }
            GameScene.Instance.AddCombo();
            enabled = false;
            gameObject.SetActive(false);
        }
    }
}