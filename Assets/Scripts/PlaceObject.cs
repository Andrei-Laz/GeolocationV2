using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _raycastManager;

    bool isPlacing = false;
    
    void update()
    {
        if (!_raycastManager)
        {
            return;
        }

        if (Input.touchCount > 0 && Input.GetTouch)
        {
            return true;
        }
    }
}