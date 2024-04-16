using UnityEngine;

[CreateAssetMenu(fileName = "MouseWorldSO", menuName = "ServicesSO/MouseWorldSO")]
public class MouseWorldSO : ScriptableObject
{
    [SerializeField] private LayerMask mousePlaneLayerMask;
    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(ServiceLocatorSO.InputManagerSO.GetMouseScreenPosition());
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ServiceLocatorSO.MouseWorldSO.mousePlaneLayerMask);
        return raycastHit.point;
    }
    /* public void GetTest()
    {
        //Ray ray = Camera.main.ScreenPointToRay(UnityEngine.InputSystem.Mouse.current.position.ReadValue());
        Ray ray = Camera.main.ScreenPointToRay(ServiceLocator.InputManager.GetMouseScreenPosition());
        Debug.Log(Physics.Raycast(ray));
    } */
}