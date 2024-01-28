using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitEye : MonoBehaviour
{
    //this script is for controlling the camera motion and activating the laser
    [SerializeField] private GameObject laser;
    private LaserLighningBehaviour laserBehaviour;

    private Quaternion StartPosition;
    private Quaternion EndPosition;

    private bool isLaserOn = false;
    private bool isRabbitEyeOn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        laserBehaviour = laser.GetComponent<LaserLighningBehaviour>();
        InitializeTransform();
    }

    public void InitializeTransform()
    {
        StartPosition = this.transform.localRotation;
        EndPosition = Quaternion.Euler(StartPosition.eulerAngles.x - 180, StartPosition.eulerAngles.y, StartPosition.eulerAngles.z);
    }


    public void EnableRabbitEye()
    {
        //Turn camera to back and make
        isRabbitEyeOn = true;
        StartCoroutine(LerpToRotation(EndPosition));

    }

    public void DisableRabbitEye()
    {
        if (isLaserOn) { DisableLaser(); }
        isRabbitEyeOn = false;
        StartCoroutine (LerpToRotation(StartPosition));
    }

    public void EnableLaser()
    {
        if (!isRabbitEyeOn)
        {
            EnableRabbitEye();
        }
        else
        {
            // If RabbitEye is already enabled, activate the laser immediately
            ActivateLaser();
        }
    }

    public void ActivateLaser()
    {
        isLaserOn = true;
        laser.SetActive(true);
        laserBehaviour.PowerLights = true;
    }

    public void DisableLaser()
    {
        isLaserOn = false;
        laser.SetActive(false);
    }

    
    IEnumerator LerpToRotation(Quaternion targetRotation)
    {
        float time = 0f;
        float duration = 1f; // Duration in seconds

        Quaternion initialRotation = transform.localRotation;

        while (time < duration)
        {
            transform.localRotation = Quaternion.Lerp(initialRotation, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRotation;

        // Activate the laser at the end of the rotation if RabbitEye is on
        if (isRabbitEyeOn && !isLaserOn)
        {
            ActivateLaser();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
