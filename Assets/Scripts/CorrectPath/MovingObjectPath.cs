////using UnityEngine;

////public class MovingObjectPath : MonoBehaviour
////{
////    private float _speed;
////    private int _laneIndex;
////    private bool _isFruit;

////    public void Initialize(float speed, int laneIndex, bool isFruit)
////    {
////        _speed = speed;
////        _laneIndex = laneIndex;
////        _isFruit = isFruit;
////    }

////    void Update()
////    {
////        transform.Translate(Vector3.back * _speed * Time.deltaTime);

////        if (transform.position.z < -5f)
////        {
////            if (_isFruit)
////            {
////                GameManagerPath.Instance.MissedFruit();
////            }
////            Destroy(gameObject);
////        }
////    }

////    void OnTriggerEnter(Collider other)
////    {
////        if (other.CompareTag("Player"))
////        {
////            if (_isFruit)
////            {
////                GameManagerPath.Instance.CaughtFruit();
////            }
////            else
////            {
////                GameManagerPath.Instance.HitBomb();
////            }
////            Destroy(gameObject);
////        }
////    }
////}


//using UnityEngine;

//public class MovingObjectPath : MonoBehaviour
//{
//    private float speed;
//    private bool isFruit;
//    private Vector3 direction = Vector3.back; // Towards player

//    public void Initialize(float speed, int laneIndex, bool isFruit)
//    {
//        this.speed = speed;
//        this.isFruit = isFruit;
//    }

//    void Update()
//    {
//        if (!GameManagerPath.Instance.IsGameActive) return;

//        // Continuous movement toward player
//        transform.Translate(direction * speed * Time.deltaTime);
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        Debug.Log($"Collision detected with: {other.tag}");
//        if (other.CompareTag("Player"))
//        {
//            Debug.Log($"Player hit, isFruit: {isFruit}");
//            if (isFruit)
//            {
//                GameManagerPath.Instance.CaughtFruit();
//                Destroy(gameObject);
//            }
//            else
//            {
//                GameManagerPath.Instance.HitBomb();
//                Destroy(gameObject);
//            }
//        }
//        else if (other.CompareTag("DestroyBoundary"))
//        {
//            if (isFruit) GameManagerPath.Instance.MissedFruit();
//            Destroy(gameObject);
//        }
//    }
//}


//using UnityEngine;

//public class MovingObjectPath : MonoBehaviour
//{
//    private float speed;
//    private Vector3 direction = Vector3.back; // Towards player

//    public void Initialize(float speed)
//    {
//        this.speed = speed;
//    }

//    void Update()
//    {
//        if (!GameManagerPath.Instance.IsGameActive) return;
//        transform.Translate(direction * speed * Time.deltaTime);
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            if (gameObject.CompareTag("Fruit"))
//            {
//                GameManagerPath.Instance.CaughtFruit();
//            }
//            else if (gameObject.CompareTag("Bomb"))
//            {
//                GameManagerPath.Instance.HitBomb();
//            }
//            // Don't destroy - let objects pass through player
//        }
//        else if (other.CompareTag("DestroyBoundary"))
//        {
//            if (gameObject.CompareTag("Fruit"))
//            {
//                GameManagerPath.Instance.MissedFruit();
//            }
//            Destroy(gameObject);
//        }
//    }
//}


using UnityEngine;

public class MovingObjectPath : MonoBehaviour
{
    private float speed;
    private Vector3 direction = Vector3.back;

    public void Initialize(float speed)
    {
        this.speed = speed;
    }

    void Update()
    {
        if (!GameManagerPath.Instance.IsGameActive) return;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Fruit"))
            {
                GameManagerPath.Instance.CaughtFruit();
                Destroy(gameObject); // Destroy fruit on contact
            }
            else if (gameObject.CompareTag("Bomb"))
            {
                GameManagerPath.Instance.HitBomb();
                Destroy(gameObject); // Destroy bomb on contact
            }
            // Objects will pass through if not destroyed
        }
        else if (other.CompareTag("DestroyBoundary"))
        {
            if (gameObject.CompareTag("Fruit"))
            {
                GameManagerPath.Instance.MissedFruit();
            }
            Destroy(gameObject);
        }
    }
}
