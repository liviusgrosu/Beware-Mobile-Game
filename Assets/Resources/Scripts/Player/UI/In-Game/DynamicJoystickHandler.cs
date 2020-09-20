using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicJoystickHandler: MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IUIGenericElement
{
    private bool isUIActive = true;

    private Image directionBackground;
    private Image background;
    private Image joystick;
    private Vector3 inputVector;

    private Vector3 backgroundRestingSpot, joystickRestingSpot, directionIndicatorRestingPoint;

    private Camera uiCam;
    
    // Start is called before the first frame update
    void Start()
    {
        uiCam = GameObject.Find("UI Camera").GetComponent<Camera>();

        background = GameObject.Find("Virtual Joystick Background").GetComponent<Image>();
        joystick = GameObject.Find("Virtual Joystick Stick").GetComponent<Image>();

        directionBackground = GameObject.Find("PI Joystick").GetComponent<Image>();

        backgroundRestingSpot = background.rectTransform.localPosition;
        joystickRestingSpot = joystick.rectTransform.localPosition;
        directionIndicatorRestingPoint = directionBackground.rectTransform.localPosition;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        if (!isUIActive)
        {
            ResetUILocations();
            return;
        }

        Vector3 mousePoint = uiCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, uiCam.nearClipPlane));
        Vector3 backgroundCentre = background.rectTransform.position;
        float distance = Vector3.Distance(mousePoint, backgroundCentre);
        if (distance > 1f)
        {
            Vector3 mousePointDir = mousePoint - backgroundCentre;
            mousePointDir *= 1 / distance;
            mousePoint = backgroundCentre + mousePointDir;
        }

        joystick.rectTransform.position = mousePoint;
        Vector2 moveDir = new Vector2(
            joystick.rectTransform.localPosition.x / background.rectTransform.sizeDelta.x, 
            joystick.rectTransform.localPosition.y / background.rectTransform.sizeDelta.y
            );

        inputVector = new Vector3(moveDir.x * 2, 0, moveDir.y * 2);
        inputVector = (inputVector.magnitude > 1f) ? inputVector.normalized : inputVector;
        
        directionBackground.rectTransform.localPosition = new Vector3(inputVector.x, directionBackground.rectTransform.localPosition.y, inputVector.z);
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        Vector3 point = uiCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, uiCam.nearClipPlane));
        background.rectTransform.position = point;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        ResetUILocations();
    }

    private void ResetUILocations()
    {
        inputVector = Vector3.zero;
        joystick.rectTransform.localPosition = joystickRestingSpot;
        directionBackground.rectTransform.localPosition = directionIndicatorRestingPoint;
        background.rectTransform.localPosition = backgroundRestingSpot;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");

    }
    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }

    public void ToggleUI(bool state)
    {
        isUIActive = state;
        //TODO: refactor this
        background.enabled = state;
        joystick.gameObject.SetActive(state);
    }
}
