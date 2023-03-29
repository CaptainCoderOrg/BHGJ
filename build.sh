#!/bin/bash
dotnet build CaptainCoder.Core/ -c Release
dotnet build CaptainCoder.BloodyTetris/ -c Release
CORE_PATH="CaptainCoder.Core/CaptainCoder/Core/bin/Release/netstandard2.1"
CORE="CaptainCoder.Core"
CORE_UNITY_PATH="CaptainCoder.Core/CaptainCoder/Core.UnityEngine/bin/Release/netstandard2.1"
CORE_UNITY="CaptainCoder.Core.UnityEngine"
BLOODY_TETRIS_PATH="CaptainCoder.BloodyTetris/BloodyTetris/bin/Release/netstandard2.1"
BLOODY_TETRIS="CaptainCoder.BloodyTetris"
UNITY_DLL_PATH="Bloody Tetris/Assets/Plugins/CaptainCoder"
cp "$CORE_PATH/$CORE.dll" \
    "$CORE_PATH/$CORE.xml" \
    "$CORE_UNITY_PATH/$CORE_UNITY.dll" \
    "$CORE_UNITY_PATH/$CORE_UNITY.xml" \
    "$BLOODY_TETRIS_PATH/$BLOODY_TETRIS.dll" \
    "$UNITY_DLL_PATH/"