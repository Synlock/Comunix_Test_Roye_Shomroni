using UnityEngine;
using UnityEngine.EventSystems;
using ComunixTest.Controller;

namespace ComunixTest.View
{
    public class MoveButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] int xValue;
        public void OnPointerDown(PointerEventData eventData)
        {
            xValue = Mathf.Clamp(xValue, -1, 1);
            PlayerMovement.SetXDir(xValue);
            PlayerMovement.SetIsButtonPressed(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PlayerMovement.SetIsButtonPressed(false);
        }
    }
}