using UnityEngine;

public static class Utils
{
    public static bool WasSelected<T>(T obj) where T : MonoBehaviour
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        bool wasHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue) && raycastHit.collider.GetComponent<T>() != null;
        
        if (wasHit)
        {
            ActionManager.Instance.SetSelectedTransform((IClickable) obj);
        }
        return wasHit;
    }
}
