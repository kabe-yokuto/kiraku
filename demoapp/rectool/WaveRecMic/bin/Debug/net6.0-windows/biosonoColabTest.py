#-*- coding: utf-8 -*-




#　設定
import sys
import os
import pathlib
import math

#import matplotlib.pyplot as plt
import numpy as np
#import seaborn as sns
import tensorflow as tf

from tensorflow.keras import models
from tensorflow.keras import layers

#from IPython import display

#import abcdef

#from tkinter import filedialog


#　ここから
args = sys.argv
python_source = args[0];
input_filename = args[1];

flag = args[2];
training_flag = False
if flag=='-T':
	training_flag = True 

#COLAB上で実行するときはTrueにする
colab_flag = False


if colab_flag==True :
  from google.colab import drive
  drive.mount('/content/drive')

# Set the seed value for experiment reproducibility.
seed = 42
tf.random.set_seed(seed)
np.random.seed(seed)

# ミニ音声コマンドデータセットをインポートする

if colab_flag==True :
  DATASET_PATH = '/content/drive/MyDrive/testdata'
else:
  DATASET_PATH = 'testdata'


data_dir = pathlib.Path(DATASET_PATH)

AUTOTUNE = tf.data.AUTOTUNE




"""
if not data_dir.exists():
  tf.keras.utils.get_file(
      'mini_speech_commands.zip',
      origin="http://storage.googleapis.com/download.tensorflow.org/data/mini_speech_commands.zip",
      extract=True,
      cache_dir='.', cache_subdir='data')
"""

#ディレクトリ内のエントリを取得する
commands = np.array(tf.io.gfile.listdir(str(data_dir)))
#README.mdファイルを除外する（各音声ディレクトリ名リストが取得される）
commands = commands[commands != 'README.md']
print('Commands:', commands)

# ----------- 関数の定義 ----------

# データセットの生のWAVオーディオファイルをオーディオテンソルに前処理する関数を定義

def decode_audio(audio_binary):
  # Decode WAV-encoded audio files to `float32` tensors, normalized
  # to the [-1.0, 1.0] range. Return `float32` audio and a sample rate.
  audio, _ = tf.audio.decode_wav(contents=audio_binary)
  # Since all the data is single channel (mono), drop the `channels`
  # axis from the array.
  return tf.squeeze(audio, axis=-1)

# 各ファイルの親ディレクトリを使用してラベルを作成する関数を定義

def get_label(file_path):
  parts = tf.strings.split(
      input=file_path,
      sep=os.path.sep)
  # Note: You'll use indexing here instead of tuple unpacking to enable this
  # to work in a TensorFlow graph.
  label = '';
  if len(parts) >= 2 :
    label = parts[-2]
  return label

#  すべてをまとめる別のヘルパー関数get_waveform_and_labelを定義
def get_waveform_and_label(file_path):
  label = get_label(file_path)
  audio_binary = tf.io.read_file(file_path)
  waveform = decode_audio(audio_binary)
  return waveform, label

# 波形をスペクトログラムに変換

def get_spectrogram(waveform):
  # Zero-padding for an audio waveform with less than 16,000 samples.
  input_len = 16000
  waveform = waveform[:input_len]
  zero_padding = tf.zeros(
      [16000] - tf.shape(waveform),
      dtype=tf.float32)
  # Cast the waveform tensors' dtype to float32.
  waveform = tf.cast(waveform, dtype=tf.float32)
  # Concatenate the waveform with `zero_padding`, which ensures all audio
  # clips are of the same length.
  equal_length = tf.concat([waveform, zero_padding], 0)
  # Convert the waveform to a spectrogram via a STFT.
  spectrogram = tf.signal.stft(
      equal_length, frame_length=255, frame_step=128)
  # Obtain the magnitude of the STFT.
  spectrogram = tf.abs(spectrogram)
  # Add a `channels` dimension, so that the spectrogram can be used
  # as image-like input data with convolution layers (which expect
  # shape (`batch_size`, `height`, `width`, `channels`).
  spectrogram = spectrogram[..., tf.newaxis]
  return spectrogram

# モデルを構築してトレーニングする

