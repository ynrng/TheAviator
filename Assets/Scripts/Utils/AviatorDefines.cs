using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

// public class AviatorDefines {
public enum AviatorStates : byte {
    Start,
    Rising,
    Flying,
    Falling,
    // End
}

public enum BlendMode {
    Opaque,
    Cutout,
    Fade,   // Old school alpha-blending mode, fresnel does not affect amount of transparency
    Transparent // Physically plausible transparency mode, implemented as alpha pre-multiply
}

public struct AviatorColors {
    // background: linear-gradient(#e4e0ba, #f7d9aa);
    public static Color Sky = GetColor("0xe4e0ba");
    public static Color Fog = GetColor("0xf7d9aa");
    public static Color Light = GetColor("0xdc887480");

    public static Color Red = GetColor("0xf25346");
    public static Color White = GetColor("0xd8d0d1");
    public static Color WhiteTransparent = GetColor("0xd8d0d14d");
    public static Color Brown = GetColor("0x59332e");
    public static Color Pink = GetColor("0xF5986E");
    public static Color BrownDark = GetColor("0x23190f");
    public static Color Blue = GetColor("0x68c3c0cc"); //opacity 0.8
    private static Color GetColor(string hex)
    {
        hex = hex.PadRight(10, 'f');
        return new Color(r: Convert.ToInt32(hex.Substring(2, 2), 16) / 255f,
        g: Convert.ToInt32(hex.Substring(4, 2), 16) / 255f,
        b: Convert.ToInt32(hex.Substring(6, 2), 16) / 255f,
        a: Convert.ToInt32(hex.Substring(8, 2), 16) / 255f
        );
    }

}

public struct Aviator {
    //mouse control or touch control
    public static Vector2 mousePos;

    public static int SpinningSpeed;

    //below is original project vars
    #region initial game data
    public static int speed = 0;
    public static float initSpeed = .00035f;
    public static float baseSpeed = .00035f;
    public static float targetBaseSpeed = .00035f;
    public static float incrementSpeedByTime = .0000025f;
    public static float incrementSpeedByLevel = .000005f;
    public static float distanceForSpeedUpdate = 100f;
    public static float speedLastUpdate = 0f;

    public static int distance;
    public static float ratioSpeedDistance = 50;
    public static int energy;
    public static int ratioSpeedEnergy = 3;

    public static int level;
    public static int levelLastUpdate = 0;
    public static int distanceForLevelUpdate = 1000;

    public static float planeDefaultHeight = 100;
    public static float planeAmpHeight = 80;
    public static float planeAmpWidth = 75;
    public static float planeMoveSensivity;
    public static float planeRotXSensivity = 0.0008f;
    public static float planeRotZSensivity;
    public static float planeFallSpeed = .001f;
    public static float planeMinSpeed = 1.2f;
    public static float planeMaxSpeed = 1.6f;
    public static float planeSpeed = 0;
    public static float planeCollisionDisplacementX = 0;
    public static float planeCollisionSpeedX = 0;

    public static float planeCollisionDisplacementY = 0;
    public static float planeCollisionSpeedY = 0;

    public static int seaRadius;
    public static int seaLength;
    public static float seaRotationSpeed = 0.006f;
    public static float wavesMinAmp = 5f;
    public static float wavesMaxAmp = 20f;
    public static float wavesMinSpeed = 0.001f;
    public static float wavesMaxSpeed = 0.003f;

    public static float cameraFarPos = 500;
    public static float cameraNearPos = 150;
    public static float cameraSensivity = 0.002f;

    public static float coinDistanceTolerance = 15;
    public static int coinValue = 3;
    public static float coinsSpeed = .5f;
    public static float coinLastSpawn = 0;
    public static float distanceForCoinsSpawn = 100;

    public static float ennemyDistanceTolerance = 10;
    public static int ennemyValue = 10;
    public static float ennemiesSpeed = .6f;
    public static float ennemyLastSpawn = 0;
    public static float distanceForEnnemiesSpawn = 50;

    public static AviatorStates status;
    #endregion

    public static void reset()
    {
        mousePos = Vector2.zero;
        SpinningSpeed = 20;

        speed = 0;
        initSpeed = .00035f;
        baseSpeed = .00035f;
        targetBaseSpeed = .00035f;
        incrementSpeedByTime = .0000025f;
        incrementSpeedByLevel = .000005f;
        distanceForSpeedUpdate = 100f;
        speedLastUpdate = 0f;

        distance = 0;
        ratioSpeedDistance = 50;
        energy = 100;
        ratioSpeedEnergy = 3;

        level = 1;
        levelLastUpdate = 0;
        distanceForLevelUpdate = 1000;

        planeDefaultHeight = 100;
        planeAmpHeight = 80;
        planeAmpWidth = 75;
        planeMoveSensivity = 2f;
        planeRotXSensivity = 0.0008f;
        planeRotZSensivity = 0.5f;
        planeFallSpeed = .001f;
        planeMinSpeed = 1.2f;
        planeMaxSpeed = 1.6f;
        planeSpeed = 0;
        planeCollisionDisplacementX = 0;
        planeCollisionSpeedX = 0;

        planeCollisionDisplacementY = 0;
        planeCollisionSpeedY = 0;

        seaRadius = 600;
        seaLength = 800;
        seaRotationSpeed = 0.006f;
        wavesMinAmp = 5f;
        wavesMaxAmp = 20f;
        wavesMinSpeed = 0.001f;
        wavesMaxSpeed = 0.003f;

        cameraFarPos = 500;
        cameraNearPos = 150;
        cameraSensivity = 0.002f;

        coinDistanceTolerance = 15;
        coinValue = 3;
        coinsSpeed = .5f;
        coinLastSpawn = 0;
        distanceForCoinsSpawn = 100;

        ennemyDistanceTolerance = 10;
        ennemyValue = 10;
        ennemiesSpeed = .6f;
        ennemyLastSpawn = 0;
        distanceForEnnemiesSpawn = 50;

        status = AviatorStates.Start;
    }

}
