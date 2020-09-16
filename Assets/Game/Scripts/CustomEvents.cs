using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEvents : MonoBehaviour
{
    public delegate void ResetWorld(int worldOffset);
    public static event ResetWorld OnWorldReset;
}
