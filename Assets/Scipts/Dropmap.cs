using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropmap : MonoBehaviour {

    public void mapdrop()
    {
        var map = GetComponent<Rigidbody>();
        if (map = null)
            return;
        //set the gravity to true
        map.useGravity = true;
           
    }

}
