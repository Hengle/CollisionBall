using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {

    private Vector3 _click_point;
    private Vector3 _move_dir;
    private Rigidbody2D _rigidbody;
    public float score = 1000;
    public int forceScale = 5;

    [SerializeField]
    private float _force;
    [SerializeField]

	// Use this for initialization
	void Start () {
        _rigidbody = transform.GetComponent<Rigidbody2D>();
        //_rigidbody.velocity = new Vector2(-10, 10);
	}
	
	// Update is called once per frame
	void Update () {
        OnMouseKeepDown(0);
        OnMouseButtonUp(0);

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right*0.1f);
        }
	}

    void OnMouseButtonDown(int index)
    {
        //if(Input.GetMouseButtonDown(0))
    }

    void OnMouseKeepDown(int index)
    {
        if (_rigidbody.velocity.magnitude > 0.15)
            return;
        if (Input.GetMouseButton(index))
        {
            _click_point = Input.mousePosition;
            Vector3 screen_point = Camera.main.WorldToScreenPoint(transform.position);
            _force = (screen_point - _click_point).magnitude * forceScale;
            GameEnter.Instance.m_EventComponent.m_EventManager.FireEvent(EventID.ForceChangeEvent, new ForceChangeEventParam(_force));
        }
    }

    void OnMouseButtonUp(int index)
    {
        if (_rigidbody.velocity.magnitude > 0.15)
            return;
        if (Input.GetMouseButtonUp(index))
        {
            _click_point = Input.mousePosition;
            Vector3 screen_point = Camera.main.WorldToScreenPoint(transform.position);
            _move_dir = (screen_point - _click_point).normalized;
            _rigidbody.AddForce(_force * _move_dir);
            GameEnter.Instance.m_EventComponent.m_EventManager.FireEvent(EventID.ForceChangeEvent, new ForceChangeEventParam(0));
        }
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
            GameEnter.Instance.m_EventComponent.m_EventManager.FireEvent(EventID.ScoreChangeEvent, new ScoreChangeEventParam(score));
            return;
        }
        if(collision.transform.tag == "Player")
        {
            float enemy = collision.transform.GetComponent<EnemyLogic>().score;
            float tenpercent_enemy = 0.1f * enemy;
            float tenpercent_our = 0.1f * score;
            collision.transform.GetComponent<EnemyLogic>().score = enemy - tenpercent_enemy + tenpercent_our;
            score = score - tenpercent_our + tenpercent_enemy;
            GameEnter.Instance.m_EventComponent.m_EventManager.FireEvent(EventID.ScoreChangeEvent, new ScoreChangeEventParam(score));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    void OnMouseButtonMove(int index)
    {

    }
}
