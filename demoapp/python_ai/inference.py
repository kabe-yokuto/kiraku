import os
os.environ['TF_CPP_MIN_LOG_LEVEL']='2'
import numpy as np
import tensorflow as tf
tf.get_logger().setLevel("ERROR")

data_path = "C:/Users/kanet/音源入れ場/実地試験12-26/231221_120637_Soundland_HLSM_Guttural.wav"
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
model = tf.keras.models.load_model('./to_happyoukai/saved_model/my_model')

res = model.predict(data_list)
print(np.count_nonzero(res > 0.5))
print(np.count_nonzero(res <= 0.5))