using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float rotateSpeed;
    public Transform pivot;
    public bool lockCursor;

    void Start()
    {
        // When starts, the pivot moves to the player's position
        pivot.transform.position = target.transform.position;

        // Turns the pivot as player's child
        // pivot.transform.parent = target.parent;
        pivot.transform.parent = null;

        // Locks the mouse cursor
        if(lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void LateUpdate()
    {
        pivot.transform.position = target.transform.position;

        // Get the X position of the mouse & rotate the target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0, horizontal, 0);
        
        // Get the Y position of the mouse & rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(vertical, 0, 0);

        // Limit up/down camera rotation
        if(pivot.rotation.eulerAngles.x > 45f && pivot.rotation.eulerAngles.x < 180f) {
            pivot.rotation = Quaternion.Euler(45f, 0, 0);       
        }
        
        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 315f) {
            pivot.rotation = Quaternion.Euler(315f, 0, 0);       
        }

        // Move the camera based on the current rotation of the target & the original offset
        float desiredXAngle = pivot.eulerAngles.x;
        float desiredYAngle = pivot.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);
        
        if(transform.position.y < target.position.y + 0.5f) {
            transform.position = new Vector3(transform.position.x, target.position.y + .5f, transform.position.z);
        }

        transform.LookAt(target);   
    }
}
