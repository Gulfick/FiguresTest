using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    
    private Coroutine _coroutine;
    private void Update()
    {
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");
        if (hor != 0 || ver != 0)
        {
            if(_coroutine != null)
                StopCoroutine(_coroutine);
            transform.position += new Vector3(hor * _speed * Time.deltaTime,ver * _speed * Time.deltaTime);
        }
        
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }
    }

    private IEnumerator MoveTo(Vector2 mousePos)
    {
        while (Vector2.Distance(mousePos, transform.position) > 0.02f)
        {
            transform.position = Vector2.MoveTowards(transform.position, mousePos, 
                _speed * Time.deltaTime);
            yield return null;
        }

        _coroutine = null;
    } 
}
