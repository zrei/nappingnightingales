using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAndKeyController : MonoBehaviour
{
    private GateController childGate;

    private void Awake()
    {
        childGate = gameObject.transform.Find("Gate").GetComponent<GateController>();
    }
    
    public void KeyObtain()
    {
        childGate.OpenGate();
    }
}