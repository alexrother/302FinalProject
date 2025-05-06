using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonGenerator : MonoBehaviour {
    [System.Serializable]
    public class RoomPrefab {
        public GameObject prefab;
        public Vector2Int size;
    }

    public List<RoomPrefab> roomPrefabs;
    public GameObject horizontalHallway;
    public GameObject verticalHallway;
    public GameObject door;
    public GameObject key;
    public int numberOfRooms = 100;
    public TextMeshProUGUI seedDisplayText;
    private int seed;
    private HashSet<Vector2Int> occupiedPositions = new HashSet<Vector2Int>();
    private List<Vector2Int> roomPositions = new List<Vector2Int>();

    void Start()
    {
        seed = Random.Range(0, int.MaxValue);
        Random.InitState(seed);
        seedDisplayText.text = "" + seed;
        GenerateDungeon();
    }

    void PlaceObjectsInDungeon()
    {
        List<Vector2Int> positions = new List<Vector2Int>(occupiedPositions);
        positions.Remove(Vector2Int.zero);

        Vector2Int doorPos = positions[Random.Range(0, positions.Count)];
        positions.Remove(doorPos);
        door.transform.position = new Vector3(doorPos.x + 0.5f, doorPos.y + 0.5f, 0);
        door.SetActive(true);

        Vector2Int keyPos = positions[Random.Range(0, positions.Count)];
        key.transform.position = new Vector3(keyPos.x + 0.5f, keyPos.y + 0.5f, 0);
        key.SetActive(true);
    }


    void GenerateDungeon()
    {
        roomPositions.Clear();
        Vector2Int currentPosition = Vector2Int.zero;

        for (int i = 0; i < numberOfRooms; i++)
        {
            RoomPrefab roomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
            PlaceRoom(roomPrefab, currentPosition);
            roomPositions.Add(currentPosition);
            occupiedPositions.Add(currentPosition);

            if (i > 0) ConnectRooms(roomPositions[i - 1], currentPosition);

            currentPosition = GetNextPosition(currentPosition, roomPrefab.size);
        }

        PlaceObjectsInDungeon();
    }


    void PlaceRoom(RoomPrefab roomPrefab, Vector2Int position) {
        Instantiate(roomPrefab.prefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
    }

    void ConnectRooms(Vector2Int from, Vector2Int to) {
        Vector2Int direction = to - from;
        Vector2Int hallwayPosition = from + direction / 2;

        if (direction.x != 0) Instantiate(horizontalHallway, new Vector3(hallwayPosition.x, hallwayPosition.y, 0), Quaternion.identity);
        else if (direction.y != 0) Instantiate(verticalHallway, new Vector3(hallwayPosition.x, hallwayPosition.y, 0), Quaternion.identity);
    }

    Vector2Int GetNextPosition(Vector2Int currentPosition, Vector2Int roomSize) {
        List<Vector2Int> possibleDirections = new List<Vector2Int> {
            new Vector2Int(roomSize.x, 0),
            new Vector2Int(-roomSize.x, 0),
            new Vector2Int(0, roomSize.y),
            new Vector2Int(0, -roomSize.y)
        };

        // random shuffle directions
        for (int i=0; i<possibleDirections.Count; ++i)
        {
            int randomIndex = Random.Range(i, possibleDirections.Count);
            (possibleDirections[i], possibleDirections[randomIndex]) = (possibleDirections[randomIndex], possibleDirections[i]);
        }

        foreach (var direction in possibleDirections) {
            Vector2Int newPosition = currentPosition + direction;
            if (!occupiedPositions.Contains(newPosition)) {
                return newPosition;
            }
        }

        return currentPosition;
    }
}