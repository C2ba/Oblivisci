using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;
using UnityEngine.UIElements;

public class InventoryToggle : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] GameObject inventoryMenu;
    //[SerializeField] InputActionReference toggleButton;
    [SerializeField] InputAction toggle;
    [SerializeField] EventReference openInventory;
    [SerializeField] EventReference closeInventory;
    bool isInventoryOpen;

    [Header("Options Menu")]
    [SerializeField] GameObject OptionsMenu;
    //[SerializeField] InputActionReference toggleButton;
    [SerializeField] InputAction OptionsToggle;
    bool isOptionsOpen;

    void Start()
    {
        if (inventoryMenu != null)
        {
            inventoryMenu.SetActive(false);
            isInventoryOpen = false;
        }

        if (OptionsMenu != null)
        {
            OptionsMenu.SetActive(false);
            isOptionsOpen = false;
        }
    }

    private void OnEnable()
    {
        if (toggle != null)
        {
            toggle.Enable();
            toggle.performed += ToggleCanvas;
        }

        if (OptionsToggle != null)
        {
            OptionsToggle.Enable();
            OptionsToggle.performed += ToggleOptionsCanvas;
        }
    }

    private void OnDisable()
    {
        if (toggle != null)
        {
            toggle.Disable();
            toggle.performed -= ToggleCanvas;
        }

        if (OptionsToggle != null)
        {
            OptionsToggle.Disable();
            OptionsToggle.performed -= ToggleOptionsCanvas;
        }
    }

    private void ToggleCanvas(InputAction.CallbackContext context)
    {
        if (inventoryMenu == null)
        {
            return;
        }

        if (isInventoryOpen)
        {
            FMODUnity.RuntimeManager.PlayOneShot(openInventory);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot(closeInventory);
        }

        inventoryMenu.SetActive(!inventoryMenu.activeSelf);
    }

    private void ToggleOptionsCanvas(InputAction.CallbackContext context)
    {
        if (OptionsMenu == null)
        {
            return;
        }

        if (isOptionsOpen)
        {
            FMODUnity.RuntimeManager.PlayOneShot(openInventory);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot(closeInventory);
        }

        OptionsMenu.SetActive(!OptionsMenu.activeSelf);
    }
}
