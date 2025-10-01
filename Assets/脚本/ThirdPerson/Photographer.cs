using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photographer : MonoBehaviour
{
    public float Pitch {  get; private set; }
    public float Yaw { get; private set; }

    public float mouseSensitivity = 5;

    public float cameraRotatingSpeed = 80;

    public float cameraYspeed = 5;
    private Transform _target;
    private Transform _camera;
    [SerializeField]
    private AnimationCurve _armLengthCurve;
    // Start is called before the first frame update
    private void Awake()
    {
        _camera = transform.GetChild(0);
    }
    void Start()
    {
        
    }
     public void InitCamera(Transform target)
    {
        _target = target;
        transform.position = target.position;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
        UpdatePosition();
        UpdateArmLength();
    }

    private void UpdateRotation()
    {
        Yaw += Input.GetAxis("Mouse X")*mouseSensitivity;
        Yaw += Input.GetAxis("Camera Rate X") *cameraRotatingSpeed*Time.deltaTime;
        Pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
        Pitch += Input.GetAxis("Camera Rate Y") * cameraRotatingSpeed * Time.deltaTime;
        Pitch = Mathf.Clamp(Pitch, -90, 90);//限定上下看角度在-90~90之间

        transform.rotation = Quaternion.Euler(Pitch,Yaw, 0);
    }

    private void UpdatePosition()
    {
        float newY = Mathf.Lerp(transform.position.y,_target.position.y, Time.deltaTime*cameraYspeed);
        transform.position = new Vector3(_target.position.x, newY,_target.position.z);
    }

    private void UpdateArmLength()
    {
        _camera.localPosition = new Vector3(0,0,_armLengthCurve.Evaluate(Pitch)*-1);
    }
}
