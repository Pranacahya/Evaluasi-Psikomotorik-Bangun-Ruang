using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Linq;

namespace Leap.Unity.DetectionExamples
{

	public class PinchDraw : MonoBehaviour {

    [Tooltip("Each pinch detector can draw one line at a time.")]

        private LinkedList<Vector3> points;
        private IDrawable _drawAble;


        [SerializeField]
    private PinchDetector[] _pinchDetectors;

    [SerializeField]
    private Material _material;

    [SerializeField]
    private Color _drawColor = Color.white;
        [SerializeField]
        private int drawMode = 0;

        [SerializeField]
    private float _smoothingDelay = 0.01f;

    [SerializeField]
    private float _drawRadius = 0.002f;

    [SerializeField]
    private int _drawResolution = 8;

    [SerializeField]
    private float _minSegmentLength = 0.005f;

    private DrawState[] _drawStates;
        private GameObject Parent;
        private Vector3 startV3 = Vector3.zero;
        private Vector3 endV3 = Vector3.zero;
        private Vector3 startPoint = Vector3.zero;
        private Vector3 endPoint = Vector3.zero;



        //-----------color-------------

        public void CRed()
        {
            _drawColor = Color.red;
            return;
        }
        public void CBlue()
        {
            _drawColor = Color.blue;
            return;
        }
        public void CGreen()
        {
            _drawColor = Color.green;
            return;
        }
        public void CBlack()
        {
            _drawColor = Color.black;
            return;
        }
        public void CYellow()
        {
            _drawColor = Color.yellow;
            return;
        }

        public Color DrawColor {
      get {
        return _drawColor;
      }
      set {
        _drawColor = value;
      }
    }

    public float DrawRadius {
      get {
        return _drawRadius;
      }
      set {
        _drawRadius = value;
      }
    }

        public void AMode()
        {
            UnityEngine.Debug.Log("Line Mode");
            _drawAble = new LineDrawable();
            return;
        }
        public void BMode()
        {
            UnityEngine.Debug.Log("Triangle Mode");
            _drawAble = new TriDrawable();
            return;
        }
        public void CMode()
        {
            UnityEngine.Debug.Log("Square Mode");
            _drawAble = new QuadDrawable();
            return;
        }

        public IDrawable DrawAble
        {
            get
            {
                return _drawAble;
            }
            set
            {
                _drawAble = value;
            }
        }

        public int DrawModes{ 
        get {
          return drawMode;
        }
        set {
          drawMode = value;
        }

    }

        public static Stopwatch stopWatch = new Stopwatch();
        private Mesh _mesh;

        void OnValidate() {
      _drawRadius = Mathf.Max(0, _drawRadius);
      _drawResolution = Mathf.Clamp(_drawResolution, 3, 24);
      _minSegmentLength = Mathf.Max(0, _minSegmentLength);
    }

    void Awake() {
            points = new LinkedList<Vector3>();
            
            //drawAble = new  TriDrawable();
            if (_pinchDetectors.Length == 0) {
        UnityEngine.Debug.LogWarning("No pinch detectors were specified!  PinchDraw can not draw any lines without PinchDetectors.");
      }
    }

    void Start() {
            _drawAble = new LineDrawable();
            UnityEngine.Debug.Log(_drawAble);
            _drawStates = new DrawState[_pinchDetectors.Length];
      for (int i = 0; i < _pinchDetectors.Length; i++) {
        _drawStates[i] = new DrawState(this);
      }
    }

