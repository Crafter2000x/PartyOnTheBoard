using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectMovmentScript : MonoBehaviour
{
    public GameTileScript NextTile;
    public GameTileScript PreviousTile;
    public GamePlayerInformation ConnectedPlayer;
    public List<Transform> Locations;
    [SerializeField] private Camera Camera;

    public void MovePlayerOnBoard() 
    {
        StartCoroutine("MovePlayer");
    }

    public void SwitchCam() 
    {
        if (Camera.enabled == true)
        {
            Camera.enabled = false;
        }
        else
        {
            Camera.enabled = true;
        }
    }

    IEnumerator MovePlayer() 
    {
        int AllowedMoves = ConnectedPlayer.CurrentRoll;

        for (int i = 0; i < AllowedMoves; i++)
        {
            foreach (Transform Child in NextTile.gameObject.transform)
            {
                Locations.Add(Child);
            }
            this.transform.parent = Locations[NextTile.PlayersOnTile.Count];
            this.transform.localPosition = new Vector3(0, 0, 0);
            Locations.Clear();
            yield return new WaitForSeconds(2);

        }

        ConnectedPlayer.PlayerDoneMoving = true;
    }
}


