import cv2
import matplotlib.pyplot as plt
import numpy as np
from PIL import Image
import sys
import dlib
import os

testImage = Image.open(os.getcwd()+"\\Assets\\StreamingAssets\\감정인식\\sample\\sample.png")
testImage = np.array(testImage)

plt.imshow(testImage)

face_detector = dlib.get_frontal_face_detector()

face_detection = face_detector(testImage, 1)

for face_detection in face_detection:
    left, top, right, bottom = face_detection.left(), face_detection.top(), face_detection.right(), face_detection.bottom()
    cv2.rectangle(testImage,(left,top),(right,bottom),(0,255,0),2)
    
plt.imshow(testImage)

left, top, right, bottom = face_detection.left(), face_detection.top(), face_detection.right(), face_detection.bottom()

testImage = testImage[top:bottom, left:right]

plt.imshow(testImage)

testImage = cv2.resize(testImage,(48,48))

testImage.shape

plt.imshow(testImage)

testImage = testImage/255

testImage = np.expand_dims(testImage,axis=0)
testImage.shape

from keras.utils import np_utils
from keras.datasets import mnist
from keras.models import Sequential
from keras.layers import Dense, Activation
import numpy as np
from numpy import argmax

from keras.models import load_model
model = load_model('emotion_classification_model_higher_epochs.h5')



pred_probability = model.predict(testImage)
pred_probability

pred = np.argmax(pred_probability)
pred

file = open(os.getcwd()+"\\Assets\\StreamingAssets\\감정인식\\predicted\\PredictedEmotion.txt","w",encoding='UTF8')
file.write(pred)
file.close()


