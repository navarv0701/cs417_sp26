using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreakoutToggle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("XR Rig to move")]
    public Transform xrOrigin;

    [Header("Viewpoints")]
    public Transform insideViewpoint;
    public Transform outsideViewpoint;

    [Header("Button")]
    public InputActionReference toggleAction;

    private bool atOutside = false;

    void Start()
    {
        if (xrOrigin == null) Debug.LogError("xrOrigin is not assigned.");
        if (insideViewpoint == null) Debug.LogError("insideViewpoint is not assigned.");
        if (outsideViewpoint == null) Debug.LogError("outsideViewpoint is not assigned.");
        if (toggleAction == null) Debug.LogError("toggleAction is not assigned.");

        MoveRigTo(insideViewpoint);
        atOutside = false;
    }

    void OnEnable()
    {
        if (toggleAction == null) return;
        toggleAction.action.Enable();
        toggleAction.action.performed += OnTogglePerformed;
    }

    void OnDisable()
    {
        if (toggleAction == null) return;
        toggleAction.action.performed -= OnTogglePerformed;
        toggleAction.action.Disable();
    }

    private void OnTogglePerformed(InputAction.CallbackContext ctx)
    {
        if (xrOrigin == null || insideViewpoint == null || outsideViewpoint == null) return;

        // First press goes outside, then alternate
        atOutside = !atOutside;

        if (atOutside) MoveRigTo(outsideViewpoint);
        else MoveRigTo(insideViewpoint);
    }

    private void MoveRigTo(Transform target)
    {
        xrOrigin.position = target.position;

        xrOrigin.rotation = Quaternion.Euler(0f, target.eulerAngles.y, 0f);

        Vector3 euler = xrOrigin.eulerAngles;
        euler.y = target.eulerAngles.y;
        xrOrigin.eulerAngles = euler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
