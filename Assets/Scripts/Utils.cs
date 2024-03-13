using UnityEngine;

public static class Utils
{
    private static IClickable selectedObject;
    public static bool WasSelected<T>(T obj) where T : MonoBehaviour
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        bool wasHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue) && raycastHit.collider.GetComponent<T>() != null;
        if (wasHit)
        {
            selectedObject = (IClickable)obj.GetComponent<T>();
            ActionManager.Instance.SetSelectedTransform((IClickable) obj);
        }
        return wasHit;
    }
}
