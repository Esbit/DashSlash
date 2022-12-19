using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum VFX
{
    HIT1,
    HIT2,
    HIT3,
    COUNT,
    NONE
}

/// <summary>
/// Static class designed to create one-shot vfx instances in specific locations
/// </summary>
public class VFXFactory
{
    /// <summary>
    /// Default constructor
    /// Private to prevent this class from being instantiated
    /// </summary>
    private VFXFactory()
    {
  
    }

    public static GameObject CreateVFX(VFX type, Vector3 position, Quaternion? rotation = null)
    {
        if (rotation == null) rotation = Quaternion.identity;
        GameObject vfx = (GameObject)Object.Instantiate(Resources.Load("VFX/" + type.ToString()), position, (Quaternion)rotation, null);
        return vfx;
    }
}
