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

public struct AviatorColors {
    // background: linear-gradient(#e4e0ba, #f7d9aa);
    public static Color Sky = GetColor("0xe4e0ba");
    public static Color Fog = GetColor("0xf7d9aa");
    public static Color Light = GetColor("0xdc887480");

    public static Color Red = GetColor("0xf25346");
    public static Color White = GetColor("0xd8d0d1");
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
    public static float SpinningSpeed = 20f;
    public static int Center = -600;

}
