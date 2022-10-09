using UnityEngine;

public class CommonStateMovement: IMovementState
{
    public void Move(Movement movement)
    {
        UserInput userInput = FWC.GlobalData.UserInput;
        PlayerData playerData = FWC.GlobalData.PlayerData;
        CharacterController characterController = playerData.Player.CharacterController;

        Vector3 direction = userInput.CalculateWorldDirection(movement.Camera).normalized;
        Vector3 offset = movement.Speed * Time.deltaTime * direction * userInput.JoystickOffcet.magnitude + Physics.gravity * Time.deltaTime;
      
        characterController.Move(offset);
        
        float joystickAngle = Mathf.Atan2(userInput.JoystickOffcet.x, userInput.JoystickOffcet.y) * 180 / Mathf.PI;

        if (userInput.JoystickOffcet!=new Vector2(0,0))
            playerData.Player.Transform.rotation = Quaternion.Euler(new Vector3(0, joystickAngle, 0));
    }
}
