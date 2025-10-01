using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    [SerializeField]
    public Photographer _photographer;
    [SerializeField]
    private Transform _followingTarget;

    // Start is called before the first frame update
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _photographer.InitCamera(_followingTarget);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovementInput();
    }

    private void UpdateMovementInput()
    {
        Quaternion rot = Quaternion.Euler(0,_photographer.Yaw,0);
         
        _playerMovement.SetMovementInput(rot * Vector3.forward * Input.GetAxis("Vertical")
                                        + rot * Vector3.right * Input.GetAxis("Horizontal"));
    }
}
