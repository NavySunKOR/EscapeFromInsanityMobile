using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    public Transform cameraTr;
    public Transform armTr;
    public float rotationSpeed;
    public float cameraLerpSpeed;

    public bool isInverted;
    public Vector3 cameraOffset;
    public float pitch;
    public float yaw;
    public float upperAngle;
    public float lowerAngle;
    public LayerMask collisionMask;
    private float invertedVal;
    private Vector3 targetPosition;
    private MobileInputManager inputManager;
    private Vector3 cameraInnerOffset;





    // Use this for initialization
    void Start () {
        SetInverted();
        Vector3 currentToEuler = transform.rotation.eulerAngles;
        yaw = currentToEuler.y;
        pitch = currentToEuler.x;
        cameraInnerOffset = cameraOffset ;
        inputManager = GetComponentInChildren<MobileInputManager>();
    }
	
	// Update is called once per frame
	void Update () {
        SetCameraPos();
        SetCameraOffset();
        CameraCollision();

    }

    void SetCameraPos()
    {
        float horizontal = inputManager.viewHorizontal * invertedVal * Time.deltaTime;
        float vertical = -inputManager.viewVertical * invertedVal * Time.deltaTime;

        pitch += vertical * rotationSpeed;
        yaw += horizontal * rotationSpeed;
        pitch = Mathf.Clamp(pitch, lowerAngle, upperAngle);
        armTr.eulerAngles = Vector3.Lerp(armTr.eulerAngles, new Vector3(pitch, yaw, 0), 1f);

        targetPosition = armTr.position + armTr.forward * cameraInnerOffset.z + armTr.up * cameraInnerOffset.y + armTr.right * cameraInnerOffset.x;
    }

    void SetCameraOffset()
    {
        if(inputManager.isAim)
        {
            cameraInnerOffset.x = Mathf.Lerp(cameraInnerOffset.x , cameraOffset.x * 2f, cameraLerpSpeed * Time.deltaTime);
            cameraInnerOffset.y = Mathf.Lerp(cameraInnerOffset.y,cameraOffset.y * 0.9f, cameraLerpSpeed * Time.deltaTime);
            cameraInnerOffset.z = Mathf.Lerp(cameraInnerOffset.z,cameraOffset.z / 2f, cameraLerpSpeed * Time.deltaTime);
        }
        else
        {
            cameraInnerOffset.x = Mathf.Lerp(cameraInnerOffset.x,cameraOffset.x, cameraLerpSpeed * Time.deltaTime);
            cameraInnerOffset.y = Mathf.Lerp(cameraInnerOffset.y,cameraOffset.y, cameraLerpSpeed * Time.deltaTime);
            cameraInnerOffset.z = Mathf.Lerp(cameraInnerOffset.z, cameraOffset.z, cameraLerpSpeed * Time.deltaTime);
        }
    }

    void SetInverted()
    {
        invertedVal = (isInverted) ? -1.0f : 1.0f;
    }

    void CameraCollision()
    {
        RaycastHit hit;

        if(Physics.Linecast(cameraTr.position, armTr.position, out hit, collisionMask))
        {
            cameraTr.position = hit.point;
        }
        else
        {
            cameraTr.position = targetPosition;
        }
    }
}
