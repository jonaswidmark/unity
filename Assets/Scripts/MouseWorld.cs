using UnityEngine;

public class MouseWorld : ServiceManager<MouseWorld>
{
    [SerializeField] private LayerMask mousePlaneLayerMask;
    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(ServiceLocator.InputManager.GetMouseScreenPosition());
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ServiceLocator.MouseWorld.mousePlaneLayerMask);
        return raycastHit.point;
    }
    /* public void GetTest()
    {
        //Ray ray = Camera.main.ScreenPointToRay(UnityEngine.InputSystem.Mouse.current.position.ReadValue());
        Ray ray = Camera.main.ScreenPointToRay(ServiceLocator.InputManager.GetMouseScreenPosition());
        Debug.Log(Physics.Raycast(ray));
    } */
}
