using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour
{
    public Player Player;

    public bool lookAtCursor;
    public KeyCode leftButton = KeyCode.A;
    public KeyCode rightButton = KeyCode.D;
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;

    private void Start()
    {

        Player = Player == null ? GetComponent<Player>() : Player;
        if (Player == null)
        {
            Debug.LogError("Player not set to controller");
        }
    }

    private void Update()
    {

        if (Player != null)
        {

            if (Input.GetKey(KeyCode.D))
            {
                Player.MoveRight();
            }
            if (Input.GetKey(KeyCode.A))
            {
                Player.MoveLeft();
            }
        }
    }
}
