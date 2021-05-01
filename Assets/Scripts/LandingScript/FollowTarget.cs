using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform target;
    public Camera cameraGameObject;
    private void Update()
    {
        if (target != null)
        {
            transform.position = cameraGameObject.WorldToScreenPoint(target.position);
        }
    }
}
