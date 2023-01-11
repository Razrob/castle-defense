using System.Collections.Generic;
using UnityEngine;

public class KeyboardOffsetTransmitter : CycleInitializerBase
{
    private List<KeyCode> _offsetButtonsIsDown;
    private Dictionary<KeyCode, Vector2> _offsetButtonsOffsetInPair;

    protected override void OnInit()
    {
        _offsetButtonsIsDown = new List<KeyCode>();
        _offsetButtonsOffsetInPair = new Dictionary<KeyCode, Vector2>();

        _offsetButtonsOffsetInPair[KeyCode.W] = new Vector2(0, 1);
        _offsetButtonsOffsetInPair[KeyCode.S] = new Vector2(0, -1);
        _offsetButtonsOffsetInPair[KeyCode.A] = new Vector2(-1, 0);
        _offsetButtonsOffsetInPair[KeyCode.D] = new Vector2(1, 0);

        foreach (var keyCodeInPair in _offsetButtonsOffsetInPair)
            KeyboardUserInput.RegisterCallback(keyCodeInPair.Key, UserInputOffsetChange);
    }

    private void UserInputOffsetChange(bool flag, KeyCode key)
    {
        Vector2 offset = _offsetButtonsOffsetInPair[key];

        if (flag == true)
        {
            FWC.GlobalData.UserInput.JoystickOffcet = offset;
            _offsetButtonsIsDown.Add(key);
        }
        else
        {
            _offsetButtonsIsDown.Remove(key);

            if (_offsetButtonsIsDown.Count == 0)
                FWC.GlobalData.UserInput.JoystickOffcet = Vector2.zero;
            else
                FWC.GlobalData.UserInput.JoystickOffcet = 
                    _offsetButtonsOffsetInPair[_offsetButtonsIsDown[_offsetButtonsIsDown.Count-1]];
        }

    }
}