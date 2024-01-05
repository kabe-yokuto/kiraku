import os
os.environ['TF_CPP_MIN_LOG_LEVEL']='2'
import numpy as np
import tensorflow as tf
tf.get_logger().setLevel("ERROR")

data_path = "" #判定したい音源のPATH
model_path = './my_model' #モデルのPATH
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
model = tf.keras.models.load_model(model_path)

res = model.predict(data_list) #resは、区間毎に誤嚥であるfroat型の確率(0<x<1)で判定された結果が格納されたnp.arrayの配列
#print(f'誤嚥の疑いあり:{np.count_nonzero(res > 0.5)}')
#print(f'誤嚥の疑いなし:{np.count_nonzero(res <= 0.5)}')