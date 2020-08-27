using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicJoystickHandler: MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IUIGenericElement
{
    private bool isUIActive = true;

    private Image directionIndicator;
    private Image directionBackground;
    private Image background;
    private Image joystick;
    private Vector3 inputVector;

    public RectTransform restingPosition;
    private Vector3 backgroundRestingSpot;
    
    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.Find("Virtual Joystick Background").GetComponent<Image>();
        joystick = GameObject.Find("Virtual Joystick Stick").GetComponent<Image>();

        backgroundRestingSpot = background.rectTransform.anchoredPosition;

        directionBackground = GameObject.Find("PI Background").GetComponent<Image>();
        directionIndicator = GameObject.Find("PI Joystick").GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        if (!isUIActive)
        {
            inputVector = Vector3.zero;
            joystick.rectTransform.anchoredPosition = Vector3.zero;
            directionIndicator.rectTransform.anchoredPosition = Vector3.zero;
            return;
        }

        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out pos))
        {
            pos.x = (pos.x / background.rectTransform.sizeDelta.x);
            pos.y = (pos.y / background.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
            inputVector = (inputVector.magnitude > 1f) ? inputVector.normalized : inputVector;

            joystick.rectTransform.anchoredPosition = new Vector3(inputVector.x * (background.rectTransform.sizeDelta.x / 3), inputVector.z * (background.rectTransform.sizeDelta.y / 3));
            directionIndicator.rectTransform.anchoredPosition = new Vector3(inputVector.x * 7f * (directionBackground.rectTransform.sizeDelta.x / 3), inputVector.z * 7f * (directionBackground.rectTransform.sizeDelta.y / 3));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        background.rectTransform.position = Input.mousePosition;
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        background.rectTransform.anchoredPosition = backgroundRestingSpot;

        inputVector = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
        directionIndicator.rectTransform.anchoredPosition = Vector3.zero;
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
