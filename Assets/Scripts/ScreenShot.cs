using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Android;


public class ScreenShot : MonoBehaviour
{
    float delayTime = 1f;
    string folderPath;
    PythonInUnity pythonInUnity;
    string directoryPath;
    string fileName = "captureSample";
    private void Start()
    {
        pythonInUnity = gameObject.GetComponent<PythonInUnity>();
        folderPath = Application.streamingAssetsPath;
        directoryPath = folderPath + "/감정인식/sample/";
        StartCoroutine(SetTimer());
    }
    IEnumerator SetTimer()
    {
        Debug.Log("time = " + Time.time);
        StartCoroutine(CaptureScreenForMobile());
        pythonInUnity.PythonExecute();

        yield return new WaitForSeconds(delayTime);
        StartCoroutine(SetTimer());
    }
    IEnumerator CaptureScreenForMobile()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        SaveTextureToPNGFile(texture, directoryPath, fileName);
        // cleanup
        Object.Destroy(texture);
    }

    
    public void SaveTextureToPNGFile(Texture2D texture,string directoryPath,string fileName)
    {
        if (string.IsNullOrEmpty(directoryPath))
        {
            return;
        }
        if (!Directory.Exists(directoryPath))
        {
            Debug.Log("디렉토리가 존재하지 않음 -> 생성");
            Directory.CreateDirectory(directoryPath);
        }

        byte[] texturePNGBytes = texture.EncodeToPNG();

        string filePath = directoryPath + fileName + ".png";
        File.WriteAllBytes(filePath, texturePNGBytes);
    }
}
