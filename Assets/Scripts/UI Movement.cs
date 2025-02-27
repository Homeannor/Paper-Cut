using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovement : MonoBehaviour
{
    public float sensitivity = 25f;
    public float smoothSpeed = 5f;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 offset = (mousePos - screenCenter) / screenCenter.magnitude;

        Vector3 targetPosition = initialPosition - new Vector3(offset.x, offset.y, 0) * sensitivity;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}