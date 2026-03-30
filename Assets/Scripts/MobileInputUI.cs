using TMPro;
using UnityEngine;

public class MobileInputUI : MonoBehaviour
{
    public PlayerRunning player;
    public TextMeshProUGUI controlText; //UI Label to show current control mode

    //Called when LEFT arrow is pressed
    public void MoveLeft()
    {
        if (player == null) return;
            
        if (player.currentMode == PlayerRunning.ControlMode.Buttons)
        {
            player.ChangeLanePublic(-1);
        }
    }

    //Called when RIGHT arrow is pressed
    public void MoveRight()
    {
        if (player == null) return;

        if (player.currentMode == PlayerRunning.ControlMode.Buttons) 
        { 
            player.ChangeLanePublic(1); 
        }
    }

    //Call when "Switch Control Mode" button is pressed
    public void SwitchControlMode()
    {
        if (player == null) return;

        if (player.currentMode == PlayerRunning.ControlMode.Buttons)
        {
            player.currentMode = PlayerRunning.ControlMode.Swipe;
            controlText.text = "Current Control: Swipe";
        }
        else if (player.currentMode == PlayerRunning.ControlMode.Swipe)
        {
            player.currentMode = PlayerRunning.ControlMode.Gyro;
            controlText.text = "Current Control: Gyro";
        }
        else
        {
            player.currentMode = PlayerRunning.ControlMode.Buttons;
            controlText.text = "Current Control: Buttons";
        }
    }
}