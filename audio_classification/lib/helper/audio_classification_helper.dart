/*
 * Copyright 2023 The TensorFlow Authors. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *             http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

import 'dart:developer';
import 'dart:typed_data';
import 'package:flutter/cupertino.dart';
import 'package:flutter/services.dart';
import 'package:tflite_flutter/tflite_flutter.dart';
import 'package:fftea/fftea.dart';

class AudioClassificationHelper {
  //static const _modelPath = 'assets/models/yamnet.tflite';
  //static const _labelsPath = 'assets/models/yamnet_label_list.txt';
  static const _modelPath = 'assets/models/kmodeldata.tflite';
  static const _labelsPath = 'assets/models/kmodeldata.txt';

  late Interpreter _interpreter;
  late final List<String> _labels;
  late Tensor _inputTensor;
  late Tensor _outputTensor;

  Future<void> _loadModel() async {
    final options = InterpreterOptions();
    // Load model from assets
    _interpreter = await Interpreter.fromAsset(_modelPath, options: options);

    _inputTensor = _interpreter.getInputTensors().first;
    log(_inputTensor.shape.toString());
    _outputTensor = _interpreter.getOutputTensors().first;
    log(_outputTensor.shape.toString());
    log('Interpreter loaded successfully');

    debugPrint("inputTensort:${_inputTensor}");
    debugPrint("outputTensor:${_outputTensor}");
  }

  // Load labels from assets
  Future<void> _loadLabels() async {
    final labelTxt = await rootBundle.loadString(_labelsPath);
    _labels = labelTxt.split('\n');
  }

  Future<void> initHelper() async {
    await _loadLabels();
    await _loadModel();
  }


  List<List<List<double>>> transSTFT(Float64List soundData, int frameLength, int frameStep){
    final stft = STFT(frameLength, Window.hamming(frameLength));
    final spectrogram = <List<List<double>>>[];
    List<List<double>> addNewAxis(Float64List freq){
      List<List<double>> rt = <List<double>>[];
      for(double x in freq){
        rt.add([x]);
      }
      return rt;
    }
    stft.run(soundData,
            (Float64x2List freq) {
          spectrogram.add(addNewAxis(freq.discardConjugates().magnitudes()));},
        frameStep);
    return spectrogram;
  }


  Future<Map<String, double>> inference(Float32List input) async {
    //final output = [List<double>.filled(521, 0.0)];
    Float64List input64 = Float64List.fromList(input.map((e) => e.toDouble()).toList());

    List<List<List<double>>> stftData = transSTFT(input64, 255, 128);

    List<List<List<List<double>>>> paddedAudioData =
    [
      stftData,
    ];

    final output = [List<double>.filled(4, 0.0)];
    //_interpreter.run(List.of(input), output);
    _interpreter.run(List.of(paddedAudioData), output);
    var classification = <String, double>{};
    for (var i = 0; i < output[0].length; i++) {
      // Set label: points
      classification[_labels[i]] = output[0][i];
    }
    return classification;
  }

  closeInterpreter() {
    _interpreter.close();
  }
}
