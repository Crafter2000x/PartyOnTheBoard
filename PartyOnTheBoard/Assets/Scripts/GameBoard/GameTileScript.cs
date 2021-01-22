using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTileScript : MonoBehaviour
{
    [SerializeField] private GameTileScript NextTile;
    [SerializeField] private GameTileScript PreviousTile;
    public List<GameObject> PlayersOnTile;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("poopy");
        collision.gameObject.GetComponent<PlayerObjectMovmentScript>().NextTile = NextTile;
        collision.gameObject.GetComponent<PlayerObjectMovmentScript>().PreviousTile = PreviousTile;

        PlayersOnTile.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        PlayersOnTile.Remove(collision.gameObject);
    }
}
