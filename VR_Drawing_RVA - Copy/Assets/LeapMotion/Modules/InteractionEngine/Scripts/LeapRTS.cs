using UnityEngine;

namespace Leap.Unity {

  /// <summary>
  /// Use this component on a Game Object to allow it to be manipulated by a pinch gesture.  The component
  /// allows rotation, translation, and scale of the object (RTS).
  /// </summary>
  public class LeapRTS : MonoBehaviour {

    public enum RotationMethod {
      None,
      Single,
      Full
    }
    public GameObject StateDraw;
        public CapsuleHand HandL;
        public GameObject HandDraw;
        public CapsuleHand _handL;
    [SerializeField]
    private PinchDetector _pinchDetectorA;
    private GameObject PinchLeft;
    public PinchDetector PinchDetectorA {
      get {
        return _pinchDetectorA;
      }
      set {
        
        _pinchDetectorA = value;
      }
    }

    [SerializeField]
    private PinchDetector _pinchDetectorB;
        private GameObject PinchRight;
        public PinchDetector PinchDetectorB {
      get {
        return _pinchDetectorB;
      }
      set {
        _pinchDetectorB = value;
      }
    }

    [SerializeField]
    private RotationMethod _oneHandedRotationMethod;

    [SerializeField]
    private RotationMethod _twoHandedRotationMethod;

    [SerializeField]
    private bool _allowScale = true;

    [Header("GUI Options")]
    [SerializeField]
    private KeyCode _toggleGuiState = KeyCode.None;

    [SerializeField]
    private bool _showGUI = true;

    private Transform _anchor;

    private float _defaultNearClip;

    void Start() {
            

                GameObject pinchControl = new GameObject("RTS Anchor");
      _anchor = pinchControl.transform;
      _anchor.transform.parent = transform.parent;
      transform.parent = _anchor;
    }

        void Awake()
        {

            StateDraw = GameObject.Find("Pinch Drawing");
            HandDraw = GameObject.Find("Capsule Hand Left");


        }

        private void OnDestroy()
        {
           
        }


        void Update() {
            StateDraw.SetActive(true);
            


            PinchLeft = GameObject.Find("HandModelsL");
            Debug.Log(PinchLeft);
            PinchDetectorA = PinchLeft.GetComponentInChildren<PinchDetector>(false);
            _pinchDetectorA = PinchDetectorA;

          

            PinchRight = GameObject.Find("HandModelsR");
            Debug.Log(PinchRight);
            PinchDetectorB = PinchRight.GetComponentInChildren<PinchDetector>(false);
            _pinchDetectorB = PinchDetectorB;

            if (HandDraw != null && HandDraw.activeSelf)
            {
                StateDraw.SetActive(false);
            }

                if (Input.GetKeyDown(_toggleGuiState)) {
        _showGUI = !_showGUI;
      }
            
            bool didUpdate = false;
      if(_pinchDetectorA != null)
        didUpdate |= _pinchDetectorA.DidChangeFromLastFrame;
      if(_pinchDetectorB != null)
        didUpdate |= _pinchDetectorB.DidChangeFromLastFrame;

      if (didUpdate) {
        transform.SetParent(null, true);
      }
      //--------------dua tangan-----------------
      if (_pinchDetectorA != null && _pinchDetectorA.IsActive && 
          _pinchDetectorB != null &&_pinchDetectorB.IsActive) {
                StateDraw.SetActive(false);
                transformDoubleAnchor();
                
      } else if (_pinchDetectorA != null && _pinchDetectorA.IsActive) {
                StateDraw.SetActive(false);
                transformSingleAnchor(_pinchDetectorA);
      } 

      if (didUpdate) {
        transform.SetParent(_anchor, true);
      }
    }

       /* void OnGUI() {
      if (_showGUI) {
        //GUILayout.Label("One Handed Settings");
        doRotationMethodGUI(ref _oneHandedRotationMethod);
       /* GUILayout.Label("Two Handed Settings");
        doRotationMethodGUI(ref  _twoHandedRotationMethod);
        _allowScale = true;
      }
    }*/

    private void doRotationMethodGUI(ref RotationMethod rotationMethod) {
            //GUILayout.BeginHorizontal();

            /*  GUI.color = rotationMethod == RotationMethod.None ? Color.green : Color.white;
              if (GUILayout.Button("No Rotation")) {
                rotationMethod = RotationMethod.None;
              }

              GUI.color = rotationMethod == RotationMethod.Single ? Color.green : Color.white;
              if (GUILayout.Button("Single Axis")) {
                rotationMethod = RotationMethod.Single;
              }

              GUI.color = rotationMethod == RotationMethod.Full ? Color.green : Color.white;
              if (GUILayout.Button("Full Rotation")) {

              }*/
          /*  if (_pinchDetectorA != null && _pinchDetectorA.IsActive &&
             _pinchDetectorB != null && _pinchDetectorB.IsActive)
            {

                rotationMethod = RotationMethod.Full;

            }
            if (_pinchDetectorA != null && _pinchDetectorA.IsActive)
            {
                
                rotationMethod = RotationMethod.Single;
                
            }
            
            else
            {
                rotationMethod = RotationMethod.None;
            }*/
            
            //GUI.color = Color.white;

     // GUILayout.EndHorizontal();
    }

    private void transformDoubleAnchor() {
            
      _anchor.position = (_pinchDetectorA.Position + _pinchDetectorB.Position) / 2.0f;

      /*switch (_twoHandedRotationMethod) {
        case RotationMethod.None:
          break;
        case RotationMethod.Single:*/
                    //_allowScale = false;
                    Vector3 p = _pinchDetectorA.Position;
          p.y = _anchor.position.y;
          _anchor.LookAt(p);
         // break;
       // case RotationMethod.Full:

                    /* Quaternion pp = Quaternion.Lerp(_pinchDetectorA.Rotation, _pinchDetectorB.Rotation, 0.5f);
                     Vector3 u = pp * Vector3.up;
                     _anchor.LookAt(_pinchDetectorA.Position, u);
                     break;*/
                    _allowScale = true;
                   // break;
     // }
      if (_allowScale == true) {
        _anchor.localScale = Vector3.one * Vector3.Distance(_pinchDetectorA.Position, _pinchDetectorB.Position);
      }
    }

    private void transformSingleAnchor(PinchDetector singlePinch) {
      _anchor.position = singlePinch.Position;

     // switch (_oneHandedRotationMethod) {
      //  case RotationMethod.None:
      //    break;
    //    case RotationMethod.Single:
                    _allowScale = false;
          Vector3 p = singlePinch.Rotation * Vector3.right;
          p.y = _anchor.position.y;
          _anchor.LookAt(p);
       //   break;
      //  case RotationMethod.Full:
                     _anchor.rotation = singlePinch.Rotation;
       //             _allowScale = true;
      //    break;
    //  }

      _anchor.localScale = Vector3.one;
    }
  }
}
