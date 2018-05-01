using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Windows.Speech;

public class Helper : MonoBehaviour {
    //drop the map
    public void dropmap()
    {
        var rb = GetComponent<Rigidbody>();
        if (rb == null)
            return;
        // set gravity to true
        rb.useGravity = true;
    }

}
