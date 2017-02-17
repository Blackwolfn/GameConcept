using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexWorld {

    HexTile[,] hexTiles;
    int width;
    public int Width {
        get {
            return width;
        }
    }
    
    int height;
    public int Height {
        get {
            return height;
        }
    }

    public HexWorld(int width=5, int height=4) {
        this.width = width;
        this.height = height;

        hexTiles = new HexTile[width, height];

        for( int x = 0; x < width; x++ ) {
            for( int y = 0; y < height; y++ ) {
                hexTiles[x, y] = new HexTile( this, x, y );
            }
        }

        //Debug.Log((width*height)+" HexTiles created!");
        
    }

    public HexTile GetHexTileAt(int x, int y) {
        if( x > width || x < 0 || y > height || y < 0 ) {
            //Debug.Log( "Tile ("+x+", "+y+") is out of range!" );
            return null;
        }
        return hexTiles[x,y];
    }

    public bool IsInGrid(int x, int y) {
        if( x > width || x < 0 || y > height || y < 0 ) {
            //Debug.Log( "Tile (" + x + ", " + y + ") is not part of the grid!" );
            return false;
        }
        return true;
    }
	
}
