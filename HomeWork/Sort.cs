using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sort : MonoBehaviour
{
    
    public GameObject elementPrefab;
    private GameObject[] elementObjects;
    private int[] sortingOrder;
    
    private int partitionIndex;
    
    private float startTime; // 정렬 시작 시간 기록용 변수
    
    // 스왑 시 재생할 소리
    public AudioClip swapSound;
    private AudioSource audioSource;
    
    IEnumerator _BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                HighlightElements(j, j + 1, Color.red);
                yield return new WaitForSeconds(0.5f); // 비교 과정을 천천히 보여주기 위한 대기
                if (arr[j] > arr[j + 1])
                {
                    // 두 원소 교환
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    
                    // 오브젝트의 위치 교환(또는 애니메이션)
                    yield return StartCoroutine(SwapObjects(j, j + 1));
                }
                
                // 하이라이트 복구
                HighlightElements(j, j + 1, Color.white);
            }
        }
    }
    
    IEnumerator Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = (low - 1);
        HighlightElement(high, Color.blue);
        for (int j = low; j < high; j++)
        {
            HighlightElement(j, Color.red); // pivot과 비교하는 요소 강조
            yield return new WaitForSeconds(0.5f);
            if (arr[j] < pivot)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
                // 오브젝트의 위치 교환(또는 애니메이션)
                yield return StartCoroutine(SwapObjects(i, j));
            }
            HighlightElement(j, Color.white);
        }
        
        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
        yield return StartCoroutine(SwapObjects(i + 1, high));
        
        HighlightElement(i + 1, Color.white);
        partitionIndex = i + 1; // partition 결과 저장
    }
    
    IEnumerator QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            // PartitionRoutine 실행 후 partitionIndex 반환 값 사용
            yield return StartCoroutine(Partition(arr, low, high));
            int pi = partitionIndex;
            
            yield return StartCoroutine(QuickSort(arr, low, pi - 1));
            yield return StartCoroutine (QuickSort(arr, pi + 1, high));
        }
    }
    
    void HighlightElement(int index, Color color)
    {
        var renderer = elementObjects[index].GetComponent<Renderer>();
        if(renderer != null) renderer.material.color = color;
    }
    
    void HighlightElements(int indexA, int indexB, Color color)
    {
        var rendererA = elementObjects[indexA].GetComponent<Renderer>();
        var rendererB = elementObjects[indexB].GetComponent<Renderer>();

        if(rendererA != null) rendererA.material.color = color;
        if(rendererB != null) rendererB.material.color = color;
    }

    IEnumerator SwapObjects(int indexA, int indexB)
    {
        GameObject objA = elementObjects[indexA];
        GameObject objB = elementObjects[indexB];

        Vector3 startPosA = objA.transform.position;
        Vector3 startPosB = objB.transform.position;
        
        // 간단히 위치를 바로 교체하는 대신, Lerp를 통한 애니메이션 예시
        float duration = 0.01f;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            objA.transform.position = Vector3.Lerp(startPosA, new Vector3(startPosB.x, startPosA.y, startPosB.z), t);
            objB.transform.position = Vector3.Lerp(startPosB, new Vector3(startPosA.x, startPosB.y, startPosA.z), t);
            yield return null;
            
            // 스왑 직후 소리 재생
            if (audioSource != null && swapSound != null)
                audioSource.PlayOneShot(swapSound);
        }

        // 위치 교환 후 elementObjects 배열 내 순서 교환
        elementObjects[indexA] = objB;
        elementObjects[indexB] = objA;
    }
    
    IEnumerator Start()
    {
        // AudioSource 초기화
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        
        int length = 1000;
        List<int> numbers = new List<int>(length);

        // 1부터 length까지의 리스트 생성
        for (int i = 1; i <= length; i++)
        {
            numbers.Add(i);
        }

        // 리스트를 섞는(Fisher–Yates Shuffle) 함수
        for (int i = numbers.Count - 1; i > 0; i--)
        {
            int randIndex = Random.Range(0, i + 1);
            (numbers[i], numbers[randIndex]) = (numbers[randIndex], numbers[i]);
        }
        
        sortingOrder = numbers.ToArray();
        // for (var i = 0; i < sortingOrder.Length; i++)
        //     sortingOrder[i] = Random.Range(1, 2000);
        
        elementObjects = new GameObject[sortingOrder.Length];
        for (int i = 0; i < sortingOrder.Length; i++)
        {
            GameObject obj = Instantiate(elementPrefab, new Vector3(i - sortingOrder.Length, sortingOrder[i]/2.0f, 0), Quaternion.identity);
            obj.transform.localScale = new Vector3(1, sortingOrder[i], 1);
            // TMP 텍스트 업데이트
            // var textMesh = obj.GetComponentInChildren<TMPro.TextMeshPro>();
            // if (textMesh != null)
            //     textMesh.text = sortingOrder[i].ToString();

            elementObjects[i] = obj;
        }

        Debug.Log($"Unsorted Array ({sortingOrder.Length} elements): {string.Join(", ", sortingOrder)}");

        Debug.Log("Sorting started...");
        // 정렬 시작 시간 기록
        startTime = Time.time;
        
        yield return StartCoroutine(QuickSort(sortingOrder, 0, sortingOrder.Length - 1));
        // yield return StartCoroutine(_BubbleSort(sortingOrder));
        
        // 정렬 완료 시간 계산
        float endTime = Time.time;
        float elapsedTime = endTime - startTime;
        
        Debug.Log("Sorting completed!");
        Debug.Log($"Sorted Array: {string.Join(", ", sortingOrder)}");
        Debug.Log($"Sorting took: {elapsedTime:F2} seconds");
    }
    
}
