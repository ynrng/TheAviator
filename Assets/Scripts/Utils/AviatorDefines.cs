using System;
using UnityEngine;

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
    public static Color Sky = GetColor("#e4e0ba");
    public static Color Fog = GetColor("#f7d9aa");
    public static Color Light = GetColor("#dc887480");
    public static Color Red = GetColor("#f25346");
    public static Color White = GetColor("#d8d0d1");
    public static Color WhiteTransparent = GetColor("#d8d0d14d");
    public static Color Brown = GetColor("#59332e");
    public static Color Pink = GetColor("#F5986E");
    public static Color BrownDark = GetColor("#23190f");
    public static Color Green = GetColor("#009999");
    public static Color Blue = GetColor("#68c3c0cc"); //opacity 0.8

    private static Color GetColor(string hex)
    {
        hex = hex.PadRight(9, 'f');
        return new Color(r: Convert.ToInt32(hex.Substring(1, 2), 16) / 255f,
        g: Convert.ToInt32(hex.Substring(3, 2), 16) / 255f,
        b: Convert.ToInt32(hex.Substring(5, 2), 16) / 255f,
        a: Convert.ToInt32(hex.Substring(7, 2), 16) / 255f
        );
    }

}

public struct Aviator {
    //mouse control or touch control
    public static Vector2 mousePos;
    public static float scaler = 25;

    //below is original project vars
    #region initial game data
    public static float speed;
    public const float initSpeed = 7f;
    public static float baseSpeed;
    public static float targetBaseSpeed;
    public const float incrementSpeedByTime = .5f;//.05f;
    public const float incrementSpeedByLevel = 1f;//.1f;
    public const int distanceForSpeedUpdate = 100;
    public static float speedLastUpdate;

    public static float distance;
    public const int ratioSpeedDistance = 3; // 50

    public static float energy;
    public const float ratioSpeedEnergy = .3f;// 3;

    public static int level;
    public static float levelLastUpdate;
    public const int distanceForLevelUpdate = 1000;

    public const float planeDefaultHeight = 100;
    public const float planeAmpHeight = 80;
    public const float planeAmpWidth = 75;
    public const float planeMoveSensivity = 2f;
    // public static float planeRotXSensivity;
    // public static float planeRotZSensivity;

    // public static float planeFallSpeed;
    public const float planeMinSpeed = 1.2f;
    public const float planeMaxSpeed = 1.6f;
    public static float planeSpeed; // only dependent on mousePos.x; clamp between planeMinSpeed & planeMaxSpeed
    // public static float planeCollisionDisplacementX;
    // public static float planeCollisionSpeedX;

    // public static float planeCollisionDisplacementY;
    // public static float planeCollisionSpeedY;

    public const int seaRadius = 600;
    public const int seaLength = 800;
    public const float seaRotationSpeed = 0.006f;
    public const float wavesMinAmp = 5f;
    public const float wavesMaxAmp = 20f;
    public const float wavesMinSpeed = 0.001f;
    public const float wavesMaxSpeed = 0.003f;

    // public const float cameraFarPos = 500;
    // public const float cameraNearPos = 150;
    public const float cameraSensivity = 0.002f;

    // public static float coinDistanceTolerance;
    const int coinValue = 3;
    // public static float coinsSpeed;
    // public static float coinLastSpawn;
    // public static float distanceForCoinsSpawn;

    // // public static float ennemyDistanceTolerance;
    const int ennemyValue = 10;
    // public static float ennemiesSpeed;
    // public static float ennemyLastSpawn;
    // public static float distanceForEnnemiesSpawn;

    public static AviatorStates status;
    #endregion

    public static void reset()
    {
        mousePos = Vector2.zero;

        speed = 0f;
        baseSpeed = initSpeed;
        targetBaseSpeed = initSpeed;
        speedLastUpdate = 1f;

        distance = 0f;
        energy = 100f;

        level = 1;
        levelLastUpdate = 1f;

        planeSpeed = planeMinSpeed;

        // coinsSpeed = .5f;
        // coinLastSpawn = 0;
        // distanceForCoinsSpawn = 100;

        // ennemiesSpeed = .6f;
        // ennemyLastSpawn = 0;
        // distanceForEnnemiesSpawn = 50;

        status = AviatorStates.Start;
    }

    public static void addEnergy()
    {
        energy += coinValue;
        energy = Mathf.Min(energy, 100);
    }

    public static void removeEnergy()
    {
        energy -= ennemyValue;
        energy = Mathf.Max(0, energy);
    }
    public static float normalize(float v, float vmin, float vmax, float tmin, float tmax)
    {
        float nv = Mathf.Clamp(v, vmin, vmax);
        float pc = (nv - vmin) / (vmax - vmin);
        float dt = tmax - tmin;
        float tv = tmin + (pc * dt);
        return tv;
    }
}
