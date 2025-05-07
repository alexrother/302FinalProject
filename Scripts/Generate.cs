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
        // Gen random seed
        seed = Random.Range(0, int.MaxValue);
        // Seed random
        Random.InitState(seed);
        // Display seed on UI
        seedDisplayText.text = "" + seed;
        // Call dungeon generation
        GenerateDungeon();
    }

    void PlaceObjectsInDungeon()
    {
        // Positions of "rooms"
        List<Vector2Int> positions = new List<Vector2Int>(occupiedPositions);
        // Remove the first position as that's where the player spawns
        // We don't want a door or key to spawn there as the player
        // needs to look for it
        positions.Remove(positions[0]);

        // Now, set door to random position
        Vector2Int doorPos = positions[Random.Range(0, positions.Count)];
        // Remove door pos from possible positions so key cannot spawn on Door
        positions.Remove(doorPos);
        // Move door to door position
        door.transform.position = new Vector2(doorPos.x, doorPos.y);

        // Get keypos from available positions
        Vector2Int keyPos = positions[Random.Range(0, positions.Count)];
        // Move key to key position
        key.transform.position = new Vector2(keyPos.x, keyPos.y);
    }


    void GenerateDungeon()
    {
        // Start at 0,0
        Vector2Int currentPosition = Vector2Int.zero;

        // Repeat until all rooms are places
        for (int i=0; i<numberOfRooms; ++i)
        {
            // Get random roob prefab
            RoomPrefab roomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
            // Place room prefab at position
            Instantiate(roomPrefab.prefab, new Vector3(currentPosition.x, currentPosition.y, 0), Quaternion.identity);
            // Placed a room now mark it
            roomPositions.Add(currentPosition);
            occupiedPositions.Add(currentPosition);

            // Connect last room to current room with hallway
            if (i>0) {
                ConnectRooms(roomPositions[i - 1], currentPosition);
            }

            // Now get current position after placing newroom
            currentPosition = GetNextPosition(currentPosition, roomPrefab.size);
        }

        // When rooms are done
        // Place Objects in rooms
        PlaceObjectsInDungeon();
    }

    void ConnectRooms(Vector2Int from, Vector2Int to) {
        // Get direction
        Vector2Int direction = to - from;
        // Get pos to place hallway
        Vector2Int hallwayPosition = from + direction / 2;

        // If connected horizontally
        if (direction.x != 0)
        {
            // Place horizontal hallway
            Instantiate(horizontalHallway, new Vector3(hallwayPosition.x, hallwayPosition.y, 0), Quaternion.identity);
        }
        // If connected vertically
        else if (direction.y != 0)
        {
            // Place vertical hallway
            Instantiate(verticalHallway, new Vector3(hallwayPosition.x, hallwayPosition.y, 0), Quaternion.identity);
        }
    }

    Vector2Int GetNextPosition(Vector2Int currentPosition, Vector2Int roomSize) {
        // Right, left, top, bottom
        List<Vector2Int> possibleDirections = new List<Vector2Int> {
            new Vector2Int(roomSize.x, 0),
            new Vector2Int(-roomSize.x, 0),
            new Vector2Int(0, roomSize.y),
            new Vector2Int(0, -roomSize.y)
        };

        // Random shuffle directions
        for (int i=0; i<possibleDirections.Count; ++i)
        {
            // Get random direction
            int randomIndex = Random.Range(i, possibleDirections.Count);
            // At random edge pick random direction
            (possibleDirections[i], possibleDirections[randomIndex]) = (possibleDirections[randomIndex], possibleDirections[i]);
        }

        // For each direction
        foreach (var direction in possibleDirections) {
            // Check where room would go
            Vector2Int newPosition = currentPosition + direction;
            // If existing rooms don't overlapoverlap
            if (!occupiedPositions.Contains(newPosition)) {
                // Return new position
                return newPosition;
            }
        }

        // If every room overlaps return current position
        return currentPosition;
    }
}