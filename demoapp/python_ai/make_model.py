﻿import os
os.environ['TF_CPP_MIN_LOG_LEVEL']='2'
import numpy as np
import pathlib
import tensorflow as tf
from tensorflow.keras.models import Sequential
from tensorflow.keras import layers
import time
tf.get_logger().setLevel("ERROR")

data_path = "" #音源が入ったフォルダのPATH
SampRate = 16000 #SamplingRate
start_time = time.time()

dataset_dir = pathlib.Path(data_path)
dataset_subdir = [i for i in dataset_dir.iterdir() if i.is_dir()]
data_set = []
label_list = []

print('Loading input data....')
for i in dataset_subdir:
    dataset_wav = list(i.glob('**/*.wav'))
    for wav_path in dataset_wav:
        wav_path = str(wav_path)
        train_label = tf.strings.split(input = wav_path, sep = os.path.sep)
        train_label = train_label[-2]
        label_list.append(train_label)
        data_set.append((wav_path, train_label))

print('Loading is finished.')
print('uptime:',time.time() - start_time)
print('Preprocessing data...')

data_set = tf.random.shuffle(data_set)

data_list = np.array([])
label_list = np.array([])

def data_trans(data_set):
    global data_list
    global label_list
    wav_data, label = data_set
    wav_data = tf.io.read_file(wav_data)
    wav_data, _ = tf.audio.decode_wav(wav_data)
    wav_data = tf.squeeze(wav_data, axis = -1)
    num = 0
    while num +2 *SampRate < len(wav_data):
        data_list = np.append(data_list ,wav_data[num:num +2 *SampRate])
        label_list = np.append(label_list ,label)
        num += int(SampRate /2)
        
    if num +SampRate <len(wav_data):
        pad_num = SampRate *2 - len(wav_data[num:])
        app_data = np.append(wav_data[num:], np.zeros(pad_num))
        data_list = np.append(data_list, app_data)
        label_list = np.append(label_list ,label)
        
for i in data_set:
    data_trans(i)
    
data_list = data_list.reshape(-1, 2 *SampRate)

def func(lab):
    if lab == 'seijou_enge'.encode():
        return 1
    else:
        return 0
    
label_list = np.array(list(map(lambda x: func(x), label_list)))

def app_stft(data):
    data = tf.signal.stft(data, frame_length = 255, frame_step = 128)
    data = tf.abs(data)
    data = data[..., tf.newaxis]
    return data

data_list = np.array(list(map(lambda x: app_stft(x), data_list)))

print('Preprocessing completed.')
print('uptime:',time.time() - start_time)


model = Sequential()
model.add(layers.Input(shape=(249,129,1)))
model.add(layers.Resizing(128,64))
model.add(layers.Conv2D(32, kernel_size=(3,3), activation='relu', input_shape=(128,64,1)))
model.add(layers.Conv2D(64, kernel_size=(3,3), activation='relu'))
model.add(layers.MaxPooling2D(pool_size = (2,2)))
model.add(layers.Dropout(0.25))
model.add(layers.Flatten())
model.add(layers.Dense(64, activation = 'relu'))
model.add(layers.Dropout(0.25))
model.add(layers.Dense(32, activation = 'relu'))
model.add(layers.Dropout(0.5))
model.add(layers.Dense(1, activation = 'sigmoid'))

model.summary()

model.compile(loss = 'binary_crossentropy',
              optimizer = 'adam',
              metrics = ['accuracy'])

callbacks = [tf.keras.callbacks.EarlyStopping(patience = 5, restore_best_weights = True)]

model.fit(data_list,
          label_list,
          batch_size = 64,
          epochs = 20,
          validation_split = 0.2,
          callbacks = callbacks)
print('uptime:',time.time() - start_time)

model.save('saved_model/my_model')