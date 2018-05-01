using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Windows.Speech;

public class Plane : MonoBehaviour {

    public Material MaterialInGaze;
    private Material _oldMaterial;
    private GameObject _objectInFocus;

    // Use this for initialization
    void Start () {
        GestureRecognizer gesturerecognizer = new GestureRecognizer();
        // set wht tpye of gestures settings you want ex tap
        gesturerecognizer.SetRecognizableGestures(GestureSettings.Tap);
        gesturerecognizer.TappedEvent += Gesturerecognizer_TappedEvent;
        // start the event
        gesturerecognizer.StartCapturingGestures();
	}

    private void Gesturerecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        //are we looking at the object
        if (_objectInFocus == null)
            return;
        //call helpers class dropmap
        _objectInFocus.SendMessage("dropmap");
    }

    // Update is called once per frame
    void Update () {
        var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit rayinfo;
        if (Physics.Raycast(ray, out rayinfo))
        {
            var hitObject = rayinfo.transform.gameObject;
            if (hitObject == _objectInFocus)
                return;
            var renderer = hitObject.GetComponent<Renderer>();
            if (renderer == null)
                return;
            _oldMaterial = renderer.material;
            renderer.material = MaterialInGaze;
            _objectInFocus = hitObject;

        }
        else
        {
            if (_objectInFocus == null)
                return;
            var renderer = _objectInFocus.GetComponent<Renderer>();
            renderer.material = _oldMaterial;
            _objectInFocus = null;
        }
    }
}
