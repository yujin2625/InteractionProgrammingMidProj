using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using TMPro;
using UnityEditor.Scripting.Python;
using Python.Runtime;
using System.IO;

public class PythonInUnity : MonoBehaviour
{
    public TMP_Text result;
    ReadResult readResult;
    void Start()
    {
        readResult = gameObject.GetComponent<ReadResult>();
    }
    public void PythonExecuteTry()
    {
        PythonEngine.Initialize();
        using (Py.GIL())
        {
            PythonEngine.RunSimpleString(@"
import cv2;
import matplotlib.pyplot as plt;
import numpy as np;
from PIL import Image;
import sys;
import dlib;
import os;

testImage = Image.open(os.getcwd()+'/sample/sample.png');
testImage = np.array(testImage);

plt.imshow(testImage);

face_detector = dlib.get_frontal_face_detector();

face_detection = face_detector(testImage, 1);

for face_detection in face_detection:
    left, top, right, bottom = face_detection.left(), face_detection.top(), face_detection.right(), face_detection.bottom();
    cv2.rectangle(testImage,(left,top),(right,bottom),(0,255,0),2);
    
plt.imshow(testImage);

left, top, right, bottom = face_detection.left(), face_detection.top(), face_detection.right(), face_detection.bottom();

testImage = testImage[top:bottom, left:right];

plt.imshow(testImage);

testImage = cv2.resize(testImage,(48,48));

testImage.shape;

plt.imshow(testImage);

testImage = testImage/255;

testImage = np.expand_dims(testImage,axis=0);
testImage.shape;

from keras.utils import np_utils;
from keras.datasets import mnist;
from keras.models import Sequential;
from keras.layers import Dense, Activation;
import numpy as np;
from numpy import argmax;

from keras.models import load_model;
model = load_model('emotion_classification_model_higher_epochs.h5');



pred_probability = model.predict(testImage);
pred_probability;

pred = np.argmax(pred_probability);
pred;

def pred_emotion(pred):
    match pred:
        case 0:
            print('Angry');
            return 'Angry';
        case 1:
            print('Disgust');
            return 'Disgust';
        case 2:
            print('Fear');
            return 'Fear';
        case 3:
            print('Happy');
            return 'Happy';
        case 4:
            print('Neutral');
            return 'Neutral';
        case 5:
            print('Sad');
            return 'Sad';
        case 6:
            print('Surprise');
            return 'Surprise';

pred_emotion(pred);

file = open(os.getcwd()+'/predicted/PredictedEmotion.txt','w');
file.write(pred_emotion(pred));
file.close();




");
        }
    }

    public void PythonExecute()
    {
        try
        {
            Process process = new();
            process.StartInfo.FileName = @"C:\Users\yujin\AppData\Local\Programs\Python\Python310\python.exe";
            process.StartInfo.Arguments = Application.streamingAssetsPath + "/감정인식/EmotionClassificationModel.py";

            //process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            process.WaitForExit();
            readResult.DisplayResult();
        }
        catch(Exception e)
        {
            UnityEngine.Debug.LogError("Unable to launch app: " + e.Message);
            readResult.DisplayFail(e.Message);
        }
        //Process.Start(Application.streamingAssetsPath + "/감정인식/dist/EmotionClassificationModel.exe");
        //try
        //{
        //    readResult.DisplayResult();
        //}
        //catch (Exception e)
        //{
        //    UnityEngine.Debug.Log(e.Message);
        //}
        //    //string Result = process.StandardOutput.ReadToEnd();
        //foreach(Process process in Process.GetProcesses())
        //{
        //    if (process.ProcessName.StartsWith("EmotionClassificationModel"))
        //    {
        //        process.Kill();
        //    }
        //}

    }
}
