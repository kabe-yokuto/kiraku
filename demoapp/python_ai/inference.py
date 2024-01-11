import os
os.environ['TF_CPP_MIN_LOG_LEVEL']='2'
import sys
import numpy as np
import tensorflow as tf
#import cv2
#import time

tf.get_logger().setLevel("ERROR")

args = sys.argv

data_path = 'D:/kiraku/demoapp/python_ai/SoundSource/test-12-26/231221_120637_Soundland_HLSM_Guttural.wav' #判定したい音源のPATH

categorize_model_path = 'saved_model/categorize_model' #分類モデルのPATH
determine_model_path = 'saved_model/determine_model' #異常判別モデルのPATH
SampRate = 16000

data_list = np.array([])

def data_trans(data):
    global data_list
    data = tf.io.read_file(data)
    data, _ = tf.audio.decode_wav(data)
    data = tf.squeeze(data, axis = -1)
    num = 0
    while num +2 *SampRate < len(data):
        data_list = np.append(data_list ,data[num:num +2 *SampRate])
        num += int(SampRate /2)
        
    if num +SampRate <len(data):
        pad_num = SampRate *2 - len(data[num:])
        app_data = np.append(data[num:], np.zeros(pad_num))
        data_list = np.append(data_list, app_data)
        
data_trans(data_path)
data_list = data_list.reshape(-1, 2 *SampRate)

def app_stft(data):
    data = tf.signal.stft(data, frame_length = 255, frame_step = 128)
    data = tf.abs(data)
    data = data[..., tf.newaxis]
    return data

data_list = np.array(list(map(lambda x: app_stft(x), data_list)))

categorize_model = tf.keras.models.load_model(categorize_model_path)
categorized_data = categorize_model.predict(data_list) #categorized_dataは、区間毎に5種の音声のそれぞれの確率(0<x<1)のnp.arrayの配列
labeled_list = np.argmax(categorized_data, axis = 1)

to_determine_list = []

for i in range(len(labeled_list)):
    if labeled_list[i] == 3:
        to_determine_list.append(data_list[i])

to_determine_list = np.array(to_determine_list)
determine_model = tf.keras.models.load_model(determine_model_path)
determined_data = determine_model.predict(to_determine_list) #determined_dataは、区間毎に誤嚥であるfroat型の確率(0<x<1)のnp.arrayの配列

goen_count = np.count_nonzero(determined_data > 0.5)
seijou_count = np.count_nonzero(determined_data <= 0.5)

if goen_count *2 > seijou_count:
    print('result:Abnormal')
else:
    print('result:Normal')
