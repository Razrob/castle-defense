using UnityEngine;

public class CommonStateMovement: IMovementState
{
    private Vector3 _lastDirection;

    public void Move(Movement movement)
    {
        UserInput userInput = FWC.GlobalData.UserInput;
        PlayerData playerData = FWC.GlobalData.PlayerData;
        CharacterController characterController = playerData.Player.CharacterController;

        Vector3 direction = Vector3.Slerp(_lastDirection, userInput.CalculateWorldDirection(movement.Camera).normalized, Time.deltaTime * 8.5f);
        Vector3 offset = movement.Speed * Time.deltaTime * direction + Physics.gravity * Time.deltaTime;

        _lastDirection = direction;

        characterController.Move(offset);

        float joystickAngle = playerData.Player.transform.eulerAngles.y;

        if (direction.magnitude >= 0.01f)
            joystickAngle = Quaternion.LookRotation(direction, Vector3.up).eulerAngles.y;

        playerData.Player.Transform.rotation = Quaternion.Euler(new Vector3(0, joystickAngle, 0));
    }
}
