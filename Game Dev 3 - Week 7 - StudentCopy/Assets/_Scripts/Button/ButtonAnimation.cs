using UnityEngine;
using UnityEngine.EventSystems;

// This script handles button animations for hover and click effects.
// It implements the IPointerEnterHandler, IPointerExitHandler, and IPointerClickHandler interfaces
// to respond to mouse interactions with the button.
public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Reference to the settings menu GameObject (not used in this script but could be for additional functionality).
    public GameObject settingsMenu;

    // Stores the original scale of the button.
    public Vector3 originalScale;

    // Stores the scale of the button when hovered over.
    public Vector3 hoverScale;

    // Tracks whether the button was clicked (not used in this script but could be for additional logic).
    private bool wasClicked = false;

    // Initialization method called when the script starts.
    private void Start()
    {
        // Store the original scale of the button.
        originalScale = transform.localScale;

        // Calculate the hover scale by multiplying the original scale by 1.2 (20% larger).
        hoverScale = originalScale * 1.2f;
    }

    // Called when the mouse pointer enters the button's area.
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Scale the button up to the hover scale when the mouse hovers over it.
        transform.localScale = hoverScale;
    }

    // Called when the mouse pointer exits the button's area.
    public void OnPointerExit(PointerEventData eventData)
    {
        // Restore the button to its original scale when the mouse stops hovering.
        transform.localScale = originalScale;
    }

    // Called when the button is clicked.
    public void OnPointerClick(PointerEventData eventData)
    {
        // Restore the button to its original scale when clicked.
        transform.localScale = originalScale;
    }
}