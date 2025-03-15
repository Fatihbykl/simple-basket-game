using System;
using System.Collections;
using TigerForge;
using UnityEngine;
using Random = UnityEngine.Random;

public class RocketManager : MonoBehaviour
{
    public GameObject rocketPrefab;
    public GameObject warningSign;
    public float rocketSpeed;
    public float rocketRotationSpeed;
    public float waitBeforeSign;
    public float flyTime;
    public float delayTime;
    private bool _isActivated;
    private GameObject _rocket;
    private GameObject _warningSign;
    private bool _isRocketLeft;
    private bool _isRocketMoving;

    private void Update()
    {
        if (!_isRocketMoving || !_rocket) return;
        _rocket.transform.Rotate(0f, rocketRotationSpeed * Time.deltaTime, 0f);
        if (_isRocketLeft)
        {
            _rocket.transform.position += new Vector3(rocketSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            _rocket.transform.position += new Vector3(-1 * rocketSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void ActivateRocket()
    {
        if (_isActivated) { return; }

        _isActivated = true;
        StartCoroutine(RocketFly());
    }

    private IEnumerator RocketFly()
    {
        var newPos = GetPosition();
        _rocket = Instantiate(rocketPrefab, newPos, GetRotation());
        _warningSign = Instantiate(warningSign, GetSignPosition(newPos), warningSign.transform.rotation);
        while (true)
        {
            yield return new WaitForSeconds(waitBeforeSign);
            _warningSign.transform.position = new Vector3(0, 1000f, 0);
            _isRocketMoving = true;
            yield return new WaitForSeconds(flyTime);
            _isRocketMoving = false;
            _isRocketLeft = !_isRocketLeft;
            newPos = GetPosition();
            _rocket.transform.position = newPos;
            _rocket.transform.rotation = GetRotation();
            yield return new WaitForSeconds(delayTime);
            _warningSign.transform.position = GetSignPosition(newPos);
        }
    }

    private Vector3 GetSignPosition(Vector3 pos)
    {
        var offset = 2.5f;
        if (_isRocketLeft)
        {
            pos.x += offset;
        }
        else
        {
            pos.x -= offset;
        }

        pos.y += 1f;

        return pos;
    }

    private Vector3 GetPosition()
    {
        var offset = 1.5f;
        float x;
        var y = Random.Range(CameraBounds.bottomWallPos.y + offset, CameraBounds.topWallPos.y - offset);
        if (_isRocketLeft)
        {
            x = CameraBounds.leftWallPos.x - offset;
        }
        else
        {
            x = CameraBounds.rightWallPos.x + offset;
        }

        return new Vector3(x, y, 0);
    }

    private Quaternion GetRotation()
    {
        return Quaternion.Euler(_isRocketLeft ? new Vector3(0, 0, -90f) : new Vector3(0, 0, 90f));
    }
}
