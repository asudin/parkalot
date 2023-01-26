using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace LevelBuilder
{
    public class LevelBuilder : EditorWindow
    {
        private const string _pathBuildings = "Assets/LowPoly/Prefabs/Buildings";
        private const string _pathCharacters = "Assets/LowPoly/Prefabs/Characters";
        private const string _pathNature = "Assets/LowPoly/Prefabs/Nature";
        private const string _pathPlanes = "Assets/LowPoly/Prefabs/Planes";
        private const string _pathProps = "Assets/LowPoly/Prefabs/Props";
        private const string _pathRoads = "Assets/LowPoly/Prefabs/Roads";
        private const string _pathVehicles = "Assets/LowPoly/Prefabs/Vehicles";
        private const float _half = 0.5f;

        private readonly Vector2 _iconDimensions = new Vector2(100, 200);

        private Dictionary<string, List<GameObject>> _catalogs = new Dictionary<string, List<GameObject>>();

        private Dictionary<List<GameObject>, List<GUIContent>> _iconCatalogs =
            new Dictionary<List<GameObject>, List<GUIContent>>();

        private List<GameObject> _currentCatalog = new List<GameObject>();
        private GameObject _parent;
        private GameObject _createdObject = null;
        private Vector2 _scrollPosition;
        private LayerMask _propLayer;
        private string[] _layerNames;
        private int _selectedElement;
        private int _selectedTabNumber;
        private bool _building;
        private bool _collisionAllowed;
        private bool _leapToSurface;
        private float _rotationSpeed = 2f;
        private float _scaleSpeed = 1.5f;
        private float _verticalΜοvementSpeed = 20f;
        private float _lastYPosition;

        private string[] _tabNames =
            {"Buildings", "Characters", "Nature", "Planes", "Props", "Roads", "Vehicles"};

        [MenuItem("Level/Builder")]
        private static void ShowWindow()
        {
            GetWindow(typeof(LevelBuilder));
        }

        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnGUI()
        {
            
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (_building == false)
                return;

            if (_createdObject == null)
                CreateObject();

            sceneView.Focus();

            if (Raycast(out Vector3 contactPoint))
            {
                DrawPointer(Color.red);
                ManipulateCreatedObject(contactPoint);
                sceneView.Repaint();
            }
        }

        private void CreateObject()
        {

        }

        private bool Raycast(out Vector3 contactPoint)
        {
            Ray guiRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            contactPoint = Vector3.zero;

            if (Physics.Raycast
                (
                    guiRay,
                    out RaycastHit raycastHit,
                    Single.PositiveInfinity,
                    LayerMask.GetMask(LayerMask.LayerToName(_parent.layer))
                ))
            {
                contactPoint = raycastHit.point;
                return true;
            }

            return false;
        }

        private void DrawPointer(Color color)
        {

        }

        private void ManipulateCreatedObject(Vector3 contactPoint)
        {

        }
    }
}
