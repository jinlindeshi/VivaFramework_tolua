using UnityEditor;
using UnityEngine;

public class PlaceMapBg : MonoBehaviour
{
    //地图区块预制体
    public GameObject blockPrefab;
    private static string SHADER_PATH = "Universal Render Pipeline/Simple Lit";
    private static string BLOCK_IMG_PATH = "Assets/Res/Textures/GC1Map/blocks/block{0}_{1}.jpg";
    private static string MATERIER_SAVE_PATH = "Assets/Res/Materials/World/block{0}_{1}.mat";
    
    private static float H_NUM = 6.0f;
    private static float V_NUM = 6.0f;
    
    private static Vector2 BLOCK_SIZE = new Vector2(1000,600);
    public void TakePlace()
    {
        // Vector2
        for (int i = 1; i <= V_NUM; i++)
        {
            for (int j = 1; j <= H_NUM; j++)
            {
                // GameObject blockObj = GameObject.Instantiate(blockPrefab, transform);
                // blockObj.name = "" + j + "-" + i;
                // blockObj.transform.localPosition = new Vector3((j-H_NUM/2 - 0.5f) * BLOCK_SIZE.x/100,
                //     (V_NUM/2 - i + 0.5f)  * BLOCK_SIZE.y/100, 0);
                // Material mat =new Material(Shader.Find("Universal Render Pipeline/Simple Lit"));
                // Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath(string.Format(BLOCK_IMG_PATH,j,i),typeof(Texture2D));
                // mat.mainTexture = texture;
                // blockObj.GetComponent<MeshRenderer>().material = mat;
                // Debug.Log("你妹啊：" + j + " - " + i);
            }
        }
        // GameObject block = GameObject.Instantiate(blockPrefab, transform);
        // Material mat =new Material(Shader.Find("Universal Render Pipeline/Simple Lit"));
        // Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Res/Textures/GC1Map/blocks/block1_1.jpg",typeof(Texture2D));
        // mat.mainTexture = texture;
        // block.GetComponent<MeshRenderer>().material = mat;
        // AssetDatabase.CreateAsset(mat,"Assets/mat.mat");
    }
}