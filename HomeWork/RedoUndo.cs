using System.Collections.Generic;
using UnityEngine;

public class RedoUndoComponents : MonoBehaviour
{
    private const float Speed = 3.0f;

    private readonly Stack<Vector3> _undoStack = new Stack<Vector3>(); 
    private readonly Stack<Vector3> _redoStack = new Stack<Vector3>();
    private Vector3 _myPos ;

    private void Start()
    {
        _myPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Movements();
        }
        
        ApplyPosition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Undo();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            Redo();
        }
        
        
    }

    private void Movements()
    {
        Vector3 movePos = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movePos += transform.forward;
        
        if (Input.GetKey(KeyCode.S))
            movePos -= transform.forward;
        
        if (Input.GetKey(KeyCode.A))
            movePos -= transform.right;
        
        if (Input.GetKey(KeyCode.D))
            movePos += transform.right;
        
        if (Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.D))
        {
            movePos = Vector3.zero;
            SaveState();
        }
        
        _myPos += movePos.normalized * (Speed * Time.deltaTime);
    }

    private void SaveState()
    {
        _undoStack.Push(transform.position);
        _redoStack.Clear();
        Debug.Log($"새로운 위치 저장 - 언두 스택: {_undoStack.Count}, 리두 스택 초기화됨");
    }

    private void Undo()
    {
        if (!_undoStack.TryPop(out Vector3 newPosition)) return;
        _redoStack.Push(_myPos);
        _myPos = newPosition;
        Debug.Log($"Undo 실행 - 언두 스택: {_undoStack.Count}, 리두 스택: {_redoStack.Count}");
    }

    private void Redo()
    {
        if (!_redoStack.TryPop(out Vector3 newPosition)) return;
        _undoStack.Push(_myPos);
        _myPos = newPosition;
        Debug.Log($"Redo 실행 - 언두 스택: {_undoStack.Count}, 리두 스택: {_redoStack.Count}");
    }
    
    private void ApplyPosition()
    {
        transform.position = _myPos;
    }
    
}
