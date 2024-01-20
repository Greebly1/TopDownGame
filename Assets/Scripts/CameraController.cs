using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraTarget;
    public CameraTargetMode camTargetMode = CameraTargetMode.Player;
    public CameraMode camMode = CameraMode.Combat;

    public Vector3 targetPos {
        get { return cameraTarget.transform.position; }
        private set { cameraTarget.transform.position = value; } }
    public float camZoom = 15f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    public Vector3 camOrientation
    {
        get { return new Vector3(transform.forward.x, 0, transform.forward.z).normalized; }
    }

    public float initialXRotation = 60;
    float localXRotation = 60;
    public float maxXRot = 70;
    public float minXRot = 30;

    void Awake()
    {
        transform.rotation = Quaternion.Euler(initialXRotation, 0, 0);
    }

    void Update()
    {
        targetPos = PlayerController.ClientPlayerController.possessedPawn.transform.position;

        transform.position = targetPos + (-transform.forward * camZoom) + camOrientation * (1.5f);
    }

    public void Zoom(float amount)
    {
        camZoom -= amount;
        camZoom = Mathf.Clamp(camZoom, maxZoom, minZoom);
    }

    public void RotateX(float amount)
    {
        localXRotation += amount;

        if (localXRotation < minXRot || localXRotation > maxXRot) { 
            localXRotation = Mathf.Clamp(localXRotation, minXRot, maxXRot);
        } else
        {
            transform.Rotate(amount, 0, 0, Space.Self);
        }
    }
}

public enum CameraTargetMode
{
    Player, Mouse, Object
}

public enum CameraMode
{
    Cinematic, Combat, Roaming
}