using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Windows.Speech;

public class Map : MonoBehaviour {
    public Material MaterialInGaze;
    private Material _oldMaterial;
    private GameObject _objectInFocus;
    public GameObject ObjectToReset;
    public GameObject snowman;
    // Use this for initialization
    void Start () {
        //allow gesture
        GestureRecognizer tapinfo = new GestureRecognizer();
        //set the gesture type to tap
        tapinfo.SetRecognizableGestures(GestureSettings.Tap);
        //create a class for tap
        tapinfo.TappedEvent += Tapinfo_TappedEvent;
        tapinfo.StartCapturingGestures();
        

        //reset the map to the center of the hololens
        KeywordRecognizer resetmap = new KeywordRecognizer(new[] { "Map" });
        resetmap.OnPhraseRecognized += Resetmap_OnPhraseRecognized;
        resetmap.Start();

        //pin move to the right
        KeywordRecognizer moveright = new KeywordRecognizer(new[] { "right" });
        moveright.OnPhraseRecognized += Moveright_OnPhraseRecognized;
        moveright.Start();

        //pin move to the left
        KeywordRecognizer moveleft = new KeywordRecognizer(new[] { "left" });
        moveleft.OnPhraseRecognized += Moveleft_OnPhraseRecognized;
        moveleft.Start();

        //pin move down
        KeywordRecognizer movedown = new KeywordRecognizer(new[] { "down" });
        movedown.OnPhraseRecognized += Movedown_OnPhraseRecognized;
        movedown.Start();

        //pin move up
        KeywordRecognizer moveup = new KeywordRecognizer(new[] { "up"});
        moveup.OnPhraseRecognized += Moveup_OnPhraseRecognized;
        moveup.Start();
	}

    private void Moveup_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (snowman == null)
            return;
        var rb = snowman.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        snowman.transform.position = new Vector3(0f, 0f, 0f);
    }

    private void Movedown_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if(snowman == null)
            return;
        var rb = snowman.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        snowman.transform.position = new Vector3(0f, 0f, 0f);
    }

    private void Moveleft_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (snowman == null)
            return;
        var rb = snowman.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        snowman.transform.position = new Vector3(0f, 0f, 0f);
    }

    private void Moveright_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (snowman == null)
            return;
        var rb = snowman.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }

    private void Resetmap_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (ObjectToReset == null)
            return;

        var rb = ObjectToReset.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        
        ObjectToReset.transform.position = new Vector3(0f, 0f, 2f);
    }

    private void Tapinfo_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {   
        //object is not in front of the hololens
        if (_objectInFocus == null)
            return;
        //call the class mapdrop from dropmap.cs
        _objectInFocus.SendMessage("mapdrop");
    }

    // Update is called once per frame
    void Update () {
        var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit rayinfo;
        if (Physics.Raycast(ray, out rayinfo))
        {
            var hitobj = rayinfo.transform.gameObject;
            if (hitobj == _objectInFocus)
                return;
            var renderer = hitobj.GetComponent<Renderer>();
            if (renderer == null)
                return;
            _oldMaterial = renderer.material;
            //renderer.material = MaterialInGaze;
            _objectInFocus = hitobj;
        }
        else
        {
            if (_objectInFocus == null)
                return;
            var renderer = _objectInFocus.GetComponent<Renderer>();
            //renderer.material = _oldMaterial;
            _objectInFocus = null;
        }
	}
}
