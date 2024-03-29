using Kurenaiz.Utilities.Types;
using Seega.Enums;
using Seega.Types;
using UnityEngine;

public class CaptureVerifier : MonoBehaviour, ICaptureVerifier
{
    bool ICaptureVerifier.VerifyCapture (Safe2DArray fields, TileField currentField, ColorType allyColor)
    {
        var coordinates = new Coordinates (currentField);
        Coordinates[] coordsToVerify = { coordinates.up, coordinates.right, coordinates.down, coordinates.left };

        for (int i = 0; i < coordsToVerify.Length; i++)
        {
            //Verifying the piece adjacent to the currentField
            //[farTile][inBetweenTile][currentTile]
            var farCoordinates = coordsToVerify[i];
            var inBetweenTile = fields[coordsToVerify[i]];

            if (currentField.Coordinates.x > coordsToVerify[i].x)
                farCoordinates.x--;
            else if (currentField.Coordinates.x < coordsToVerify[i].x)
                farCoordinates.x++;
            else if (currentField.Coordinates.y < coordsToVerify[i].y)
                farCoordinates.y++;
            else if (currentField.Coordinates.y > coordsToVerify[i].y)
                farCoordinates.y--;

            var farTile = fields[farCoordinates];

            //Verifying the adjacent tile is an enemy
            if (inBetweenTile != null &&
                inBetweenTile.Piece != null &&
                inBetweenTile.Piece.type != allyColor)
            {
                //Veryfying if the far tile is an ally
                if (farTile != null &&
                    farTile.Piece != null &&
                    farTile.Piece.type == allyColor)
                {
                    //if the inBetweenPiece is on the middle, it can't be captured
                    if (inBetweenTile.Coordinates.x != 2 ||
                        inBetweenTile.Coordinates.y != 2)
                    {
                        inBetweenTile.Capture ();
                        return true;
                    }
                }
            }
        }

        return false;
    }
}