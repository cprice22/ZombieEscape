using UnityEngine;

public class MobileInputUI : MonoBehaviour
{
    public PlayerRunning player;

    public void MoveLeft()
    {
        if (player != null) player.ChangeLanePublic(-1);
    }

    public void MoveRight()
    {
        if (player != null) player.ChangeLanePublic(1);
    }
}