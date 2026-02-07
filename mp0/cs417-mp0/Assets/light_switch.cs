using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitchXR : MonoBehaviour
{
    public InputActionReference action;

    [Header("Light behavior")]
    public bool toggleEnabled = false;
    public Color colorA = Color.white;
    public Color colorB = Color.red;

    private Light pointLight;
    private bool state = true;

    void Start()
    {
        pointLight = GetComponent<Light>();
        if (pointLight == null)
        {
            Debug.LogError("LightSwitchXR must be attached to a GameObject with a Light component.");
            return;
        }
    
        if (pointLight == null) return;

        pointLight.enabled = true;
        pointLight.color = colorA;
    }

    void OnEnable()
    {
        if (action == null) return;

        action.action.Enable();
        action.action.performed += OnPerformed;
    }

    void OnDisable()
    {
        if (action == null) return;

        action.action.performed -= OnPerformed;
        action.action.Disable();
    }

    private void OnPerformed(InputAction.CallbackContext ctx)
    {
        if (pointLight == null) return;

        float v = 0f;
        try { v = ctx.ReadValue<float>(); } catch { v = 1f; }

        if (v < 0.5f) return;

        state = !state;

        if (toggleEnabled)
        {
            pointLight.enabled = state;
        }
        else
        {
            pointLight.color = state ? colorA : colorB;
        }
    }
}