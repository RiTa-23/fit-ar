using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI log_text;
    // Start is called before the first frame update
    void Start()
    {
        log_text.text += "EventManager���sOK\n";
    }
    bool b21 = false;
    bool a11 = false;
    bool a21 = false;

    // Update is called once per frame
    public void B21()
    {
        if(!b21)
        {
            log_text.text += "B21�}�[�J�[��F�����܂���\n";
            b21 = true;
        }
        
    }

    public void A11()
    {
        if (!a11)
        {
            log_text.text += "A11�}�[�J�[��F�����܂���\n";
            a11 = true;
        }
    }

    public void A23()
    {
        if(!a21)
        {
            log_text.text += "A23�}�[�J�[��F�����܂���\n";
            a21 = true;
        }
        
    }
}
