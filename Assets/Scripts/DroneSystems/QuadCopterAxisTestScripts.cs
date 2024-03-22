using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadCopterAxisTestScripts : MonoBehaviour
{
    public List<Transform> waypoints; // Список путевых точек
    public float moveSpeed = 5f;      // Скорость движения персонажа
    private int waypointIndex = 0;    // Текущий индекс путевой точки
    private bool isForward = true;    // Направление движения
    [SerializeField] private Transform endWaipoint;
    [SerializeField] private BatterySystem _battery;

    private void Start()
    {
        _battery = FindObjectOfType<BatterySystem>();
    }

    void Update()
    {
        //Имеется ли у нас достаточный заряд для полета
        if (_battery.isTankEmpty == false)
        {
            // Проверяем, есть ли путевые точки
            if (waypoints.Count == 0)
                return;

            // Перемещаем персонажа к текущей путевой точке
            Vector3 targetPosition =
                new Vector3(waypoints[waypointIndex].position.x, 3f, waypoints[waypointIndex].position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Проверяем, достиг ли персонаж текущей путевой точки
            if (transform.position == targetPosition)
            {
                // Изменяем индекс на следующую/предыдущую путевую точку
                if (isForward)
                {
                    waypointIndex++;
                    if (waypointIndex >= waypoints.Count)
                    {
                        waypointIndex -= 2; // Изменяем направление на противоположное
                        isForward = false;
                    }
                }
                else
                {
                    waypointIndex--;
                    if (waypointIndex < 0)
                    {
                        waypointIndex += 2; // Изменяем направление на противоположное
                        isForward = true;
                    }
                }
            }
        }

        if (_battery.isTankEmpty == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, endWaipoint.position, moveSpeed * Time.deltaTime); 
        }

        if (transform.position == endWaipoint.transform.position)
        {
            _battery.ReffilingDroneTank();
        }
    }
}