def preprocess_dataset(files):
  files_ds = tf.data.Dataset.from_tensor_slices(files)
  output_ds = files_ds.map(
      map_func=get_waveform_and_label,
      num_parallel_calls=AUTOTUNE)
  output_ds = output_ds.map(
      map_func=get_spectrogram_and_label_id,
      num_parallel_calls=AUTOTUNE)
  return output_ds

# 波形データセットをスペクトログラムとそれに対応するラベルに整数IDとして変換する関数を定義

def get_spectrogram_and_label_id(audio, label):
  spectrogram = get_spectrogram(audio)
  label_id = tf.argmax(label == commands)
  return spectrogram, label_id



# --------- 処理開始 --------

model_path = 'model/modeldata'
model_dir = pathlib.Path(model_path)
if not model_dir.exists() or training_flag==True :
#-------- モデルがなければ生成する --------

    # オーディオクリップをfilenamesというリストに抽出し、シャッフルします

    filenames = tf.io.gfile.glob(str(data_dir) + '/*/*')
    filenames = tf.random.shuffle(filenames)
    num_samples = len(filenames)
    print('Number of total examples:', num_samples)
    print('Number of examples per label:',
          len(tf.io.gfile.listdir(str(data_dir/commands[0]))))
    print('Example file tensor:', filenames[0])

    # filenamesを、それぞれ80:10:10の比率を使用して、トレーニング、検証、およびテストセットに分割します。
    """
    train_files = filenames[:6400]
    val_files = filenames[6400: 6400 + 800]
    test_files = filenames[-800:]
    """
    count = len(filenames)
    print("file count = " +str(count))

    train_count = math.floor(count/10*8)
    #val_count = math.floor((count-train_count)/2)
    val_count = math.floor(count-train_count)
    print("file count = " +str(count) + " train_count="+str(train_count)+" val_count="+str(val_count))
    #print( "test_count="+str(-((train_count+val_count)-count)))

    train_files = filenames[:train_count]
    val_files = filenames[train_count: train_count + val_count]
    #test_files = filenames[((train_count+val_count)-count):]

    print('Training set size', len(train_files))
    print('Validation set size', len(val_files))
    #print('Test set size', len(test_files))

    # 音声とラベルのペアを抽出するためのトレーニングセットを作成



    files_ds = tf.data.Dataset.from_tensor_slices(train_files)

    waveform_ds = files_ds.map(
        map_func=get_waveform_and_label,
        num_parallel_calls=AUTOTUNE)

    # データの調査を開始します。 1つの例のテンソル化された波形と対応するスペクトログラムの形状を印刷し、元のオーディオを再生

    for waveform, label in waveform_ds.take(1):
      label = label.numpy().decode('utf-8')
      spectrogram = get_spectrogram(waveform)

    print('Label:', label)
    print('Waveform shape:', waveform.shape)
    print('Spectrogram shape:', spectrogram.shape)
    #print('Audio playback')
    #display.display(display.Audio(waveform, rate=16000))


    # get_spectrogram_and_label_idを使用して、データセットの要素全体にDataset.mapをマッピング

    spectrogram_ds = waveform_ds.map(
      map_func=get_spectrogram_and_label_id,
      num_parallel_calls=AUTOTUNE)

    # データセットのさまざまな例についてスペクトログラムを調べます

    train_ds = spectrogram_ds
    val_ds = preprocess_dataset(val_files)
    #test_ds = preprocess_dataset(test_files)

    # モデルトレーニングのトレーニングセットと検証セットをバッチ処理します

    batch_size = 64
    train_ds = train_ds.batch(batch_size)
    val_ds = val_ds.batch(batch_size)

    # Dataset.cacheおよびDataset.prefetch操作を追加して、モデルのトレーニング中の読み取りレイテンシーを減らします

    train_ds = train_ds.cache().prefetch(AUTOTUNE)
    val_ds = val_ds.cache().prefetch(AUTOTUNE)

    #モデルでは、オーディオファイルをスペクトログラム画像に変換したため、単純な畳み込みニューラルネットワーク（CNN）を使用します。
    #tf.keras.Sequentialモデルは、次のKeras前処理レイヤーを使用します。
    #tf.keras.layers.Resizing ：入力をダウンサンプリングして、モデルをより高速にトレーニングできるようにします。
    #tf.keras.layers.Normalization ：平均と標準偏差に基づいて画像の各ピクセルを正規化します。
    #Normalizationレイヤーの場合、集合体統計（つまり、平均と標準偏差）を計算するために、最初にトレーニングデータでそのadaptメソッドを呼び出す必要があります。

    for spectrogram, _ in spectrogram_ds.take(1):
      input_shape = spectrogram.shape
    print('Input shape:', input_shape)
    num_labels = len(commands)

    # Instantiate the `tf.keras.layers.Normalization` layer.
    norm_layer = layers.Normalization()
    # Fit the state of the layer to the spectrograms
    # with `Normalization.adapt`.
    norm_layer.adapt(data=spectrogram_ds.map(map_func=lambda spec, label: spec))

    #モデル構築
    model = models.Sequential([
        layers.Input(shape=input_shape),
        # Downsample the input.
        layers.Resizing(32, 32),
        # Normalize.
        norm_layer,
        layers.Conv2D(32, 3, activation='relu'),
        layers.Conv2D(64, 3, activation='relu'),
        layers.MaxPooling2D(),
        layers.Dropout(0.25),
        layers.Flatten(),
        layers.Dense(128, activation='relu'),
        layers.Dropout(0.5),
        layers.Dense(num_labels),
    ])

    model.summary()

    # Adamオプティマイザーとクロスエントロピー損失を使用してKerasモデルを構成します。

    #学習プロセスの設定
    model.compile(
        optimizer=tf.keras.optimizers.Adam(),
        loss=tf.keras.losses.SparseCategoricalCrossentropy(from_logits=True),
        metrics=['accuracy'],
    )

    # デモンストレーションの目的で、モデルを10エポック以上トレーニングします。

    EPOCHS = 10
    history = model.fit(
        train_ds,
        validation_data=val_ds,
        epochs=EPOCHS,
        callbacks=tf.keras.callbacks.EarlyStopping(verbose=1, patience=2),
    )

    # モデルのパフォーマンスを評価する
    """
    test_audio = []
    test_labels = []

    for audio, label in test_ds:
      test_audio.append(audio.numpy())
      test_labels.append(label.numpy())

    test_audio = np.array(test_audio)
    test_labels = np.array(test_labels)

    y_pred = np.argmax(model.predict(test_audio), axis=1)
    y_true = test_labels

    test_acc = sum(y_pred == y_true) / len(y_true)
    print(f'Test set accuracy: {test_acc:.0%}')
    """

    #モデルを保存する
    model.save('model/modeldata')
