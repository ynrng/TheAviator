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
    public static Color Fog = new Color(r: 247 / 255, g: 217 / 255, b: 170 / 255);
    public static Color Red = AviatorUtil.GetColor("0xf25346");
    public static Color White = AviatorUtil.GetColor("0xd8d0d1");
    public static Color Brown = AviatorUtil.GetColor("0x59332e");
    public static Color Pink = AviatorUtil.GetColor("0xF5986E");
    public static Color BrownDark = AviatorUtil.GetColor("0x23190f");
    public static Color Blue = AviatorUtil.GetColor("0x68c3c0");

}



class AviatorUtil {
    public static Color GetColor(string hex)
    {
        hex = hex.PadRight(10, 'f');
        return new Color(r: Convert.ToInt32(hex.Substring(2, 2), 16) / 255, g: Convert.ToInt32(hex.Substring(4, 2), 16) / 255, b: Convert.ToInt32(hex.Substring(6, 2), 16) / 255);
    }
}
