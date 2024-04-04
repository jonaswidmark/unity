using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraManager : ServiceManager<CameraManager>
{
    [SerializeField] float movementSpeed;
    [SerializeField] float movementTime;
    [SerializeField] Vector3 newPosition;
    private EventManager eventManager;
    private void Start()
    {
        newPosition = transform.position;
        eventManager = ServiceLocator.EventManager;
        eventManager.OnKeyPressed += EventManager_OnKeyPressed;
        eventManager.OnKeyReleased += EventManager_OnKeyReleased;
    }
    private void EventManager_OnKeyPressed(object sender, StringEventArgs e)
    {
        HandleMovementInput(e.StringArg);
    }
    private void EventManager_OnKeyReleased(object sender, StringEventArgs e)
    {
        Debug.Log("Key released in cameramenager");
    }
    private void HandleMovementInput(string keyPressed)
    {
        Debug.Log(keyPressed);
        if(keyPressed =="w" || keyPressed == "upArrow")
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if(keyPressed =="s" || keyPressed == "downArrow")
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if(keyPressed =="d" || keyPressed == "rightArrow")
        {
            newPosition += (transform.right * movementSpeed);
        }
        if(keyPressed =="a" || keyPressed == "leftArrow")
        {
            newPosition += (transform.right * -movementSpeed);
        }
        transform.position = Vector3.Lerp(transform.position, newPosition, movementTime * Time.deltaTime);
    }
}
