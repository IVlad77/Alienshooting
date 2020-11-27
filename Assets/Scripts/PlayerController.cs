using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")]
    [SerializeField]
    float controlSpeed = 4f;

    [Tooltip("In m")]
    [SerializeField]
    float xRange = 2.85f;

    [Tooltip("In m")]
    [SerializeField]
    float yRange = 2.1f;

    [SerializeField]
    GameObject[] guns;

    [Header("Screen-position Based")]
    [SerializeField]
    float positionPitchFactor = -5f;

    [SerializeField]
    float positionYawFactor = -5f;

    [Header("Controll-throw Based")]
    [SerializeField]
    float controlPitchFactor = -5f;

    [SerializeField]
    float controlRollFactor = -5f;

    float xThrow;
    float yThrow;

    bool isControlEnabled = true;

    private void Update()
    {
        if(isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
        
    }

    void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor ;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOFfset = yThrow * controlSpeed * Time.deltaTime;


        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOFfset;

        float clampedYPos = Mathf.Clamp(rawYPos, -2.1f, yRange);
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("");
    }

    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emmisionModule = gun.GetComponent<ParticleSystem>().emission;
            emmisionModule.enabled = isActive;
        }
    }

}
