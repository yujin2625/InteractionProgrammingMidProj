using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ReadResult : MonoBehaviour
{
    public TMP_Text result;
 
    private void Start()
    {
        result = result.GetComponent<TMP_Text>();
    }
    public void DisplayResult()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "감정인식/predicted/PredictedEmotion.txt");
        result.text = ReadTxt(filePath);
    } 
    public void DisplayFail(string errorMessage)
    {
        result.text = "Failed Face Detection : "+errorMessage.ToString();
    }
    string ReadTxt(string filePath)
    {
        FileInfo fileInfo = new(filePath);
        string value = "";

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            value = reader.ReadToEnd();
            Debug.Log(value);
            if (value != "")
            {
                reader.Close();
            }
        }
        else
            value = "파일이 없습니다.";

        return value;
    }
}
