using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour
{
    public PlayerOld Player;
    public bool ClikLock;

    public bool lookAtCursor;
    public KeyCode leftButton = KeyCode.A;
    public KeyCode rightButton = KeyCode.D;
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;

    private void Start()
    {

        Player = Player == null ? GetComponent<PlayerOld>() : Player;
        if (Player == null)
        {
            Debug.LogError("Player not set to controller");
        }
    }

    private void Update()
    {

        if (Player != null)
        {
            if (Input.GetKey(rightButton) || Input.GetKey(leftButton) || Input.GetKey(upButton) || Input.GetKey(downButton))
            {
                if (Input.GetKey(rightButton))
                {
                    Player.MoveRight();
                    ClikLock = false;
                }
                if (Input.GetKey(leftButton))
                {
                    Player.MoveLeft();
                    ClikLock = false;
                }
                if (Input.GetKey(upButton))
                {
                    Player.MoveUp();
                    ClikLock = false;
                }
                if (Input.GetKey(downButton))
                {
                    Player.MoveDoun();
                    ClikLock = false;
                }
            }
            else
            {
                ClikLock = true;
            }
        }
    }
}
