using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MindVenture.ui.UI_Debug
{
    public class UI_ShowFPS : MonoBehaviour
    {
        public TextMeshProUGUI fpsText, max, min;
        public int FPS { get; private set; }

        public int frameRange = 60;
        public int AverageFPS { get; private set; }

        int[] fpsBuffer;
        int fpsBufferIndex;

        List<int> fpsMemory = new List<int>();

        public int HighestFPS { get; private set; }
        public int LowestFPS { get; private set; }

        void Update() {
            FPS = (int)(1f / Time.unscaledDeltaTime);

            DisplayFPS();

            if (fpsBuffer == null || fpsBuffer.Length != frameRange) {
                InitializeBuffer();
            }

            UpdateBuffer();
            CalculateFPS();
        }

        void DisplayFPS(){
            if(max) max.text = Mathf.Clamp(HighestFPS, 0, 300).ToString();
            fpsText.text = Mathf.Clamp(AverageFPS,0,300).ToString();
            if(min) min.text = Mathf.Clamp(LowestFPS, 0, 300).ToString();
        }

        void InitializeBuffer() {
            if (frameRange <= 0) {
                frameRange = 1;
            }
            fpsBuffer = new int[frameRange];
            fpsBufferIndex = 0;
        }

        void UpdateBuffer() {
            fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);

            if (fpsBufferIndex >= frameRange) {
                fpsBufferIndex = 0;
            }

            if(fpsMemory.Count >= frameRange) {
                fpsMemory.RemoveAt(0);
            }

            fpsMemory.Add((int)(1f / Time.unscaledDeltaTime));
        }

        void CalculateFPS() {
            int sum = 0;
            int highest = 0;
            int lowest = int.MaxValue;
            for (int i = 0; i < frameRange; i++)
            {
                int fps = fpsBuffer[i];
                sum += fps;
                if (fps > highest)
                {
                    highest = fps;
                }
                if (fps < lowest)
                {
                    lowest = fps;
                }
            }
            AverageFPS = sum / frameRange;
            HighestFPS = highest;
            LowestFPS = lowest;
        }
    }
}