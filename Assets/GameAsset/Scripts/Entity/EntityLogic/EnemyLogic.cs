using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour {
    public float[] _forceRange = new float[] { 500, 3000 };
    private UnitManager _unitManager;
    public float score = 1000;
    private Rigidbody2D _rigidbody;

    // Use this for initialization
    void Start () {
        _unitManager = GameEnter.Instance.m_Unitmanger;
        _rigidbody = transform.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (_rigidbody.velocity.magnitude > 0.15)
            return;
        StartMove();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Target")
        {
            Destroy(collision.gameObject);
            Spawner spawner = GameEnter.Instance.m_SpawnerManager;
            spawner.RemoveTarget(new Vector2(collision.transform.position.x, collision.transform.position.y));
            Vector2[] new_born;
            spawner.GenRamdonPositions(1, out new_born);
            spawner.GenNewTaraget(new_born);
            score += collision.transform.GetComponent<Target>().score;
            return;
        }
        if (collision.transform.tag == "Player")
        {
            float enemy = collision.transform.GetComponent<EnemyLogic>().score;
            float tenpercent_enemy = 0.1f * enemy;
            float tenpercent_our = 0.1f * score;
            score = score - tenpercent_our + tenpercent_enemy;
        }
    }

    private void StartMove()
    {
        GameObject[] groups = _unitManager.GetUnitsNearByTarget(gameObject);
        if (groups.Length == 0)
        {
            Vector2 tempdir = Random.insideUnitCircle.normalized;
            Vector3 dir = new Vector3(tempdir.x, tempdir.y, 0);
            float randomForce = Random.Range(_forceRange[0], _forceRange[1]);
            _rigidbody.AddForce(randomForce * dir);
        }
        else
        {
            float probability = Random.Range(0, 1);
            if (probability < 0.5)
            {
                Vector2 tempdir = Random.insideUnitCircle.normalized;
                Vector3 dir = new Vector3(tempdir.x, tempdir.y, 0);
                float randomForce = Random.Range(_forceRange[0], _forceRange[1]);
                _rigidbody.AddForce(randomForce * dir);
            }
            else
            {
                Vector3 dir = _unitManager.MoveDir(transform.position, groups);
                float randomForce = Random.Range(_forceRange[0], _forceRange[1]);
                _rigidbody.AddForce(randomForce * dir);
            }
        }
    }
}
