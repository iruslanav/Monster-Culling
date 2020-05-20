using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Escalado
{
    [RequireComponent(typeof(Camera))]

    public class Reescalado : MonoBehaviour
    {
        public float DesignAspectWidth = 16f;
        public float DesignAspectHeight = 9f;

        [Tooltip("si es verdadero forzara la resolucion al aspecto designado, si es false escalara lo maximo posible")]
        public bool ForceAspect = true;

        void Awake()
        {
            UpdateAspect(); 
        }

        public void UpdateAspect()
        {
            if (ForceAspect)
            {
                this.GetComponent<Camera>().aspect = (DesignAspectWidth / DesignAspectHeight);

            }
            else { Resize(); }
        }

        private void Resize()
        {
           //Aspect Ratio
           float targetaspect = (DesignAspectWidth / DesignAspectHeight);
            //Check actual aspectratio
            float windowaspect = ((float)Screen.width / (float)Screen.height);
            //Check actual vs wish aspect
            float scaleheight = (windowaspect / targetaspect);

            if (scaleheight < 1.0f)//portrait
            {
                Rect rect = GetComponent<Camera>().rect;
                rect.width = 1.0f;
                rect.height = scaleheight;
                rect.x = 0;
                rect.y = ((1.0f - scaleheight) / 2.0f);

                GetComponent<Camera>().rect = rect;
            }
            else //landscape
            {
                float scalewidth = (1.0f / scaleheight);
                Rect rect = GetComponent<Camera>().rect;
                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = ((1.0f - scalewidth) / 2.0f);
                rect.y = 0;

                GetComponent<Camera>().rect = rect;
            }

            //Create Background in black
            CreateBackGround();
        }

        private void CreateBackGround()
        {
            Camera cam = new GameObject().AddComponent<Camera>();
            cam.gameObject.isStatic = true;
            cam.depth = -10;
            cam.cullingMask = 0;
            cam.farClipPlane = 1f;
            cam.orthographic = true;
            cam.backgroundColor = Color.black;
            cam.gameObject.name = "Camara negra Fondo";
        }
    }

}