    void Update() {


            /*if (Input.GetKeyDown(KeyCode.L))
            {
                UnityEngine.Debug.Log(drawAble);
                drawAble = new LineDrawable();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                UnityEngine.Debug.Log(drawAble);
                drawAble = new TriDrawable();
            }
            if (Input.GetKeyDown(KeyCode.M));
            {
                UnityEngine.Debug.Log(drawAble);
                drawAble = new QuadDrawable();
            }*/

            for (int i = 0; i < _pinchDetectors.Length; i++) {
        var detector = _pinchDetectors[i];
        var drawState = _drawStates[i];


        if (detector.DidStartHold) {
                    points.Clear();
                    points.AddLast(detector.Position);
          startV3 = detector.Position;
          startPoint = startV3;
          GameObject temp = drawState.BeginNewLine();
                    if (temp) {
                        Parent = temp;
                    }
          
        }
        
        if (detector.DidRelease) {
                    points.AddLast(detector.Position);
                    endV3 = detector.Position;
          endPoint = endV3;
		  drawState.FinishLine();
          
          stopWatch.Stop();
		  TimeSpan ts = stopWatch.Elapsed;
		  string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
		  //UnityEngine.Debug.Log("RunTime " + elapsedTime);
                    if (Parent)
                    {
                        Destroy(Parent.transform.parent.gameObject);
                    }
                    Parent = null;
                    _mesh = new Mesh();
                    _mesh.name = "Line Mesh";
                    _mesh.MarkDynamic();

                    GameObject lineObj = new GameObject("Line Object");
                    lineObj.AddComponent<LineRenderer>();
                    lineObj.AddComponent<LeapRTS>();
                    LineRenderer lineSave = lineObj.GetComponent<LineRenderer>();
                    //lineObj.transform.localScale = Vector3.one;
                    //lineObj.AddComponent<MeshFilter>().mesh = _mesh;
                    //lineObj.AddComponent<MeshRenderer>().sharedMaterial = _material;

                    Vector3[] res = _drawAble.Draw(points.ToArray());
                    lineSave.positionCount = res.Length;
                    lineSave.SetPositions(res);
                    lineSave.useWorldSpace = false;
                    lineSave.material = _material;
                    lineSave.SetColors(_drawColor, _drawColor);
                    //lineSave.BakeMesh(_mesh, true);
                    //lineSave.transform.localScale = Vector3.one;
                    lineSave.SetWidth(0.01f, 0.01f);


                }

                if (detector.IsHolding) {
          drawState.UpdateLine(detector.Position);
                    points.AddLast(detector.Position);
                }
      }
    }
    
    [System.Serializable]
    private class DrawState {
      private List<Vector3> _vertices = new List<Vector3>();
      private List<int> _tris = new List<int>();
      private List<Vector2> _uvs = new List<Vector2>();
      private List<Color> _colors = new List<Color>();

      private PinchDraw _parent;
      

      private int _rings = 0;

      private Vector3 _prevRing0 = Vector3.zero;
      private Vector3 _prevRing1 = Vector3.zero;

      private Vector3 _prevNormal0 = Vector3.zero;

      private Mesh _mesh;
      private SmoothedVector3 _smoothedPosition;
            private Vector3 startV3 = Vector3.zero;
            private Vector3 endV3 = Vector3.zero;
            private Vector3 startPoint = Vector3.zero;
            private Vector3 endPoint = Vector3.zero;


            public DrawState(PinchDraw parent) {
        _parent = parent;

        _smoothedPosition = new SmoothedVector3();
        _smoothedPosition.delay = parent._smoothingDelay;
        _smoothedPosition.reset = true;
      }

       

      public GameObject BeginNewLine() {
	    stopWatch.Reset();
	    stopWatch.Start();
        _rings = 0;
        _vertices.Clear();
        _tris.Clear();
        _uvs.Clear();
        _colors.Clear();

        _smoothedPosition.reset = true;

        _mesh = new Mesh();
        
        _mesh.name = "Line Mesh";
        _mesh.MarkDynamic();
        

        GameObject lineObj = new GameObject("Line Object");
        lineObj.transform.position = Vector3.zero;
        lineObj.transform.rotation = Quaternion.identity;
        lineObj.transform.localScale = Vector3.one;
        lineObj.AddComponent<MeshFilter>().mesh = _mesh;
        Mesh smesh = lineObj.GetComponent<MeshFilter>().sharedMesh;
        print(smesh.isReadable);
        lineObj.AddComponent<MeshRenderer>().sharedMaterial = _parent._material;
        lineObj.AddComponent<LeapRTS>();
 
        return lineObj;
      }

      public void UpdateLine(Vector3 position) {
        _smoothedPosition.Update(position, Time.deltaTime);

        bool shouldAdd = false;

        shouldAdd |= _vertices.Count == 0;
        shouldAdd |= Vector3.Distance(_prevRing0, _smoothedPosition.value) >= _parent._minSegmentLength;

        if (shouldAdd) {
          addRing(_smoothedPosition.value);
          updateMesh();
        }
      }

    

    public void FinishLine() {
         
        _mesh.UploadMeshData(false);
        //StraightLineNew();   
                
       
        
      }

