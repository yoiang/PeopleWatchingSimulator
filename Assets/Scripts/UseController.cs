using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseController : MonoBehaviour
{
    public LayerMask useLayer;
    public new Camera camera;

    private bool useUsed = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool useInput = Input.GetKey(KeyCode.E) || Input.GetMouseButton(0);
        if (useInput && !this.useUsed) {
            this.useUsed = true;

            Vector3 origin = this.camera.transform.position,//this.camera.transform.parent.TransformVector(this.camera.transform.position), 
                direction = this.camera.transform.TransformDirection(Vector3.forward);
            this.ShootUseRay(origin, direction);
        } else if (!useInput && this.useUsed) {
            this.useUsed = false;
        }

        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches) {
                if (Input.GetTouch(touch.fingerId).phase == TouchPhase.Began) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    this.ShootUseRay(ray);
                }
            }
        }
    }

    void ShootUseRay(Ray ray) {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            this.HandleRaycastHit(ray.origin, ray.direction, hit);
        } else {
            this.HandleRaycastMiss(ray.origin, ray.direction);
        }
    }

    void ShootUseRay(Vector3 origin, Vector3 direction) {
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity, this.useLayer)) {
            HandleRaycastHit(origin, direction, hit);
        } else {
            this.HandleRaycastMiss(origin, direction);
        }
    }

    private void HandleRaycastHit(Vector3 origin, Vector3 direction, RaycastHit hit) {
        Debug.DrawRay(origin, direction * hit.distance, Color.yellow, 30.0f, false);
        Debug.LogWarning("Use Ray hit " + hit.collider.gameObject, this);
        this.Use(hit.collider.gameObject);
    }

    private void HandleRaycastMiss(Vector3 origin, Vector3 direction) {
        Debug.DrawRay(origin, direction * 1000, Color.white, 30.0f, false);
        Debug.LogWarning("Use Ray from " + origin + " in direction " + direction + " did not hit anything", this);
    }

    void Use(GameObject gameObject) {
        IUsable[] usables = gameObject.GetComponents<IUsable>();
        if (usables.Length == 0) {
            Debug.LogError("Attempted to Use " + gameObject + " which does not contain any " + typeof(IUsable) + " components", this);
            return;
        }

        Debug.Log("Using IUsable components in " + gameObject);

        foreach(IUsable usable in usables) {
            usable.Use();
        }
    }
}


/*
 * public class ClickDetector : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Ended");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Mouse Up");
    }
}
*/