else:
    #モデルを読み込む
    model = tf.keras.models.load_model('model/modeldata')

 # オーディオファイルで推論を実行する

#print('check start')
#sample_file = data_dir/'normal/normal.wav'
if colab_flag==True :
  sample_file = '/content/drive/MyDrive/samplewav/test.wav'
else:
    sample_file = input_filename
    print(sample_file)
    #sample_file = 'test.wav'
    #sample_file = 'samplewav/test.wav'
    #sample_file = 'C:/Users/abe60/source/repos/WaveRecMic/biosonoColabTest/samplewav/test.wav'
    #sample_file = data_dir/'cough/cough3.wav'
  
"""
type = [('','*')]
dir = 'C:/Users/abe60/source/repos/biosonoTest/data/mini_speech_commands'
filename = filedialog.askopenfilename(filetypes = type, initialdir = dir)
#print(filename)
if len(filename) == 0:
    print('cancel!!')
else:
"""
#sample_file = pathlib.Path(filename);
sample_ds = preprocess_dataset([str(sample_file)])
#sample_ds = preprocess_dataset([sample_file])
#for spectrogram, label in sample_ds.batch(1):
for spectrogram, label in sample_ds.batch(1):
  prediction = model(spectrogram)
  result =  tf.nn.softmax(prediction[0])
  
  #予測値の配列を得る
  rarray = result.numpy()
  #最大値のINDEXを得る
  index = np.argmax(rarray)
  """
  print("index:"+str(index))
  #予測値のグラフをPLOTする
  plt.bar(commands,result)
  plt.title(f'Predictions for "{commands[index]}"')
  plt.show()
  """
print('result:',commands[index])








