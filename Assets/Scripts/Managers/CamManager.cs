using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField]
    private GridManager m_GridManager;

    // TODO : use a different data structure
    private List<float> m_CamSizes;
    private List<Vector3> m_CamPoses;

    private Camera m_Cam;

    [SerializeField] private float m_ZoomSpeed = 2f;

    void Start()
    {
        Init();
    }

    void LateUpdate()
    {
        Zoom();
    }

    public void Init()
    {
        m_Cam = Camera.main;
        float width  = (m_Cam.ViewportToWorldPoint(new Vector3(1, 0))
                     - Camera.main.ViewportToWorldPoint(new Vector3(0, 0))).magnitude;

        float heigth = (m_Cam.ViewportToWorldPoint(new Vector3(0, 1)) 
                     - Camera.main.ViewportToWorldPoint(new Vector3(0, 0))).magnitude 
                     - (m_Cam.orthographicSize / 4.5f);

        // Initialize the template grid
        m_GridManager.InitTemplateGrid(Mathf.FloorToInt(width), Mathf.FloorToInt(heigth));
        UpdateCameraPosition(width, heigth);
        InitZoomValues();
    }

    private void InitZoomValues()
    {
        m_CamSizes = new List<float>();
        m_CamPoses = new List<Vector3>();
        m_CamSizes.Add(m_Cam.orthographicSize);
        m_CamPoses.Add(m_Cam.transform.position);
    }


    private void Zoom()
    {
        float mouseWheel = Input.GetAxisRaw("Mouse ScrollWheel");

        //scroll up
        if (mouseWheel > 0)
        {
            //// bounds check (may be unnessecary in stand-alone version) 
            Vector3 mouseWorldPos = m_Cam.ScreenToWorldPoint(Input.mousePosition);

            if (mouseWorldPos.x < -0.5f || mouseWorldPos.x > m_GridManager.TemplateGrid.Width ||
                mouseWorldPos.y < -0.5f || mouseWorldPos.y > m_GridManager.TemplateGrid.Height)
                return;

            Vector3 mousePrevPos = mouseWorldPos;
            // Calculate the zoom level
            float zoomLevel = m_Cam.orthographicSize - mouseWheel * m_ZoomSpeed;
            zoomLevel = Mathf.Clamp(zoomLevel, 1, m_CamSizes[0]);

            // zoom
            m_Cam.orthographicSize = zoomLevel;

            // calculate new mouse world pos
            Vector3 mouseCurrPos = m_Cam.ScreenToWorldPoint(Input.mousePosition);

            // Move the camera to mouse position
            m_Cam.transform.position += mousePrevPos - mouseCurrPos;

            // if not completely zoomed in
            if (m_Cam.orthographicSize > 1)
            {
                m_CamSizes.Add(m_Cam.orthographicSize);
                m_CamPoses.Add(m_Cam.transform.position);
            }
        }
        //scroll down
        else if (mouseWheel < 0)
        {
            // revert
            m_Cam.orthographicSize = m_CamSizes[m_CamSizes.Count - 1];
            m_Cam.transform.position = m_CamPoses[m_CamPoses.Count - 1];
            if (m_CamSizes.Count > 1)
            {
                m_CamSizes.RemoveAt(m_CamSizes.Count - 1);
                m_CamPoses.RemoveAt(m_CamPoses.Count - 1);
            }
        }
    }

    private void UpdateCameraPosition(float _width, float _heigth)
    {
        float camOffsetX = (_width - (int)_width) / 2f;
        float camOffsetY = (_heigth - (int)_heigth) / 2f;

        m_Cam.transform.position = new Vector3(_width / 2f - 0.5f - camOffsetX, _heigth / 2f - 0.5f - camOffsetY, -10f);
    }

    public void OnSliderChange_Resize(float _index)
    {
        // set camera size
        m_Cam.orthographicSize = Mathf.FloorToInt(_index);

        // calcuate the width and height of viewport in world space (with y offset for fitting UI buttons)
        float width  = (m_Cam.ViewportToWorldPoint(new Vector3(1, 0))
                     - Camera.main.ViewportToWorldPoint(new Vector3(0, 0))).magnitude;

        float heigth = (m_Cam.ViewportToWorldPoint(new Vector3(0, 1))
                     - Camera.main.ViewportToWorldPoint(new Vector3(0, 0))).magnitude
                     - (m_Cam.orthographicSize / 4.5f); // scale the offset based on camera size

        // resize the grid
        m_GridManager.TemplateGrid.UpdateGrid((int)width, Mathf.FloorToInt(heigth));

        UpdateCameraPosition(width, heigth);

        // Store new zoom values
        InitZoomValues();
    }
}
