using UnityEngine;

public class CameraCorrections : MonoBehaviour
{
    private void Awake()
    {
        // Неумелая попытка адаптировать игру под различные размеры экранов  |
        //                                                                   |
        //                                                                   V

        //float ratio = 16 / 10 / 1.777f; 

        //string numberStr = ratio.ToString();
        //int decimalIndex = numberStr.IndexOf('.');
        //int numbsCount = 0;

        //if (decimalIndex != -1)
        //{
        //    string decimalPart = numberStr[(decimalIndex + 1)..];
        //    numbsCount = decimalPart.Length;
        //}

        //int downNum = 1;
        //for (int i = 0; i < numbsCount; i++)
        //{
        //    downNum *= 10;
        //}

        //float ratioFraction = (ratio - (int)ratio) / downNum;


        //Camera.main.orthographicSize = 8 * ratioFraction;


        //
        //
        // Это была 4 попытка, но я не смог(
        // Теперь игра не адаптированна ((
        Debug.Log("Trying to adaptate game...");
    }
}
