using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    [SerializeField] private Camera _camera;

    private bool _canClick = true;

    private void Update()
    {
        if (_canClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.TryGetComponent(out Clickable clickable))
                    {
                        clickable.Hit();
                    }
                    if (hit.collider.TryGetComponent(out CoinProjectile coin))
                    {
                        Debug.Log(123);
                        coin.Hit();
                    }
                }
            }
        }
    }

    public void Set_canClick(bool canClick)
    {
        _canClick = canClick;
    }
}
