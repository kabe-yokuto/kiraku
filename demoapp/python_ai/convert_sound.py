import librosa
import pathlib
import soundfile as sf

data_path = "" #変換するデータが入ったフォルダのPATH
SampRate = 16000 #SamplingRate

dataset_dir = pathlib.Path(data_path)

dataset_wav = list(dataset_dir.glob('**/*.wav'))
for wav_path in dataset_wav:
    data, sr = librosa.core.load(wav_path, sr=SampRate, mono = True)
    sf.write(wav_path, data, sr)
    
dataset_mp3 = list(dataset_dir.glob('**/*.mp3'))
for mp3_path in dataset_mp3:
    data, sr = librosa.core.load(mp3_path, sr=SampRate, mono = True)
    wav_path = mp3_path.with_suffix('.wav')
    sf.write(wav_path, data, sr, format="WAV")
    
dataset_aif = list(dataset_dir.glob('**/*.aif'))
for aif_path in dataset_aif:
    data, sr = librosa.core.load(aif_path, sr=SampRate, mono = True)
    wav_path = aif_path.with_suffix('.wav')
    sf.write(wav_path, data, sr, format="WAV")