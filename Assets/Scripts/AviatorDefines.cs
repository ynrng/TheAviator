using System;
using UnityEngine;

public enum BlendMode {
    Opaque,
    Cutout,
    Fade,
    Transparent
}
public struct AviatorColors {
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
        return new Color(
            r: Convert.ToInt32(hex.Substring(1, 2), 16) / 255f,
            g: Convert.ToInt32(hex.Substring(3, 2), 16) / 255f,
            b: Convert.ToInt32(hex.Substring(5, 2), 16) / 255f,
            a: Convert.ToInt32(hex.Substring(7, 2), 16) / 255f
        );
    }
}