using UnityEditor;
using UnityEditor.Scripting.Python;

public class EmotionClassificationModel
{
   [MenuItem("Python Scripts/EmotionClassificationModel")]
   public static void EmotionClassificationModel()
   {
       PythonRunner.RunFile("Assets/StreamingAssets/감정인식/EmotionClassificationModel.py");
       }
};
