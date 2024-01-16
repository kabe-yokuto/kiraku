import os
os.environ['TF_CPP_MIN_LOG_LEVEL']='2'
import sys
import numpy as np
import tensorflow as tf
#import cv2
#import time

tf.get_logger().setLevel("ERROR")

args = sys.argv

data_path = args[1]

categorize_model_path = 'saved_model/categorize_model' #分類モデルのPATH
determine_model_path = 'saved_model/determine_model' #異常判別モデルのPATH
SampRate = 16000

data_list = np.array([])

def data_trans(wav_data):
    global data_list
    wav_data = tf.io.read_file(wav_data)
    wav_data, _ = tf.audio.decode_wav(wav_data)
    wav_data = tf.squeeze(wav_data, axis = -1)
    num = -SampRate
    while num +SampRate < len(wav_data):
        to_app_wav = wav_data[max(0, num) :min(num +SampRate *2, len(wav_data)-1)]
        if num < 0:
            to_app_wav = np.append(np.zeros(-num), to_app_wav)
        if len(to_app_wav) < SampRate *2:
            to_app_wav = np.append(to_app_wav, np.zeros(SampRate *2 -len(to_app_wav)))
        
        data_list = np.append(data_list ,to_app_wav)
        num += int(SampRate /2)
        
data_trans(data_path)
if len(data_list) == 0:
    print('result:Not enough data')
    exit()
    
data_list = data_list.reshape(-1, SampRate *2)

def app_stft(data):
    data = tf.signal.stft(data, frame_length = 255, frame_step = 128)
    data = tf.abs(data)
    data = data[..., tf.newaxis]
    return data

data_list = np.array(list(map(lambda x: app_stft(x), data_list)))

categorize_model = tf.keras.models.load_model(categorize_model_path)
categorized_data = categorize_model.predict(data_list) #categorized_dataは、区間毎に5種の音声のそれぞれの確率(0<x<1)のnp.arrayの配列
labeled_list = np.argmax(categorized_data, axis = 1)

def make_log(log_data, last_num, index_ary): #ログ出力関数
    if last_num == 1:
        class_list = np.array(['swallow_sound','word','breath','no_sound'])
    elif last_num == 2:
        class_list = np.array(['normal','abnormal'])
    log_data = class_list[log_data]
    log_data = log_data.reshape([-1,1])
    
    def make_index(id_ary):
        id_ary = id_ary /2
        id_ary2 = id_ary +1
        id_ary = id_ary -1
        id_ary = np.char.mod('%.{}f'.format(1), id_ary)
        id_ary2 = np.char.mod('%.{}f'.format(1), id_ary2)
        add_string = np.full(len(id_ary), '～')
        result_ary = np.core.defchararray.add(id_ary, add_string)
        result_ary = np.core.defchararray.add(result_ary, id_ary2)
        return result_ary

    index_ary = make_index(index_ary)
    index_ary = index_ary.reshape([-1,1])
    
    log_data = np.concatenate([index_ary, log_data], 1)
    np.savetxt(f'test_log{last_num}.txt', log_data, fmt = '%s')
make_log(labeled_list, 1, np.arange(len(labeled_list))) #ログ出力用(1)。ここをコメントアウトで出力無しに

to_determine_list = []
id_list = np.array([]) #ログ出力用の音声データindex

for i in range(len(labeled_list)):
    if labeled_list[i] == 2:
        to_determine_list.append(data_list[i])
        id_list = np.append(id_list, i) #ログ出力用

if len(to_determine_list) == 0:
    print('result:No breathing sounds detected')
    exit()

to_determine_list = np.array(to_determine_list)
determine_model = tf.keras.models.load_model(determine_model_path)
determined_data = determine_model.predict(to_determine_list) #determined_dataは、区間毎に誤嚥であるfroat型の確率(0<x<1)のnp.arrayの配列
determined_data = np.round(determined_data).astype(int)

make_log(determined_data, 2, id_list) #ログ出力用(2)。ここをコメントアウトで出力無しに

goen_count = np.count_nonzero(determined_data > 0.5)
seijou_count = np.count_nonzero(determined_data <= 0.5)

if goen_count *2 > seijou_count:
    print('result:Abnormal')
else:
    print('result:Normal')
