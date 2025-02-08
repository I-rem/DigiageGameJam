using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TrailRenderer>().time = 0;
        transform.position += new Vector3(0, 0, -20);
    }
    private void Update()
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
    }

}
