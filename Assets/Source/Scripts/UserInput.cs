using UnityEngine;

public class UserInput
{
    public bool InputLocked { get; private set; }

    private Vector2 joystickOffcet;

    public Vector2 JoystickOffcet
    {
        get => joystickOffcet;
        set
        {
            if (!InputLocked)
                joystickOffcet = value;
        }
    }

    public void ChangeInputLock(bool value)
    {
        InputLocked = value;
        joystickOffcet = Vector2.zero;
    }

    public Vector3 CalculateWorldDirection(Camera camera)
    {
        Vector3 direction = new Vector3(JoystickOffcet.x, 0f, JoystickOffcet.y);
        return Quaternion.AngleAxis(camera.transform.eulerAngles.y, Vector3.up) * direction;
    }
}
