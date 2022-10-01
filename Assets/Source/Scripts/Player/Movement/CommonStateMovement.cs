using UnityEngine;

public class CommonStateMovement: IMovementState
{
    public void Move(Movement movement)
    {
        UserInput userInput = FrameworkCommander.GlobalData.UserInput;
        PlayerData playerData = FrameworkCommander.GlobalData.PlayerData;
        CharacterController characterController = playerData.Player.CharacterController;

        Vector3 direction = userInput.CalculateWorldDirection(movement.Camera).normalized;
        Vector3 offset = movement.Speed * Time.deltaTime * direction + Physics.gravity * Time.deltaTime;
      
        characterController.Move(offset);
     
        float joystickAngle = Mathf.Atan2(userInput.JoystickOffcet.x, userInput.JoystickOffcet.y) * 180 / Mathf.PI;

        playerData.Player.Transform.rotation = Quaternion.Euler(new Vector3(0, joystickAngle, 0));
    }
}
