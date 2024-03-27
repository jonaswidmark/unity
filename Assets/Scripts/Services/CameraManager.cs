using System.Collections.Generic;
using UnityEngine;

public class CameraManager : ServiceManager<CameraManager>
{
    public List<Transform> cameras; // Lista med kameratransformer att växla mellan
    private int currentCameraIndex = 0;
    [SerializeField] Transform target; // Spelarens transform, som kameran kommer att rotera runt
    public float rotationSpeed = 1f; // Hastighet för kamerarotationen
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Avståndet från spelaren som kameran kommer att placeras

    private void Start()
    {
        // Aktivera den första kameran och inaktivera resten vid start
        SwitchCamera(currentCameraIndex);
    }
    private void LateUpdate()
    {
        //Debug.Log("Camera update");
        // Beräkna önskad rotationshastighet för att rotera kameran runt spelaren
        float desiredRotation = rotationSpeed * Time.deltaTime;

        // Rota kameran runt spelaren baserat på den önskade rotationshastigheten
        transform.RotateAround(target.position, Vector3.up, desiredRotation);

        // Uppdatera kamerans position så att den alltid är på rätt avstånd från spelaren
        transform.position = target.position + offset;

        // Se till att kameran tittar mot spelaren
        transform.LookAt(target.position);
    }
    void SwitchCamera(int index)
    {
        // Loopa igenom alla kameror
        for (int i = 0; i < cameras.Count; i++)
        {
            // Aktivera kameran om dess index matchar det angivna indexet, annars inaktivera den
            cameras[i].gameObject.SetActive(i == index);
        }
    }
}
