using UnityEngine;

public class Portal : MonoBehaviour {
    public Portal Other;
    public Camera PortalView;
    private void Start() {
        Other.PortalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        GetComponentInChildren<MeshRenderer>().sharedMaterial.mainTexture = Other.PortalView.targetTexture;
    }
    private void Update() {
        Vector3 lookerPosition = Other.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
                lookerPosition = new Vector3(-lookerPosition.x, lookerPosition.y, -lookerPosition.z);
        PortalView.transform.localPosition  = lookerPosition;

        Quaternion difference = transform.rotation * Quaternion.Inverse(Other.transform.rotation * Quaternion.Euler(0,180,0));
        PortalView.transform.rotation = difference * Camera.main.transform.rotation;

        PortalView.nearClipPlane = lookerPosition.magnitude;
    }
    /*
     [SerializeField] private Portal _other;
    [SerializeField] private Collider[] _wallColliders;
    [SerializeField] private PortalRenderer _renderer;

    private Teleporter _enteredObject;

    public void Render(Camera mainCamera) {
        _renderer.Render(mainCamera, _other.transform);
    }

    private void OnTriggerEnter(Collider col) {
        var teleporter = col.GetComponent<Teleporter>();
        if (teleporter != null) {
            _enteredObject = teleporter;
            teleporter.EnterPortal(this, _other, _wallColliders);
        }
    }

    private void OnTriggerExit(Collider col) {
        var teleporter = col.GetComponent<Teleporter>();
        //teleporter.ExitPortal(_wallColliders);
        _enteredObject = null;
    }

    private void Update() {
        if (_enteredObject != null){
            Vector3 relativePosition = transform.InverseTransformPoint(_enteredObject.transform.position);
            if (relativePosition.z > 0.0f)
                _enteredObject.Teleport();
        }
    }

    
public class CameraTeleport : MonoBehaviour {
    private Camera MyCamera;

    [SerializeField] 
    private Portal[] Portals;

    void Awake() {
        MyCamera = GetComponent<Camera>();
    }

    private void OnPreRender() {
        //for (int i = 0; i < Portals.Length; i++)
            //Portals[i].Render(MyCamera);
    }
     PortalRenderer
      [SerializeField] private Color _outlineColor;
    [SerializeField] private Renderer _outline;
    [SerializeField] private Camera _portalCamera;
    [SerializeField, Range(1, 10)] private int _renderIterations = 3;

    private Material _material;
    private Renderer _renderer;
    private RenderTexture _rTexture;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        _rTexture = new RenderTexture(Screen.width, Screen.height, 0);
        _material.mainTexture = _rTexture;
        _portalCamera.targetTexture = _rTexture;
        _outline.material.color = _outlineColor;
        _material.SetInt("_DrawingFlag", 1);
    }

    public void Render(Camera mainCamera, Transform otherPortal)
    {
        if (!_renderer.isVisible)
            return;
        _material.SetInt("_DrawingFlag", 0);
        for (int i = _renderIterations - 1; i >= 0; i--) {
            RenderInternal(mainCamera, otherPortal, i);
            _material.SetInt("_DrawingFlag", 1);
        }
    }
    private void RenderInternal(Camera mainCamera, Transform otherPortal, int iteration)
    {
        Transform enterPoint = transform;
        Transform exitPoint = otherPortal;

        Transform portalCamTransform = _portalCamera.transform;
        portalCamTransform.position = mainCamera.transform.position;
        portalCamTransform.rotation = mainCamera.transform.rotation;

        for (int i = 0; i <= iteration; i++) {
            portalCamTransform.MirrorPosition(enterPoint, exitPoint);
            portalCamTransform.MirrorRotation(enterPoint, exitPoint);
        }

        SetupProjection(mainCamera, exitPoint);

        _portalCamera.Render();
    }

    private void SetupProjection(Camera mainCamera, Transform exitPoint)
    {
        Plane p = new Plane(-exitPoint.forward, exitPoint.position);
        Vector4 clipPlane = new Vector4(p.normal.x, p.normal.y, p.normal.z, p.distance);
        Vector4 clipPlaneCameraSpace = Matrix4x4.Transpose(
                                           Matrix4x4.Inverse(_portalCamera.worldToCameraMatrix)) * clipPlane;
        var newMatrix = mainCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        _portalCamera.projectionMatrix = newMatrix;
    }
     

public static class Rashirenia
{
    private static readonly Quaternion _halfTurn = Quaternion.Euler(0.0f, 180.0f, 0.0f);

    public static void MirrorPosition(this Transform target, Transform p1, Transform p2)
    {
        Vector3 relativePos = p1.InverseTransformPoint(target.position);
        relativePos = _halfTurn * relativePos;
        target.position = p2.TransformPoint(relativePos);
    }

    public static void MirrorRotation(this Transform target, Transform p1, Transform p2)
    {
        Quaternion relativeRot = Quaternion.Inverse(p1.rotation) * target.rotation;
        relativeRot = _halfTurn * relativeRot;
        target.rotation = p2.rotation * relativeRot;
    }
}

*/
}
