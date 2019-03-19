using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour {


    public float movementSpeed;
    public float horizontal;
    public float vertical;
    public Transform upperBodyTr;

    private MobileInputManager inputManager;
    private ThirdPersonCamera thirdPersonCamera;
    private CharacterController cc;
    [SerializeField]
    private float vSpeed;
    [SerializeField]
    private float hSpeed;

	// Use this for initialization
	void Start () {
        inputManager = GetComponentInChildren<MobileInputManager>();
        thirdPersonCamera = GetComponent<ThirdPersonCamera>();
        cc = GetComponent<CharacterController>();

    }
	
	// Update is called once per frame
	void Update () {
        SetParams();
        Movement();
        
    }

    private void LateUpdate()
    {
        Rotation();
    }

    void SetParams()
    {
        horizontal = inputManager.horizontal;
        vertical = inputManager.vertical;
    }

    void Movement()
    {

        hSpeed = horizontal * movementSpeed * Time.deltaTime;
        vSpeed = vertical * movementSpeed * Time.deltaTime;

        hSpeed = (Mathf.Abs(hSpeed) > 0.03f && Mathf.Abs(vSpeed) > 0.03f) ? hSpeed / 1.4f : hSpeed;
        vSpeed = (Mathf.Abs(hSpeed) > 0.03f && Mathf.Abs(vSpeed) > 0.03f) ? vSpeed / 1.4f : vSpeed;

        cc.Move(transform.forward * vSpeed + transform.right * hSpeed);
        if (!cc.isGrounded)
            cc.Move(Physics.gravity * Time.deltaTime);
    }

    void Rotation()
    {
        if(vSpeed * vSpeed > Mathf.Epsilon || hSpeed * hSpeed > Mathf.Epsilon || inputManager.isAim)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, thirdPersonCamera.cameraTr.eulerAngles.y, 0), thirdPersonCamera.rotationSpeed * Time.deltaTime);
        }

        if (inputManager.isAim)
            upperBodyTr.rotation = Quaternion.Lerp(upperBodyTr.rotation, Quaternion.Euler(thirdPersonCamera.cameraTr.eulerAngles.x, thirdPersonCamera.cameraTr.eulerAngles.y, 0), thirdPersonCamera.rotationSpeed * Time.deltaTime);
        else
            upperBodyTr.rotation = Quaternion.Lerp(upperBodyTr.rotation, Quaternion.Euler(0, transform.eulerAngles.y, 0), thirdPersonCamera.rotationSpeed * Time.deltaTime);
    }
}
