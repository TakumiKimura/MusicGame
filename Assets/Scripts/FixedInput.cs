using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedInput : MonoBehaviour {

    static FixedInput instance = null;

    int[] inputs = new int[510];
    bool[] inputsDown = new bool[510];

    public static bool GetKey(KeyCode key)
    {
        return instance != null ? instance.inputs[(int)key] > 0 : false;
    }

    public static bool GetKeyDown(KeyCode key)
    {
        return instance != null ? instance.inputs[(int)key] == 1 : false;
    }

    public static bool GetKeyUp(KeyCode key)
    {
        return instance != null ? instance.inputs[(int)key] == 0 && instance.inputsDown[(int)key] : false;
    }

    void Awake()
    {
        if(instance != null)
        {
            enabled = false;
            DestroyImmediate(this);
            instance = null;
            return;
        }
        instance = this;
    }

    void FixedUpdate()
    {
        for(int i = 0; i < 510; ++i)
        {
            inputsDown[i] = inputs[i] > 0;
            inputs[i] = Input.GetKey((KeyCode)i) ? inputs[i] + 1 : 0;
        }
    }	
}
