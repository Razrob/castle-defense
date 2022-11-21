using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickOffcetTransmitter : CycleInitializerBase
{
    [SerializeField] private float _maxStickDistance;

    private JoystickUIScreen _screen;
    private Vector2 Offcet => ((Vector2)_screen.Stick.transform.position - _stickPivot) / CalculateRealMaxStickDistance();

    private Vector2 _stickPivot;

    protected override void OnInit()
    {
        _screen = UIScreenRepository.GetScreen<JoystickUIScreen>();
        _screen.Area.OnPointerDownEvent += OnJoystickEnable;
        _screen.Area.OnDraggingEvent += Dragging;
        _screen.Area.OnPointerUpEvent += OnJoystickDisable;
    }

    private void OnJoystickEnable(PointerEventData eventData)
    {
        _screen.Stick.gameObject.SetActive(true);
        _screen.StickArea.gameObject.SetActive(true);
        _stickPivot = eventData.position;
        _screen.Stick.position = _stickPivot;
        _screen.StickArea.position = _screen.Stick.position;
    }

    private void Dragging(PointerEventData eventData)
    {
        float distance = Vector2.Distance(eventData.position, _stickPivot);
        if (distance <= CalculateRealMaxStickDistance())
            _screen.Stick.position = eventData.position;
        else 
            _screen.Stick.position = _stickPivot + (eventData.position - _stickPivot).normalized * CalculateRealMaxStickDistance();

        FWC.GlobalData.UserInput.JoystickOffcet = Offcet;
    }

    private void OnJoystickDisable(PointerEventData eventData)
    {
        _screen.Stick.gameObject.SetActive(false);
        _screen.StickArea.gameObject.SetActive(false);
       FWC.GlobalData.UserInput.JoystickOffcet = Vector2.zero;
    }

    private float CalculateRealMaxStickDistance()
    {
        return _maxStickDistance * (Camera.main.pixelWidth / 1080f);
    }
}