            public GameObject StraightLineNew()
            {
                stopWatch.Reset();
                stopWatch.Start();
                _rings = 0;
                _vertices.Clear();
                _tris.Clear();
                _uvs.Clear();
                _colors.Clear();
                

                _smoothedPosition.reset = true;

                _mesh = new Mesh();

                _mesh.name = "Line Mesh";
                _mesh.MarkDynamic();


                GameObject straightLine = new GameObject("Straight Line");
                LineRenderer lineRenderer = straightLine.AddComponent<LineRenderer>();
                lineRenderer.SetPositions(new Vector3[] { startPoint, endPoint });
                
                //straightLine.transform.rotation = Quaternion.identity;
                //straightLine.transform.localScale = Vector3.one;
                //straightLine.AddComponent<MeshFilter>().mesh = _mesh;
               // Mesh smesh = straightLine.GetComponent<MeshFilter>().sharedMesh;
                //print(smesh.isReadable);
                //straightLine.AddComponent<MeshRenderer>().sharedMaterial = _parent._material;
                straightLine.AddComponent<LeapRTS>();  

                return straightLine;
            }

            private void updateMesh() {
         
        _mesh.SetVertices(_vertices);
        _mesh.SetColors(_colors);
        _mesh.SetUVs(0, _uvs);
        _mesh.SetIndices(_tris.ToArray(), MeshTopology.Triangles, 0);
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
      }

      private void addRing(Vector3 ringPosition) {
        _rings++;

        if (_rings == 1) {
          addVertexRing();
          addVertexRing();
          addTriSegment();
        }

        addVertexRing();
        addTriSegment();

        Vector3 ringNormal = Vector3.zero;
        if (_rings == 2) {
          Vector3 direction = ringPosition - _prevRing0;
          float angleToUp = Vector3.Angle(direction, Vector3.up);

          if (angleToUp < 10 || angleToUp > 170) {
            ringNormal = Vector3.Cross(direction, Vector3.right);
          } else {
            ringNormal = Vector3.Cross(direction, Vector3.up);
          }

          ringNormal = ringNormal.normalized;

          _prevNormal0 = ringNormal;
        } else if (_rings > 2) {
          Vector3 prevPerp = Vector3.Cross(_prevRing0 - _prevRing1, _prevNormal0);
          ringNormal = Vector3.Cross(prevPerp, ringPosition - _prevRing0).normalized;
        }

        if (_rings == 2) {
          updateRingVerts(0,
                          _prevRing0,
                          ringPosition - _prevRing1,
                          _prevNormal0,
                          0);
        }

        if (_rings >= 2) {
          updateRingVerts(_vertices.Count - _parent._drawResolution,
                          ringPosition,
                          ringPosition - _prevRing0,
                          ringNormal,
                          0);
          updateRingVerts(_vertices.Count - _parent._drawResolution * 2,
                          ringPosition,
                          ringPosition - _prevRing0,
                          ringNormal,
                          1);
          updateRingVerts(_vertices.Count - _parent._drawResolution * 3,
                          _prevRing0,
                          ringPosition - _prevRing1,
                          _prevNormal0,
                          1);
        }
        
        _prevRing1 = _prevRing0;
        _prevRing0 = ringPosition;

        _prevNormal0 = ringNormal;
      }

      private void addVertexRing() {
        for (int i = 0; i < _parent._drawResolution; i++) {
          _vertices.Add(Vector3.zero);  //Dummy vertex, is updated later
          _uvs.Add(new Vector2(i / (_parent._drawResolution - 1.0f), 0));
          _colors.Add(_parent._drawColor);
        }
      }

      //Connects the most recently added vertex ring to the one before it
      private void addTriSegment() {
        for (int i = 0; i < _parent._drawResolution; i++) {
          int i0 = _vertices.Count - 1 - i;
          int i1 = _vertices.Count - 1 - ((i + 1) % _parent._drawResolution);

          _tris.Add(i0);
          _tris.Add(i1 - _parent._drawResolution);
          _tris.Add(i0 - _parent._drawResolution);

          _tris.Add(i0);
          _tris.Add(i1);
          _tris.Add(i1 - _parent._drawResolution);
        }
      }

      private void updateRingVerts(int offset, Vector3 ringPosition, Vector3 direction, Vector3 normal, float radiusScale) {
        direction = direction.normalized;
        normal = normal.normalized;

        for (int i = 0; i < _parent._drawResolution; i++) {
          float angle = 360.0f * (i / (float)(_parent._drawResolution));
          Quaternion rotator = Quaternion.AngleAxis(angle, direction);
          Vector3 ringSpoke = rotator * normal * _parent._drawRadius * radiusScale;
          _vertices[offset + i] = ringPosition + ringSpoke;
        }
      }
    }
  }
}
