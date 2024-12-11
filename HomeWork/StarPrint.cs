using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRepeat : MonoBehaviour
{
    public float spacing = 1.2f;
    public int startCount = 5;
    public GameObject starPrefab;
     void Start()
     {
         // 위쪽 부분
         for (int i = 1; i <= startCount; i += 2)
         {
             for (int j = 0; j < i; j++)
             {
                 for (int k = 0; k < i; k++) // Z축으로 피라미드 확장
                 {
                     // X 좌표는 중심 기준으로 배치
                     float xPosition = (j - (i - 1) * 0.5f) * spacing;
                     // Y 좌표는 줄 간격으로 조정
                     float yPosition = -(i - 1) * spacing * 0.5f;
                     // Z 좌표는 중심 기준으로 배치
                     float zPosition = (k - (i - 1) * 0.5f) * spacing;

                     Instantiate(starPrefab, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
                 }
             }
         }
     }
}

// 별찍고 시각화 하고나서 하다보니 피라미드 찍기까지한 모습...
