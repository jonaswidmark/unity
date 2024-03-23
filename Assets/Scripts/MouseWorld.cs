using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld Instance {get; private set;}
    [SerializeField] private LayerMask mousePlaneLayerMask;
    public void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There's more than one MouseWorld! " + transform + " - " + Instance);
            Destroy(gameObject);
            return ;
        }
        Instance = this;
    }
    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(ServiceLocator.InputManager.GetMouseScreenPosition());
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Instance.mousePlaneLayerMask);
        
        return raycastHit.point;
    }
    public void GetTest()
    {
        //Ray ray = Camera.main.ScreenPointToRay(UnityEngine.InputSystem.Mouse.current.position.ReadValue());
        Ray ray = Camera.main.ScreenPointToRay(ServiceLocator.InputManager.GetMouseScreenPosition());
        Debug.Log(Physics.Raycast(ray));
    }
}
