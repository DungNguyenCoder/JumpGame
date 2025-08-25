using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager>
{
    /// <summary>
    /// Checks if the user is currently interacting with a UI element.
    /// This method uses the EventSystem and a RaycastAll to detect UI hits.
    /// </summary>
    /// <returns>True if the user is over a UI element, false otherwise.</returns>
    public bool IsPointerOverUI()
    {
        // Get a reference to the current EventSystem
        EventSystem eventSystem = EventSystem.current;

        // Create a new PointerEventData instance for the current mouse position
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;

        // Create a list to store the results of the raycast
        List<RaycastResult> results = new List<RaycastResult>();

        // Perform a raycast against all UI elements
        eventSystem.RaycastAll(eventData, results);

        // Check if the results list contains any UI elements.
        // The results list will be empty if the pointer is not over any UI.
        return results.Count > 0;
    }
}