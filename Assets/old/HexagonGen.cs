using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        for( int i = 0; i < 5; i++ ) {
            for( int j = 0; j < 4; j++ ) {
                HexagonTile hex = new HexagonTile( i, j );
            }
        }
	}
}
