using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameFixed : MonoBehaviour
{
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(400, 640, false, 60);
    }
}
