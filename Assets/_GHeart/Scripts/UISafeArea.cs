﻿using UnityEngine;


    public class UISafeArea : MonoBehaviour {


        #region Simulations

        /// <summary>
        /// Simulation device that uses safe area due to a physical notch or software home bar. For use in Editor only.
        /// </summary>
        public enum SimDevice {
            None,
            iPhoneX,
            iPhoneXsMax
        }

        public static SimDevice Sim = SimDevice.None;

        Rect[] NSA_iPhoneX = new Rect[]{
            new Rect (0f, 102f / 2436f, 1f, 2202f / 2436f),  // Portrait
            new Rect (132f / 2436f, 63f / 1125f, 2172f / 2436f, 1062f / 1125f)  // Landscape
        };

        Rect[] NSA_iPhoneXsMax = new Rect[]{
            new Rect (0f, 102f / 2688f, 1f, 2454f / 2688f),  // Portrait
            new Rect (132f / 2688f, 63f / 1242f, 2424f / 2688f, 1179f / 1242f)  // Landscape
        };
        #endregion


        RectTransform Panel;
        Rect LastSafeArea = new Rect(0, 0, 0, 0);
        [SerializeField] bool ConformX = true;  // Conform to screen safe area on X-axis (default true, disable to ignore)
        [SerializeField] bool ConformY = true;  // Conform to screen safe area on Y-axis (default true, disable to ignore)

        public float TopCutoutArea { get; private set; }

        #region Unity Event Functions
        void Awake() {
            Panel = GetComponent<RectTransform>();

            if (Panel == null) {
                Debug.LogError("Cannot apply safe area - no RectTransform found on " + name);
                Destroy(gameObject);
            }

            Refresh();
        }

        void Update() {
            Refresh();
        }
        #endregion


        #region Helpers
        private void Refresh() {
            Rect safeArea = GetSafeArea();

            if (safeArea != LastSafeArea) {
                ApplySafeArea(safeArea);
                TopCutoutArea = Screen.height - safeArea.height - safeArea.y;
            }
        }

        private Rect GetSafeArea() {
            Rect safeArea = Screen.safeArea;

            if (Application.isEditor && Sim != SimDevice.None) {
                Rect nsa = new Rect(0, 0, Screen.width, Screen.height);

                switch (Sim) {
                    case SimDevice.iPhoneX:
                        if (Screen.height > Screen.width)  // Portrait
                            nsa = NSA_iPhoneX[0];
                        else  // Landscape
                            nsa = NSA_iPhoneX[1];
                        break;
                    case SimDevice.iPhoneXsMax:
                        if (Screen.height > Screen.width)  // Portrait
                            nsa = NSA_iPhoneXsMax[0];
                        else  // Landscape
                            nsa = NSA_iPhoneXsMax[1];
                        break;
                    default:
                        break;
                }

                safeArea = new Rect(Screen.width * nsa.x, Screen.height * nsa.y, Screen.width * nsa.width, Screen.height * nsa.height);
            }

            return safeArea;
        }


        private void ApplySafeArea(Rect r) {
            LastSafeArea = r;

            // Ignore x-axis?
            if (!ConformX) {
                r.x = 0;
                r.width = Screen.width;
            }

            // Ignore y-axis?
            if (!ConformY) {
                r.y = 0;
                r.height = Screen.height;
            }

            // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
            Vector2 anchorMin = r.position;
            Vector2 anchorMax = r.position + r.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            Panel.anchorMin = anchorMin;
            Panel.anchorMax = anchorMax;

            //GameLogger.Log(string.Format("New safe area applied to {0}: x={1}, y={2}, w={3}, h={4} on full extents w={5}, h={6}",
            //name, r.x, r.y, r.width, r.height, Screen.width, Screen.height), GameLogger.LogLevel.ERROR);
        }
        #endregion

    }