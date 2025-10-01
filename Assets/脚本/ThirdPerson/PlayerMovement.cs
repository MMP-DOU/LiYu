using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Vector3 CurrentInput { get; private set; }
    public float MaxWalkSpeed = 6;

    private Quaternion _rotation;
    // Start is called before the first frame update
    private CharacterController _characterCtrl;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (MaxWalkSpeed <= 16)
            {
                if (Input.GetKey(KeyCode.Space)&&MaxWalkSpeed>6) { 
                MaxWalkSpeed = 22;
                }
                MaxWalkSpeed += 0.1f;
            }
            else
            {
                Invoke("Speeddown", 1f);
            }
        }
        else
        {
            MaxWalkSpeed = 6;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (MaxWalkSpeed >= 1)
                MaxWalkSpeed -= 0.1f;
        }
    }
    private void FixedUpdate()
    {
        _rotation = Quaternion.LookRotation(CurrentInput);
        if (CurrentInput.magnitude != 0)
        {
            Debug.Log("平滑转向");
            _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, _rotation, 10*Time.deltaTime);
           // _rigidbody.MoveRotation(_rotation);
        }
            _rigidbody.velocity = new Vector3(CurrentInput.x*MaxWalkSpeed,_rigidbody.velocity.y, CurrentInput.z * MaxWalkSpeed);
        //_rigidbody.MovePosition(_rigidbody.position + CurrentInput*MaxWalkSpeed*Time.fixedDeltaTime);//移动  自身位置+向量*速度*时间
    }

    public void SetMovementInput(Vector3 input)
    {
        Vector3.ClampMagnitude(input, 1);
        CurrentInput = input;
    }
    
    private void Speeddown()
    {
        if(MaxWalkSpeed>=16f)
        MaxWalkSpeed -= 1;
    }
}
