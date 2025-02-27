using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Camera mainCamera;
    public BoxCollider leftWall, rightWall, topWall, bottomWall;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        AdjustColliders();
    }

    private void AdjustColliders()
    {
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 wallScale = new Vector3(0.1f, cameraHeight, 1f); 
        Vector3 floorCeilingScale = new Vector3(cameraWidth, 0.1f, 1f);

        leftWall.transform.position = new Vector3(-cameraWidth / 2f, 0, 0);
        leftWall.transform.localScale = wallScale;

        rightWall.transform.position = new Vector3(cameraWidth / 2f, 0, 0);
        rightWall.transform.localScale = wallScale;

        bottomWall.transform.position = new Vector3(0, -cameraHeight / 2f, 0);
        bottomWall.transform.localScale = floorCeilingScale;

        topWall.transform.position = new Vector3(0, cameraHeight / 2f, 0);
        topWall.transform.localScale = floorCeilingScale;
    }
}
