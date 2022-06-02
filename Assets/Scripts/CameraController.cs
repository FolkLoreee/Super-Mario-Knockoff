using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Mario's Transform
    public Transform endLimit; // GameObject that indicates the end of map
    private float offset; // initial x-offset between camera and Mario
    private float startX; // smallest x-coord of the camera
    private float endX; // largest x-coord of the camera
    private float viewportHalfWidth;
    // Start is called before the first frame update
    void Start()
    {
        //get coordinate of the bottom-left of the viewport
        //z doesn't matter since camera is ortrhographic
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x); // because the camera's coordinate is in the centre

        offset = this.transform.position.x - player.position.x;
        startX = this.transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;

    }

    // Update is called once per frame
    void Update()
    {
        float desiredX = player.position.x + offset;
        if (desiredX > startX && desiredX < endX)
        {
            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        }
    }
}
