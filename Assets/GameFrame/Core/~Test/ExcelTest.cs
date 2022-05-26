using System;
using UnityEngine;

namespace GameFrame.Core._Test
{
    public class ExcelTest : MonoBehaviour
    {
        private void Awake()
        {
            var file = "/Users/attack/Documents/GameConfig/hero.xlsx";
            ExcelReader.ReadExcel(file, (line) =>
            {
                var id = Convert.ToInt32(line.ItemArray[0]);
                var name = (string) line.ItemArray[1];
                var icon = (string)line.ItemArray[2];
                Debug.Log($"{id}_{name}_{icon}");
            });
        }
    }
}