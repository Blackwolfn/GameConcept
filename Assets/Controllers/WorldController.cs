using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    HexWorld hexWorld;
    public int width = 5;
    public int height = 4;
    public int TextureWidth = 144;
    public int TextureHeight = 144;
    public Projector projector;
    [SerializeField]
    Texture2D mouseHexTex;
    Texture2D whiteTex;
    Texture2D blackTex;
    Texture2D falloffTex;
    Material mat;
    GenerateMouseHexTexture genMouseHexTex = new GenerateMouseHexTexture();
    

    // Use this for initialization
    void Start () {
        // Initialize empty texture
        mouseHexTex = new Texture2D( TextureWidth, TextureHeight, TextureFormat.RGB24, false );
        whiteTex = new Texture2D( TextureWidth, TextureHeight, TextureFormat.RGB24, true );
        blackTex = new Texture2D( TextureWidth, TextureHeight, TextureFormat.RGB24, true );
        falloffTex = new Texture2D( 16, 1, TextureFormat.RGBA32, false );
        falloffTex.alphaIsTransparency = true;
        falloffTex.SetPixel(0,0,Color.black);

        Color[] black = new Color[( TextureWidth * TextureHeight )];
        for( int i = 0; i < black.Length; i++ ) {
            black[i] = new Color( 0, 0, 0 );
        }
        mouseHexTex.SetPixels( 0, 0, TextureWidth, TextureHeight, black );

        hexWorld = new HexWorld(width, height);

        for( int x = 0; x < hexWorld.Width; x++ ) {
            for( int y = 0; y < hexWorld.Height; y++ ) {

                HexTile hexTile_data = hexWorld.GetHexTileAt( x, y );

                GameObject hexTile_go = GameObject.CreatePrimitive( PrimitiveType.Cylinder );
                hexTile_go.transform.position = hexTile_data.tilePosition;
                hexTile_go.transform.localScale = new Vector3( 1, 0.1f, 1 );

            }
        }
        //genMouseHexTex.SetRadius(32f, 48f);
        //genMouseHexTex.SetColor( Color.black, Color.black );
        genMouseHexTex.Generate( mouseHexTex, LineDrawer.LineWidth.Medium, LineDrawer.LineWidth.Medium);
        mouseHexTex.wrapMode = TextureWrapMode.Clamp;
        mat = new Material( Shader.Find( "Projector/Light" ) );
        //mat = new Material( Shader.Find( "Projector/Multiply" ) );
        mat.SetTexture( "_ShadowTex", mouseHexTex );
        mat.SetTexture( "_FalloffTex", falloffTex );
        mat.SetColor( "_Color", Color.red);
        projector.material = mat;


	}
}
