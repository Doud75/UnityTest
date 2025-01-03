using UnityEngine;

[System.Serializable]
public class Payload
{
    public string type;
    public float positionX;
    public float positionY;
    public float positionZ;

    public void SetPosition(Vector3 vector)
    {
        positionX = vector.x;
        positionY = vector.y;
        positionZ = vector.z;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(positionX, positionY, positionZ);
    }
}
