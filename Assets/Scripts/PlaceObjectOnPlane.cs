using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectOnPlane : MonoBehaviour
{
    [SerializeField] private GameObject objectToPlace; // El Asset (Prefab) que deseas clonar
    private GameObject spawnedObject; // Referencia al objeto ya creado en la escena

    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // 1. Detectar si el usuario toca la pantalla (Input táctil móvil)
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            // 2. Lanzar el Raycast desde la posición del toque
            // TrackableType.PlaneWithinPolygon asegura que toque dentro de los límites del plano detectado
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                // Obtener la pose (posición y rotación) del plano impactado
                Pose hitPose = hits[0].pose;

                if (spawnedObject == null)
                {
                    // 3. Si no existe el objeto, lo instancia en la posición del plano
                    spawnedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                }
                else
                {
                    // 4. Si ya existe, simplemente lo mueve de lugar
                    spawnedObject.transform.position = hitPose.position;
                    spawnedObject.transform.rotation = hitPose.rotation;
                }
            }
        }
    }
}
