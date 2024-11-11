using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIGridRenderer : Graphic
{
    public GameTimerScript gameTimer;
    public Vector2Int gridSize; 
    public float thickness = 10f;

    float width;
    float height;
    float cellWidth;
    float cellHeight;
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        float gameTime = gameTimer.saveGameTimer;
        Debug.Log(gameTime);
        gridSize = new Vector2Int((int)gameTime/30, (int)gameTime / 30);
        vh.Clear();

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        cellWidth = width / (float)gridSize.x;
        cellHeight = height / (float)gridSize.y;

        int count = 0;

        for(int y = 0; y < gridSize.y; y++)
        {
            for(int x = 0; x < gridSize.x; x++)
            {
                DrawCell(x, y, count, vh);
                count++;
            }
        }
    }

    private void DrawCell(int x, int y, int index, VertexHelper vh)
    {
        float xpos = cellWidth * x;
        float ypos = cellHeight * y;

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = new Vector3(xpos, ypos);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xpos, ypos + cellHeight);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xpos + cellWidth, ypos + cellHeight);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xpos + cellWidth, ypos);
        vh.AddVert(vertex);
        //vh.AddTriangle(0, 1, 2);
        //vh.AddTriangle(2, 3, 0);

        float widthSqr = thickness * thickness;
        float distranceSqr = widthSqr / 2f;
        float distance = Mathf.Sqrt(distranceSqr);

        vertex.position = new Vector3(xpos + distance, ypos + distance);
        vh.AddVert(vertex);
        vertex.position = new Vector3(xpos + distance, ypos + (cellHeight - distance));
        vh.AddVert(vertex);
        vertex.position = new Vector3(xpos + (cellWidth - distance), ypos + (cellHeight - distance));
        vh.AddVert(vertex);
        vertex.position = new Vector3(xpos + (cellWidth - distance), ypos + distance);
        vh.AddVert(vertex);

        int offset = index * 8;

        //Left edge
        vh.AddTriangle(offset + 0, offset + 1, offset + 5);
        vh.AddTriangle(offset + 5, offset + 4, offset + 0);

        //Top edge
        vh.AddTriangle(offset + 1, offset + 2, offset + 6);
        vh.AddTriangle(offset + 6, offset + 5, offset + 1);

        //Right edge
        vh.AddTriangle(offset + 2, offset + 3, offset + 7);
        vh.AddTriangle(offset + 7, offset + 6, offset + 2);

        //Bottom edge
        vh.AddTriangle(offset + 3, offset + 0, offset + 4);
        vh.AddTriangle(offset + 4, offset + 7, offset + 3);
    }
}
