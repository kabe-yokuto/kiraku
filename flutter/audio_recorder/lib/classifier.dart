import 'dart:io';
import 'dart:typed_data';
import 'package:flutter/cupertino.dart';
import 'package:tflite_flutter/tflite_flutter.dart';
import 'package:path_provider/path_provider.dart';
import 'package:flutter/services.dart';
import 'package:fftea/fftea.dart';

class Classifier {
  // name of the model file
  final _modelFile = 'assets/kmodeldata.tflite';
  final _labelFile = 'assets/kmodeldata.txt';

  final int frameRate = 16000;

  // TensorFlow Lite Interpreter object
  late Interpreter _interpreter;

  List<int> audioData = [];

  Classifier() {
    // Load model when the classifier is initialized.
    _loadModel();
  }

  void _loadModel() async {
    // Creating the interpreter using Interpreter.fromAsset
    _interpreter = await Interpreter.fromAsset(_modelFile);
    print('Interpreter loaded successfully');
  }

  Future<void> _loadWavFile() async {
    try {
      // Get the directory for the app's data
      //Directory appDir = await getApplicationDocumentsDirectory();
      //String filePath = 'assets/test.wav';

      ByteData data = await rootBundle.load('assets/test.wav');
      Uint8List wavBytes = data.buffer.asUint8List();

      // Convert bytes to 16-bit audio data
      List<double> audioData = [];
      for (int i = 44; i < wavBytes.length; i += 2) {

        int sample = wavBytes[i] + (wavBytes[i + 1] << 8);
        audioData.add(sample.toDouble());
      }

      debugPrint("audio size : ${audioData.length*2} Bytes");

      Float64List audioData2 = Float64List.fromList(audioData);
      // 2秒に斬り出す
      Float64List cutAudio = audioData2.sublist(0,frameRate*2);

      // STFT
      List<Float64List> stftData = transSTFT(cutAudio, 255, 128);

      //this.audioData = audioData;

      // Set the audioData to the interpreter input tensor
      var inputTensor = _interpreter.getInputTensors().first;

      debugPrint("inputTensor:${inputTensor}");

      List<int> inputShape = inputTensor.shape;

      // Get the width and height from the input shape
      int modelInputWidth = inputShape[2];
      int modelInputHeight = inputShape[1];

      // Resize audio data to match model input size
      //List<int> resizedAudioData = resizeAudioData(audioData, modelInputWidth, modelInputHeight);

      // Set the resized audio data to the interpreter input tensor
      var inputBuffer = inputTensor.data; // Get the data of the input tensor
      //Uint8List inputUint8List = Uint8List.fromList(resizedAudioData);
      // Float64Listからintに変換する
      /*
      List<int> intList = [];
      for (var floatList in stftData) {
        for (var floatValue in floatList) {
          intList.add(floatValue.toInt());
        }
      }
      */

      Uint8List uint8List = Uint8List.fromList(stftData.expand((floatList) => floatList.map((value) => value.toInt())).toList());

      inputBuffer.setAll(0, uint8List); // Set resized audio data to input buffer

    } catch (e) {
      print('Error loading WAV file: $e');
    }
  }

  List<Float64List> transSTFT(Float64List soundData, int frameLength, int frameStep){
    final stft = STFT(frameLength, Window.hamming(frameLength));
    final spectrogram = <Float64List>[];
    stft.run(soundData,
            (Float64x2List freq) {
          spectrogram.add(freq.discardConjugates().magnitudes());},
        frameStep);
    return spectrogram;
  }

  List<int> resizeAudioData(List<int> audioData, int targetWidth, int targetHeight) {
    // オーディオデータの中央部分を切り取る
    int startX = (audioData.length - targetWidth) ~/ 2;
    int startY = (audioData.length - targetHeight) ~/ 2;
    return audioData.sublist(startY, startY + targetHeight);
  }

  Future<List<double>> classify(String path) async {
    // Load the WAV file and set the audio data to the input tensor
    await _loadWavFile();

    // Get the input and output tensors
    var inputTensor = _interpreter.getInputTensors().first;
    var outputTensor = _interpreter.getOutputTensors().first;

    // Run inference

    _interpreter.run(inputTensor, outputTensor);

    // Get the output data from the output tensor
    var outputBuffer = outputTensor.data;
    List<double> convertedOutput = outputBuffer.buffer.asFloat32List().toList();

    // Return the output
    return convertedOutput;
  }

}