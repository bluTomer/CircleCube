using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<Vector3> MousePositionEvent;
    public event Action MouseClickEvent;

    private Camera _camera;
    private LayerMask _hitMask;
    
    public void Setup(Camera camera, LayerMask hitMask)
    {
        _camera = camera;
        _hitMask = hitMask;
    }

    private void Update()
    {
        UpdatePosition();
        UpdateClick();
    }

    private void UpdateClick()
    {
        if (MouseClickEvent != null && Input.GetMouseButton(0))
        {
            MouseClickEvent();
        }
    }
    
    private void UpdatePosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, _hitMask))
        {
            if (MousePositionEvent != null)
            {
                MousePositionEvent(hit.point);
            }
        }
    }
}
