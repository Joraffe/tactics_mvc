using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform followTransform;
    public float movementSpeed;
    public float movementTime;

    public Vector3 newPosition;
    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        this.newPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.followTransform != null)
        {
            Vector3 cameraPosition = new Vector3(followTransform.position.x, followTransform.position.y, -10);
            this.transform.position = Vector3.Lerp(this.transform.position, cameraPosition, Time.deltaTime * movementTime);;
        }
        else
        {
            HandleMouseInput();
            HandleMovementInput();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.followTransform = null;
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            
            if (plane.Raycast(ray, out entry))
            {
                this.dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;

            if (plane.Raycast(ray, out entry))
            {
                this.dragCurrentPosition = ray.GetPoint(entry);
                this.newPosition = this.transform.position + this.dragStartPosition - this.dragCurrentPosition;
            }
        }
    }

    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.up * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.up * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }

        this.transform.position = Vector3.Lerp(this.transform.position, newPosition, Time.deltaTime * movementTime);


    }
}
