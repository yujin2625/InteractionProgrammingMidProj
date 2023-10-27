
# 1. 사진 읽어오기
import cv2
import matplotlib.pyplot as plt
import numpy as np
from PIL import Image
import sys
import dlib
import os

testImage = Image.open(os.getcwd()+"/sample/sample.png")
testImage = np.array(testImage)

plt.imshow(testImage)

#testImage = cv2.resize(testImage,(500,700))

# 2. 얼굴 인식
face_detector = dlib.get_frontal_face_detector()

face_detection = face_detector(testImage, 1)

for face_detection in face_detection:
    left, top, right, bottom = face_detection.left(), face_detection.top(), face_detection.right(), face_detection.bottom()
    cv2.rectangle(testImage,(left,top),(right,bottom),(0,255,0),2)
    
plt.imshow(testImage)

# 3. 얼굴 부분 이미지 가져오기
left, top, right, bottom = face_detection.left(), face_detection.top(), face_detection.right(), face_detection.bottom()

testImage = testImage[top:bottom, left:right]

plt.imshow(testImage)

# 4. 감정인식 가능한 크기 및 형태로 resize
testImage = cv2.resize(testImage,(48,48))

testImage.shape

plt.imshow(testImage)

testImage = testImage/255

testImage = np.expand_dims(testImage,axis=0)
testImage.shape

# 5. 감정인식 모델 가져오기
from keras.utils import np_utils
from keras.datasets import mnist
from keras.models import Sequential
from keras.layers import Dense, Activation
import numpy as np
from numpy import argmax

from keras.models import load_model
model = load_model('emotion_classification_model_higher_epochs.h5')



# 6. 감정 분류
pred_probability = model.predict(testImage)
pred_probability

# 7. 분류된 감정 중 가장 높은값 찾기
pred = np.argmax(pred_probability)
pred

def pred_emotion(pred):
    match pred:
        case 0:
            print("Angry")
            return "Angry"
        case 1:
            print("Disgust")
            return "Disgust"
        case 2:
            print("Fear")
            return "Fear"
        case 3:
            print("Happy")
            return "Happy"
        case 4:
            print("Neutral")
            return "Neutral"
        case 5:
            print("Sad")
            return("Sad")
        case 6:
            print("Surprise")
            return "Surprise"

pred_emotion(pred)

file = open(os.getcwd()+"/predicted/PredictedEmotion.txt","w")
file.write(pred_emotion(pred))
file.close()


